using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sp_system_suggestions_29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"
CREATE OR REPLACE PROCEDURE public.sp_system_suggestions(
	IN limit_count integer,
	IN pagination integer,
	IN minimum_percentage_for_system_suggestions integer,
	IN requisition_details jsonb,
	IN employee_details jsonb,
	IN requisition_id uuid,
	IN parameter_value_pairs text[],
	IN filter jsonb,
	IN pref_weightage_constraint real,
	IN order_score_by text,
	OUT var_resp json)
LANGUAGE 'plpgsql'
AS $BODY$

declare
	employees jsonb;
	max_score bigint := 1;
	score_calculated integer := 0;
	requisition_parameters jsonb;
	parameter_checking text;
	parameter_weightage integer;
	parameter_priority text;
	employee_basic_parameter_value text;
	employee_pref_parameter_value text [];
	requisition_parameter_value text;
	score_breakup jsonb := '[]';
	score_breakup_row Record;
	pref_data jsonb;
	employee_failed_must_have boolean := false;
	free_from_days int := 0;
	working_in_same_project bool := false;
	leaves jsonb := '{}';
	where_condition text := '';
	
--	Constants
	must_have text := 'must have';
	good_to_have text := 'good to have';
	total_per_day_effort int := 8;
	preferred_match text := 'preferred';
	base_match text := 'base';
	no_match text := '';
	mandatory text := 'mandatory';
	optional text := 'optional';
	
--  Looping variables
	i text;
	skillItem jsonb;
	skillItemsArray jsonb[];
	param_value_pair text;
	checkParameterName text;
	checkParameterValue decimal;
	filter_user bool;
begin

-- 	Total Weightage of parameters
	select SUM(rp.""RequisitionWeight"") into max_score
	from public.""RequisitionParameters"" as rp
	where ""RequisitionId"" = requisition_id
	and ""IsChecked"" = true
	and ""RequisitionWeight"" is not null;
	
--  Create a temporary table to store fetched all user skills
	create temp table user_skills_temp_table(
		skill_name text,	
		skill_code text	
	);
	
--  Create a temporary table to store matched user skills with requisition
	create temp table matched_user_skill_temp_table(
		skill_name text,	
		skill_code text,	
		skill_type text
	);
	
--  Create a temporary table to store requisition skills
	create temp table requisition_skill_temp_table(
		skill_name text,	
		skill_code text,	
		skill_type text
	);
	
--  Create a temporary table to store score breakup
	create temp table score_breakups_table(
		parameter text,
		value decimal,
		matched_type text,
		matching_value text
	);
	
--  Create a temporary table to store finalUserList
	create temp table final_user_data(
		""empName"" text,
		""email"" text,
		""designation"" text,
		""grade"" text,
		""competency"" text,
		""competencyId"" text,
		""location"" text,
		""supercoach"" text,
		""revenue_unit"" text,
		""business_unit"" text,
		""sub_industry"" text,
		""industry"" text,
		""skill"" jsonb,
		""pref_location"" text,
		""pref_skill"" text,
		""pref_industry"" text,
		""pref_sub_industry"" text,
		""score_breakup"" jsonb,
		""score"" decimal,
		""available"" bool,
		""interested"" bool,
		""free_from_days"" int
	);
	
	
-- 	Extract the optional Skills of the requisition and insert into requisition skill table
	insert into requisition_skill_temp_table
	(skill_name, skill_code, skill_type)
	SELECT 
		skill->>'SkillName' as skillName,
		skill->>'SkillCode' as skillCode,
		optional as skill_type
	FROM jsonb_array_elements(requisition_details->'RequisitionSkill') AS skill
	WHERE TRIM(LOWER(skill->>'Type')) = TRIM(LOWER('Additional Optional Skills'));

-- 	Extract the mandatory Skills of the requisition and insert into requisition skill table
	insert into requisition_skill_temp_table
	(skill_name, skill_code, skill_type)
	SELECT 
		skill->>'SkillName' as skillName,
		skill->>'SkillCode' as skillCode,
		mandatory as skill_type
	FROM jsonb_array_elements(requisition_details->'RequisitionSkill') AS skill
	WHERE TRIM(LOWER(skill->>'Type')) = TRIM(LOWER('Expertise skills (These are mandatory for requisition)'));
	
