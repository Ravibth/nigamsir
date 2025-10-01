using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modify_sp_allocationday_view : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"
                CREATE OR REPLACE PROCEDURE public.sp_allocationday_view(
	                IN timeoptionname text,
	                IN inpipelinecode text,
	                IN injobcode text,
	                IN startdate date,
	                IN enddate date,
	                OUT response jsonb)
                LANGUAGE 'plpgsql'
                AS $BODY$

                            declare 
                                employees_availability_row Record;
	                            chartviewoption TEXT;
                            begin 
                                DROP table IF EXISTS temp_allocation;
                                create temp table temp_allocation(
                                    monthname timestamp without time zone,                    
                                    designation text,
		                            grade text,
					                totaltime bigint,
                                    cost double precision
                                );
                                BEGIN 
                                IF timeoptionname = 'weekly' 
                                    THEN
			                            chartviewoption = 'week';
                                ELSIF timeoptionname = 'monthly'
                                    THEN
			                            chartviewoption = 'month';
                                ELSIF timeoptionname = 'daily'
                                    THEN
			                            chartviewoption = 'day';
                                    END IF;
                                END;

				                INSERT INTO temp_allocation
                                SELECT 
                                    DATE_TRUNC(chartviewoption, v.""AllocationDate"") AS monthname,
					                v.""Designation"",
                                    v.""Grade"",
                                    SUM(v.""Efforts""),                   
                                    SUM(allocationdays.""RatePerHour"" * v.""Efforts"") AS cost
                                FROM public.""allocationdaysreqview"" as v
                                INNER JOIN ""PublishedResAllocDays"" as allocationdays 
                                    ON allocationdays.""RequisitionId"" = v.""RequisitionId""
					                AND allocationdays.""AllocationDate"" = v.""AllocationDate""
                                INNER JOIN ""PublishedResAlloc"" as allocation 
                                    ON allocation.""RequisitionId"" = v.""RequisitionId""
                                INNER JOIN ""PublishedResAllocDetails"" as details 
                                    ON details.""RequisitionId"" = v.""RequisitionId""
                                JOIN ""Requisition"" req 
                                    ON req.""Id"" = v.""RequisitionId""
                                where 
                                    lower(req.""JobCode"") = lower(injobcode)
                                    AND lower(req.""PipelineCode"") = lower(inpipelinecode)
                                    AND v.""AllocationDate"" >= startdate
                                    AND v.""AllocationDate"" <= enddate
                                    AND v.""IsActive"" = true
                                    AND details.""IsActive"" = true
                                    AND lower(details.""AllocationStatus"") IN (
                                        'employee allocation accepted by reviewer',
						                 'employee allocation accepted by supercoach',
                                        'employee allocation accepted by employee',
                                        'employee allocation supercoach accepted resource requestor rejected employee rejection'
                                    )
                                GROUP BY 
				                    monthname,
                                    v.""Designation"",
                                    v.""Grade"";    	
                                select to_jsonb(json_agg(temp_allocation.*)) 
                                into response
                                from temp_allocation;	
                            end 
            
                $BODY$;";
            migrationBuilder.Sql(sp);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            drop procedure sp_allocationday_view;");
        }
    }
}
