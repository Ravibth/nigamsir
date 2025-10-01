using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fun_allocationday_view : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"create or replace FUNCTION public.fun_allocationday_view(timeoptionname text,injobcode text,startDate date,endDate date)
            returns TABLE  (monthname Timestamp   , totaltime bigint,Designation text )
            AS  $$

            BEGIN
             IF timeoptionname = 'weekly' THEN
              RETURN QUERY SELECT DATE_TRUNC('week',""ConfirmedAllocationStartDate"") AS monthname, SUM(""ConfirmedPerDayHours""),""Designation""
  					FROM public.""allocationdaysreqview""
					where ""JobCode""=injobcode AND ""ConfirmedAllocationStartDate"" >= startDate 
					AND ""ConfirmedAllocationStartDate"" <= endDate AND ""IsActive"" = true
  					GROUP BY ""Designation"",monthname;
             ELSIF timeoptionname = 'monthly'  THEN
                 RETURN QUERY SELECT DATE_TRUNC('month',""ConfirmedAllocationStartDate"") AS monthname, SUM(""ConfirmedPerDayHours""),""Designation""
  					FROM public.""allocationdaysreqview"" 
					where ""JobCode""=injobcode AND ""ConfirmedAllocationStartDate"" >= startDate 
					AND ""ConfirmedAllocationStartDate"" <= endDate AND ""IsActive"" = true
  					GROUP BY ""Designation"",monthname;
	         ELSIF timeoptionname = 'daily'  THEN
              RETURN QUERY SELECT DATE_TRUNC('day',""ConfirmedAllocationStartDate"") AS monthname, SUM(""ConfirmedPerDayHours""),""Designation""
  					FROM public.""allocationdaysreqview"" 
					where ""JobCode""=injobcode AND ""ConfirmedAllocationStartDate"" >= startDate 
					AND ""ConfirmedAllocationStartDate"" <= endDate AND ""IsActive"" = true
  					GROUP BY ""Designation"",monthname;
                END IF;
            END;
            $$
            LANGUAGE plpgsql";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            drop function fun_allocationday_view;");
        }
    }
}