--- Iterate over each employee ---
	for employees in 
		select * from jsonb_array_elements(employee_details) 
	loop 
	
-- 		Reset for next user ---
		delete from score_breakups_table;
		delete from matched_user_skill_temp_table;
		delete from user_skills_temp_table;
		score_breakup := '{}';
		score_calculated := 0;
		free_from_days := 0;
		employee_failed_must_have := false;
		filter_user := false;
		
-- 		Extract all the skills of the user and store them into user skills table
-- 		skillItemsArray := employees->>'skill';
-- 		insert into user_skills_temp_table
-- 		(skill_name, skill_code)
-- 		SELECT 
-- 			skill->>'SkillName' as skillName,
-- 			skill->>'SkillCode' as skillCode
-- 		FROM jsonb_array_elements(skillItemsArray) AS skill;
		
		
		FOR skillItem IN SELECT * FROM jsonb_array_elements(employees->'skill')
		LOOP
			INSERT INTO user_skills_temp_table
			(skill_name, skill_code)
			VALUES 
			(skillItem->>'SkillName', skillItem->>'SkillCode');
		END LOOP;
		
		

-- 		Match mandatory skills of the requisition against user skills and insert into matched user skills table
		insert into matched_user_skill_temp_table
		(skill_name, skill_code, skill_type)
		SELECT 
			req_skill.""skill_name"" as skill_name,
			req_skill.""skill_code"" as skill_code,
			mandatory as skill_type
		FROM requisition_skill_temp_table AS req_skill
		join user_skills_temp_table 
		on user_skills_temp_table.skill_code = req_skill.skill_code
		WHERE req_skill.""skill_type"" = mandatory;
		
-- 		Match optional skills of the requisition against user skills and insert into matched user skills table
		insert into matched_user_skill_temp_table
		(skill_name, skill_code, skill_type)
		SELECT 
			req_skill.""skill_name"" as skill_name,
			req_skill.""skill_code"" as skill_code,
			optional as skill_type
		FROM requisition_skill_temp_table AS req_skill
		join user_skills_temp_table 
		on user_skills_temp_table.skill_code = req_skill.skill_code
		WHERE req_skill.""skill_type"" = optional ;
		
-- 		Check if user is previously rejected for this project ---
		if not exists ( 
						select * 
						from public.""UnPublishedResAllocDetails"" un_pub_details
						join public.""Requisition""
						on ""Requisition"".""Id"" = un_pub_details.""RequisitionId""
						where trim(lower(un_pub_details.""EmpEmail"")) = trim(lower(employees->>'email'))
						and trim(""Requisition"".""PipelineCode"") = trim(requisition_details->>'PipelineCode') 
						and ((""Requisition"".""JobCode"" is null and requisition_details->>'JobCode' is null) or (trim(""Requisition"".""JobCode"") = trim(requisition_details->>'JobCode')))
						and trim(lower(un_pub_details.""AllocationStatus"")) = 'rejected'
				)
			then 
				if( select count(*)
					from public.""PublishedResAllocDetails"" pub_details
				   	join public.""Requisition""
					on ""Requisition"".""Id"" = pub_details.""RequisitionId""
					where trim(lower(pub_details.""EmpEmail"")) = trim(lower(employees->>'email'))
					and trim(""Requisition"".""PipelineCode"") = trim(requisition_details->>'PipelineCode')
					and ((""Requisition"".""JobCode"" is null and requisition_details->>'JobCode' is null) or (trim(""Requisition"".""JobCode"") = trim(requisition_details->>'JobCode')))
					and pub_details.""IsActive"" = true
				) > 0 
				then 
					working_in_same_project := true;
				else 
					working_in_same_project := false;
				end if;
			
				leaves := jsonb_agg(jsonb_build_object('email',employees->>'email','leavesAllocationDTOs','{}','totalhours',cast (employees->>'leaves' as int)));
			
--	 			Fetch bench time of user
				SELECT 
					case
						when ( GREATEST(EXTRACT(EPOCH FROM (CURRENT_TIMESTAMP - published_days.""AllocationDate"")) / 86400 - 1,0) > 0) 
						then 
							GREATEST(EXTRACT(EPOCH FROM (CURRENT_TIMESTAMP - published_days.""AllocationDate"")) / 86400 - 1,0)
						else NULL
					END 
				INTO free_from_days	
				FROM public.""PublishedResAllocDays"" published_days
				join public.""PublishedResAlloc"" published_allocations
				on published_allocations.""Id"" = published_days.""PublishedResAllocId""
				join public.""PublishedResAllocDetails"" published_details
				on published_details.""Id"" = published_allocations.""PublishedResAllocDetailsId""
				WHERE TRIM(LOWER(published_details.""EmpEmail"")) = TRIM(LOWER(employees->>'email'));

