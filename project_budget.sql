 DROP INDEX IF EXISTS project_budget_index;

 DROP MATERIALIZED VIEW IF EXISTS public.project_budget;

 CREATE MATERIALIZED VIEW IF NOT EXISTS public.project_budget
 TABLESPACE pg_default
 AS
  WITH allocation AS (
          WITH allocation AS (
                  Select allocation.pipeline_code,
                     allocation.job_code,
                     allocation."user",
                     allocation.email_id,
                     allocation.hour_per_day,
                     allocation.allocation_count,
                     allocation.allocation_date
                    FROM dblink('host=localhost port=5432 user=postgres password=root dbname=Allocation'::text, '
                      Select  "PipelineCode" "pipeline_code","JobCode" "job_code", 
                      "EmailId" "user", split_part("EmailId", ''__'', 2) "email_id", 
                      SUM("RatePerHour") "hour_per_day", COUNT(*) "allocation_count", "AllocationDate"::Date "allocation_date" 
                      From "PublishedResAllocDays" 
                      Group by "AllocationDate"::Date, "PipelineCode","JobCode", "EmailId"
                      '::text) allocation(pipeline_code text, job_code text, "user" text, email_id text, hour_per_day numeric, allocation_count numeric, allocation_date date)
                 ), requisition AS (
                  Select allocation.pipeline_code,
                     allocation.job_code,
                     allocation.requisition_count
                    FROM dblink('host=localhost port=5432 user=postgres password=root dbname=Allocation'::text, '
                      Select  "PipelineCode" "pipeline_code","JobCode" "job_code", count(*) "requisition_count"
                      From "Requisition"
                      Where "RequisitionTypeId" IN (3,6) and "RequisitionStatus"  IN (''Pending'')
                      Group by  "PipelineCode","JobCode"
                      '::text) allocation(pipeline_code text, job_code text, requisition_count numeric)
                 ), employee_designation_rate AS (
                  Select designation_rate.email_id,
                     designation_rate.rate_per_hour,
                     designation_rate.designation
                    FROM dblink('host=localhost port=5432 user=postgres password=root dbname=WCGT'::text, '
                      Select "emp"."email_id" "email_id", COALESCE("rdm"."RatePerHour",0) "rate_per_hour", "dd"."designation_name" "designation"
                      From "Employees" "emp"
                      left join  "Designations"  "dd" on "dd"."designation_id" = "emp"."designation_id"
                      left join "RateDesignationMaster"  "rdm" on "rdm"."CompetencyId" = "emp"."CompetencyId"
                      '::text) designation_rate(email_id text, rate_per_hour numeric, designation text)
                 )
          Select "all".pipeline_code,
             "all".job_code,
             sum(COALESCE(edr.rate_per_hour, 0::numeric) * COALESCE("all".hour_per_day, 0::numeric)) AS allocated_cost,
             sum(COALESCE("all".hour_per_day, 0::numeric)) AS allocated_hours,
             sum(COALESCE(req.requisition_count, 0::numeric)) AS requisition_count,
             sum(COALESCE("all".allocation_count, 0::numeric)) AS allocation_count
            FROM allocation "all"
              LEFT JOIN employee_designation_rate edr ON "all".email_id = edr.email_id
              LEFT JOIN requisition req ON "all".pipeline_code = "all".pipeline_code
           GROUP BY "all".pipeline_code, "all".job_code
         ), jobfee AS (
          Select timesheet.pipeline_code,
             timesheet.job_code,
             timesheet.billing_currency,
             timesheet.job_fee
            FROM dblink('host=localhost port=5432 user=postgres password=root dbname=WCGT'::text, '
               Select "pipeline_code", "job_code", "billing_currency", "agreedJobFee" "job_fee" From "Jobs"
               '::text) timesheet(pipeline_code text, job_code text, billing_currency text, job_fee numeric)
         ), timesheet AS (
          Select timesheet.job_code,
             timesheet.actual_cost,
             timesheet.actual_log_hours
            FROM dblink('host=localhost port=5432 user=postgres password=root dbname=WCGT'::text, '
               Select "jobcode" "job_code" ,
               SUM(COALESCE("rate",0) * COALESCE("totaltime",0)) "actual_cost", SUM(COALESCE("totaltime",0)) "actual_log_hours" From "Timesheet"
               Group by "jobcode"
               '::text) timesheet(job_code text, actual_cost numeric, actual_log_hours numeric)
         ), project_budget AS (
          Select project_budget.project_code,
             project_budget.client_name,
             project_budget.business_unit,
             project_budget.location,
             project_budget.pipeline_code,
             project_budget.job_code,
             project_budget.sme,
             project_budget.start_date,
             project_budget.end_date,
             project_budget.expertise,
             project_budget.job_name,
             project_budget.pipeline_name,
             project_budget.csp_user_name,
             project_budget.csp_user,
             project_budget.el_user_name,
             project_budget.el_user,
             project_budget.project_rate_per_hour,
             project_budget.budget_hours,
             project_budget.total_budget
            FROM dblink('host=localhost port=5432 user=postgres password=root dbname=Project'::text, '

               with roles as  (
               Select * From (
               Select
               "pr"."ProjectId" "project_id",
               CASE WHEN "pr"."ApplicationRole" = ''ResourceRequestor''  THEN "pr"."UserName" ELSE null END as "csp_user_name" ,
               CASE WHEN "pr"."ApplicationRole" = ''ResourceRequestor'' THEN "pr"."User" ELSE null END as "csp_user" ,
               CASE WHEN "pr"."ApplicationRole" = ''Reviewer'' THEN "pr"."UserName" ELSE null END as "el_user_name" ,
               CASE WHEN "pr"."ApplicationRole" = ''Reviewer'' THEN "pr"."User" ELSE null END as "el_user" 
               From "ProjectRoles" "pr" Where "pr"."IsActive" = true ) as PR2
               Group by "project_id", "csp_user_name","csp_user","el_user_name","el_user"
               )

               Select "pj"."Id" "project_code", 
               "pj"."ClientName" "client_name",
               "pj"."bu" "business_unit",
               "pj"."Location" "location",
               "pj"."PipelineCode" "pipeline_code",
               "pj"."JobCode" "job_code",
               "pj"."Sme" "sme",
               "pj"."StartDate"::Date "start_date",
               "pj"."EndDate"::Date "end_date",
               "pj"."Expertise" "expertise",
               "pj"."JobName" "job_name",
               "pj"."PipelineName" "pipeline_name",
               "roles"."csp_user_name" "csp_user_name",
               "roles"."csp_user" "csp_user",
               "roles"."el_user_name" "el_user_name",
               "roles"."el_user" "el_user",
               SUM(COALESCE("pb"."RatePerHour",0)) "project_rate_per_hour", 
               SUM(COALESCE("pb"."BudgetedHour",0)) "budget_hours",
               SUM((COALESCE("pb"."RatePerHour",0) * COALESCE("pb"."BudgetedHour",0))) as "total_budget"
               From "Projects" "pj"
               left join "ProjectBudget" "pb" on "pb"."ProjectId" = "pj"."Id"
               left join "roles" "roles" on "roles"."project_id" = "pj"."Id"
               group by
               "pj"."bu", "pj"."Location", "pj"."ClientName", "pj"."Id", "pj"."JobName", "pj"."PipelineName", "pj"."PipelineCode", "pj"."JobCode", "pj"."Sme",
               "pj"."Expertise",
               "roles"."csp_user_name",
               "roles"."csp_user",
               "roles"."el_user_name",
               "roles"."el_user",
               "pj"."StartDate"::Date,
               "pj"."EndDate"::Date
               '::text) project_budget(project_code text, client_name text, business_unit text, location text, pipeline_code text, job_code text, sme text, start_date date, end_date date, expertise text, job_name text, pipeline_name text, csp_user_name text, csp_user text, el_user_name text, el_user text, project_rate_per_hour numeric, budget_hours numeric, total_budget numeric)
         )
  Select t2.project_code,
     t2.client_name,
     t2.business_unit,
     t2.location,
     t2.pipeline_code,
     t2.job_code,
     t2.sme,
     t2.start_date,
     t2.end_date,
     t2.expertise,
     t2.job_name,
     t2.pipeline_name,
     t2.csp_user_name,
     t2.csp_user,
     t2.el_user_name,
     t2.el_user,
     t2.project_rate_per_hour,
     t2.budget_hours,
     t2.total_budget,
     t2.requisition_count,
     t2.allocation_count,
     t2.job_fee,
     t2.allocated_consumption_percent,
     t2.actual_consumption_percent,
     t2.allocated_cost,
     t2.allocated_hours,
     t2.actual_cost,
     t2.actual_log_hours,
     t2.progress,
     t2.elapsed_allocation,
     t2.allocated_cost - t2.elapsed_allocation AS future_cost,
     t2.allocated_cost - t2.elapsed_allocation + t2.actual_cost AS expected_expense,
     t2.total_budget - (t2.allocated_cost - t2.elapsed_allocation + t2.actual_cost) AS overall_run
    FROM ( Select pb.project_code,
             pb.client_name,
             pb.business_unit,
             pb.location,
             pb.pipeline_code,
             pb.job_code,
             pb.sme,
             pb.start_date,
             pb.end_date,
             pb.expertise,
             pb.job_name,
             pb.pipeline_name,
             pb.csp_user_name,
             pb.csp_user,
             pb.el_user_name,
             pb.el_user,
             pb.project_rate_per_hour,
             pb.budget_hours,
             pb.total_budget,
             COALESCE("all".requisition_count, 0::numeric) AS requisition_count,
             COALESCE("all".allocation_count, 0::numeric) AS allocation_count,
             COALESCE(job.job_fee, 0::numeric) AS job_fee,
                 CASE
                     WHEN pb.total_budget <> 0::numeric THEN round(COALESCE("all".allocated_cost, 0::numeric) / COALESCE(pb.total_budget, 0::numeric) * 100::numeric, 0)
                     ELSE 0::numeric
                 END AS allocated_consumption_percent,
                 CASE
                     WHEN pb.total_budget <> 0::numeric THEN round(COALESCE("time".actual_cost, 0::numeric) / COALESCE(pb.total_budget, 0::numeric) * 100::numeric, 0)
                     ELSE 0::numeric
                 END AS actual_consumption_percent,
             COALESCE("all".allocated_cost, 0::numeric) AS allocated_cost,
             COALESCE("all".allocated_hours, 0::numeric) AS allocated_hours,
             COALESCE("time".actual_cost, 0::numeric) AS actual_cost,
             COALESCE("time".actual_log_hours, 0::numeric) AS actual_log_hours,
                 CASE
                     WHEN pb.budget_hours <> 0::numeric THEN round(COALESCE("time".actual_log_hours, 0::numeric) / COALESCE(pb.budget_hours, 0::numeric), 2)
                     ELSE 0::numeric
                 END AS progress,
             COALESCE("all".allocated_cost, 0::numeric) *
                 CASE
                     WHEN pb.budget_hours <> 0::numeric THEN round(COALESCE("time".actual_log_hours, 0::numeric) / COALESCE(pb.budget_hours, 0::numeric), 2)
                     ELSE 0::numeric
                 END AS elapsed_allocation
            FROM project_budget pb
              LEFT JOIN timesheet "time" ON "time".job_code = pb.job_code
              LEFT JOIN allocation "all" ON "all".job_code = pb.job_code AND "all".pipeline_code = pb.pipeline_code
              LEFT JOIN jobfee job ON job.job_code = pb.job_code AND job.pipeline_code = pb.pipeline_code) t2
 WITH DATA;