-- 				Logic to calculate score
				for requisition_parameters IN
					select * from jsonb_array_elements(requisition_details->'RequisitionParameters') 
				loop 
					parameter_checking := trim(cast(requisition_parameters->>'Category' as varchar));
					parameter_weightage := CAST(requisition_parameters->>'RequisitionWeight' AS decimal);
					parameter_priority := case 
											when parameter_weightage = 10 
											then 
												must_have 
											else 
												good_to_have 
											end;

					if lower(parameter_checking) = 'revenue unit' 
					then 	
						parameter_checking := 'revenue_unit';
					elseif lower(parameter_checking) = 'same client' 
					then 
						parameter_checking := 'same_client';
					end if;

-- 					Store the preferred data of a category in a array of text ---
					employee_pref_parameter_value := '{}';
					pref_data := employees->>('pref_' || lower(parameter_checking));
					
					for i in 
							select * from jsonb_array_elements_text(pref_data) 
					loop 
						employee_pref_parameter_value := employee_pref_parameter_value || array [trim(lower(i))];
					end loop;

					employee_basic_parameter_value := trim(Lower(cast(employees->>lower(parameter_checking) as varchar)));

-- 					Score assigning 
					select trim(Lower(requisition_details->>parameter_checking)) 
					into requisition_parameter_value
					from to_jsonb(requisition_details);

					if(lower(trim(parameter_checking)) = 'business_unit')
					then
						requisition_parameter_value = trim(lower(requisition_details->>'BusinessUnit'));
					end if;
					
					if(lower(trim(parameter_checking)) = 'competency')
					then
						requisition_parameter_value = trim(lower(requisition_details->>'Competency'));
					end if;
					
					if(lower(trim(parameter_checking)) = 'offerings')
					then
						requisition_parameter_value = trim(lower(requisition_details->>'Offerings'));
					end if;
					
					if(lower(trim(parameter_checking)) = 'solutions')
					then
						requisition_parameter_value = trim(lower(requisition_details->>'Solutions'));
					end if;

					case
-- 				 		Checking for optional skills 
						when trim(lower(parameter_checking)) = 'skill' or trim(lower(parameter_checking)) = 'skills' 
						then 
							if((select count(*) from matched_user_skill_temp_table skill where skill.skill_type = optional)  > 0) 
							then
-- 								Divide the total skills weightage among each optional skill of the user
								insert into score_breakups_table 
								values (
									requisition_parameters->>'Category'
									,parameter_weightage / (select count(*) from requisition_skill_temp_table where requisition_skill_temp_table.skill_type = optional ) * (select count(*) from matched_user_skill_temp_table skill where skill.skill_type = optional)
									,base_match
									,array_to_string((select array_agg(skill_name) from matched_user_skill_temp_table skill where skill.skill_type = optional),',')
								);
							else
								insert into score_breakups_table 
								values(
									requisition_parameters->>'Category'
									,0
									,no_match
									,''
								);
							end if;

-- 						Checking for offerings
						when lower(parameter_checking) = 'offerings'
						then 
							IF EXISTS (
								SELECT 1
								FROM jsonb_array_elements_text(employees->'offerings') AS offering
								WHERE lower(offering) = lower(requisition_parameter_value)
							)
						then
								insert into score_breakups_table 
								values(
									requisition_parameters->>'Category'
									, parameter_weightage
									, base_match
									, requisition_parameter_value
								);
							elseif parameter_priority = good_to_have
										and ((select count(*) from jsonb_array_elements_text(employees->'pref_offerings') AS offering WHERE lower(offering) = lower(requisition_parameter_value)) > 0)
							then
								insert into score_breakups_table 
								values (
									requisition_parameters->>'Category'
									,round(parameter_weightage * pref_weightage_constraint)
									,preferred_match
									,requisition_parameter_value
								);
-- 							Check if the parameter is a must have and haven't fulfilled the must have condition ---
							elseif parameter_priority = must_have 
							then 
								employee_failed_must_have := true;
								exit;
-- 							The parameter is a good to have and no value matches give 0 score ---
							else
								insert into score_breakups_table 
								values(
									requisition_parameters->>'Category'
									,0
									,no_match
									,''
								);
							end if;
							
-- 						Checking for solutions
						when lower(parameter_checking) = 'solutions'
						then 
							IF EXISTS (
								SELECT 1
								FROM jsonb_array_elements_text(employees->'solutions') AS solutions
								WHERE lower(solutions) = lower(requisition_parameter_value)
							)
						then
								insert into score_breakups_table 
								values(
									requisition_parameters->>'Category'
									, parameter_weightage
									, base_match
									, requisition_parameter_value
								);
							elseif parameter_priority = good_to_have
										and ((select count(*) from jsonb_array_elements_text(employees->'pref_solutions') AS solutions WHERE lower(solutions) = lower(requisition_parameter_value)) > 0)
							then
								insert into score_breakups_table 
								values (
									requisition_parameters->>'Category'
									,round(parameter_weightage * pref_weightage_constraint)
									,preferred_match
									,requisition_parameter_value
								);
-- 							Check if the parameter is a must have and haven't fulfilled the must have condition ---
							elseif parameter_priority = must_have 
							then 
								employee_failed_must_have := true;
								exit;
-- 							The parameter is a good to have and no value matches give 0 score ---
							else
								insert into score_breakups_table 
								values(
									requisition_parameters->>'Category'
									,0
									,no_match
									,''
								);
							end if;
						
-- 				 		Checking for competency, business unit 
						when lower(parameter_checking) = 'business_unit' 
							or lower(parameter_checking) = 'competency' 
						then 
-- 							Check if employee base value matches ---
							if requisition_parameter_value = employee_basic_parameter_value 
							then
								insert into score_breakups_table 
								values (
									requisition_parameters->>'Category'
									,parameter_weightage
									,base_match
									,requisition_parameter_value
								);

-- 							Check if employee preferred value matches only when the parameter is a good to have ---
							elseif parameter_priority = good_to_have
										and (requisition_parameter_value) = any(employee_pref_parameter_value) 
							then
								insert into score_breakups_table 
								values (
									requisition_parameters->>'Category'
									,round(parameter_weightage * pref_weightage_constraint)
									,preferred_match
									,requisition_parameter_value
								);

-- 							Check if the parameter is a must have and haven't fulfilled the must have condition ---
							elseif parameter_priority = must_have 
							then 
								employee_failed_must_have := true;
								exit;
-- 							The parameter is a good to have and no value matches give 0 score ---
							else
								insert into score_breakups_table 
								values(
									requisition_parameters->>'Category'
									,0
									,no_match
									,''
								);
							end if;
							
-- 						Checking for same client
						when lower(parameter_checking) = 'same_client' 
						then 
-- 							Query to check for same client ** ---
							if (
								select count(*)
								FROM public.""PublishedResAllocDetails"" resource_allocations_details
								join public.""Requisition"" requisition_info
								on requisition_info.""Id"" = resource_allocations_details.""RequisitionId""
								join public.""PublishedResAllocDays"" published_allocation_days
								on requisition_info.""Id"" = published_allocation_days.""RequisitionId""
								where trim(lower(resource_allocations_details.""EmpEmail"")) = trim(lower(employees->>'email'))
									and trim(lower(requisition_info.""ClientName"")) = trim(lower(requisition_details->>'ClientName'))
									and resource_allocations_details.""IsActive"" = true
									and resource_allocations_details.""EndDate"" <= current_timestamp
									and resource_allocations_details.""EndDate"" >= current_timestamp - interval '1 year'
									and published_allocation_days.""ActualEffort"" > 0
								) > 0 
							then
								insert into score_breakups_table 
								values(
									requisition_parameters->>'Category'
									,parameter_weightage
									,no_match
									,''
								);

							elseif parameter_priority = must_have 
							then 
								employee_failed_must_have := true;
								exit;
							else
								insert into score_breakups_table
								values(
									requisition_parameters->>'Category'
									, 0
									,no_match
									,''
								);
							end if;

-- 						Checking for location ** ---
						when lower(parameter_checking) = 'location' 
						then 
							if (
								select count(*)
								from jsonb_array_elements(requisition_details->'RequisitionParameterValues') as rq
								where trim(lower(rq->>'Value')) = employee_basic_parameter_value 
								and trim(lower(rq->>'Parameter'))='location'
								) >= 1 
							then
								insert into score_breakups_table 
								values(
									requisition_parameters->>'Category'
									,parameter_weightage
									,base_match
									,employee_basic_parameter_value
								);

							elseif parameter_priority = good_to_have
										and (select count(*)
											from jsonb_array_elements(requisition_details->'RequisitionParameterValues') as rq
											where trim(lower(rq->>'Value')) = any(employee_pref_parameter_value)
											 and trim(lower(rq->>'Parameter'))='location'
										) >= 1 
							then
								insert into score_breakups_table 
								values(
									requisition_parameters->>'Category'
									,round(parameter_weightage * pref_weightage_constraint)
									,preferred_match
									,array_to_string((select array_agg(rq->>'Value')
											from jsonb_array_elements(requisition_details->'RequisitionParameterValues') as rq
											where trim(lower(rq->>'Value')) = any(employee_pref_parameter_value)
											 and trim(lower(rq->>'Parameter'))='location'),',')
								);

							elseif parameter_priority = must_have 
							then 
								employee_failed_must_have := true;
								exit;
							else
								insert into score_breakups_table 
								values(
									requisition_parameters->>'Category'
									, 0
									,no_match
									,''
								);
							end if;

-- 						Checking for sub-industries values are stored in table RequisitionParameterValues under type sub-industry
						when lower(parameter_checking) = 'sub_industry' 
						then 
							if (
								select count(*)
								from jsonb_array_elements(requisition_details->'RequisitionParameterValues') as rq
								where trim(lower(rq->>'Value')) = lower(trim(employee_basic_parameter_value))
								and trim(lower(rq->>'Parameter'))='subindustry'
								) >= 1 
							then
								insert into score_breakups_table
								values(
									requisition_parameters->>'Category'
									,parameter_weightage
									,base_match
									,employee_basic_parameter_value
								);

							elseif parameter_priority = good_to_have
										and (select count(*)
											from jsonb_array_elements(requisition_details->'RequisitionParameterValues') as rq
											where trim(lower(rq->>'Value')) = any(employee_pref_parameter_value)
											 and trim(lower(rq->>'Parameter'))='subindustry'
										) >= 1 
							then
								insert into score_breakups_table 
								values(
									requisition_parameters->>'Category'
									,round(parameter_weightage * pref_weightage_constraint)
									,preferred_match
									,array_to_string((select ARRAY_AGG(rq->>'Value')
									from jsonb_array_elements(requisition_details->'RequisitionParameterValues') as rq
									where 
										trim(lower(rq->>'Value')) = any(employee_pref_parameter_value)
										and trim(lower(rq->>'Parameter'))='subindustry'),',')
								);

							elseif parameter_priority = must_have 
							then 
								employee_failed_must_have := true;
								exit;
							else
								insert into score_breakups_table
								values(
									requisition_parameters->>'Category'
									, 0
									,no_match
									,''
								);
							end if;

-- 						Checking for industry values are stored in table RequisitionParameterValues under type industry
						when lower(parameter_checking) = 'industry' 
						then 
							if (
								select count(*)
								from jsonb_array_elements(requisition_details->'RequisitionParameterValues') as rq
								where trim(lower(rq->>'Value')) = lower(trim(employee_basic_parameter_value)) 
								and trim(lower(rq->>'Parameter'))='industry'
								) >= 1 
							then
								insert into score_breakups_table
								values(
									requisition_parameters->>'Category'
									,parameter_weightage
									,base_match
									,employee_basic_parameter_value
								);

							elseif parameter_priority = good_to_have
										and (select count(*)
											from jsonb_array_elements(requisition_details->'RequisitionParameterValues') as rq
											where trim(lower(rq->>'Value')) = any(employee_pref_parameter_value)
											 and trim(lower(rq->>'Parameter'))='industry'
										) >= 1 
							then
								insert into score_breakups_table 
								values(
									requisition_parameters->>'Category'
									,round(parameter_weightage * pref_weightage_constraint)
									,preferred_match
									,array_to_string((select ARRAY_AGG(rq->>'Value')
											from jsonb_array_elements(requisition_details->'RequisitionParameterValues') as rq
											where trim(lower(rq->>'Value')) = any(employee_pref_parameter_value)
											 and trim(lower(rq->>'Parameter'))='industry'
										),',')
								);

							elseif parameter_priority = must_have 
							then 
								employee_failed_must_have := true;
								exit;
							else
								insert into score_breakups_table 
								values(
									requisition_parameters->>'Category'
									, 0
									,no_match
									,''
								);
							end if;
						else 
-- 							score_calculated := 0;
					END CASE;
				end loop;
		
-- 			Filter data 
			FOREACH param_value_pair IN ARRAY parameter_value_pairs
			LOOP
				checkParameterName := split_part(param_value_pair, ',', 1);
				checkParameterValue := CAST(split_part(param_value_pair, ',', 2) AS DECIMAL);			
				
				if((select value from score_breakups_table where trim(lower((parameter))) = trim(lower((checkParameterName)))) < checkParameterValue)
				then 
					filter_user := true;
				end if;
				
			END LOOP;

-- 			Extract score breakup in a jsonb element ---
-- 			If employee failed the must have condition, whole score will be 0 ---
			if(employee_failed_must_have)
				then
					score_calculated := 0 ;
			else
-- 				Store the score breakup into a object for each parameter for the employee and calculate total score ---
				FOR score_breakup_row IN
				SELECT * 
					FROM score_breakups_table
				LOOP
-- 	        		Constructing the JSONB object
					score_breakup := jsonb_agg(row_to_json(score_breakup_row)) || score_breakup;
				END LOOP;
				
				score_calculated := ROUND(((select sum(value) from score_breakups_table) :: decimal/max_score :: decimal)*100) ;
			end if;
			
			IF ((SELECT COUNT(*) FROM score_breakups_table) = 0)
				THEN
					score_breakup := '[]';
					score_calculated := 0;
			END IF;
		
-- 			Insert user to final list table ---
			if ( filter_user = false and ((working_in_same_project = true and cast (employees->>'available' as bool) = true) or (working_in_same_project = false)))
			then
				if score_calculated >= minimum_percentage_for_system_suggestions or ((cast (employees->>'interested' as bool)) = true)
				then
					insert into final_user_data 
					values(
						employees->>'empName',
						employees->>'email',
						employees->>'designation',
						employees->>'grade',
						employees->>'competency',
						employees->>'competencyId',
						employees->>'location',
						employees->>'supercoach',
						employees->>'revenueUnit',
						employees->>'business_unit',
						employees->>'sub_industry',
						employees->>'industry',
						(
							SELECT COALESCE(jsonb_agg(jsonb_build_object(
								'SkillName', uss.""skill_name"",
								'SkillCode', uss.""skill_code"",
								'Type', uss.""skill_type""
							)), '[]'::jsonb) -- Handle NULL result with empty array
							FROM matched_user_skill_temp_table uss
						),
						employees->>'pref_location',
						employees->>'pref_skill',
						employees->>'pref_industry',
						employees->>'sub_industry',
						score_breakup,
-- 						COALESCE(score_breakup, '[]'::jsonb),
						score_calculated,
						cast (employees->>'available' as bool),
						cast (employees->>'interested' as bool),
						free_from_days
					);
				end if;
			end if;
		end if;
	end loop;
	
-- 	Where condition 
	where_condition := generate_where_condition(filter ::jsonb);
	
	if(TRIM(where_condition) != '')
	then 
		where_condition = 'WHERE ' || where_condition;
	END IF;
	
--  Logic for pagination and offset ---
	EXECUTE 
    'SELECT jsonb_agg(row_to_json(final_data)) 
	FROM ('
    || 'SELECT * FROM final_user_data ' || where_condition || 
	' ORDER BY 
			final_user_data.available DESC,                  
    		final_user_data.score ' || order_score_by || ',                       
    		final_user_data.free_from_days DESC NULLS LAST
		offset (( ' || pagination || ' - 1) * '|| limit_count || ') 
		limit '||limit_count
	
    || ') 
	AS final_data'
	INTO var_resp;
	
-- 	Truncate everything from temp tables
	truncate table requisition_skill_temp_table;
	truncate table user_skills_temp_table;
	truncate table matched_user_skill_temp_table;
	truncate table score_breakups_table;
	
end;
$BODY$;
";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            drop procedure sp_system_suggestions;");
        }
    }
}
