using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class allocationresourcefunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @" create or replace FUNCTION public.fun_allocationday_resource_view(start_date  date,end_date date,injobcode text)
                 returns TABLE  (EmpEmail text, EmpName text , totaltime bigint )
            AS  $$

            BEGIN
                RETURN QUERY 			 
					SELECT ""EmpEmail"",""EmpName"",  SUM(""ConfirmedPerDayHours"") as totaltime
  					FROM public.""ResourceAllocationDays""
					where ""JobCode""=injobcode AND ""ConfirmedAllocationStartDate"" >= start_date AND 
					""ConfirmedAllocationStartDate"" <= end_date
					AND ""IsActive""=true
  					GROUP BY ""EmpEmail"",""EmpName"";
               
                END;
                 $$
                LANGUAGE plpgsql;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP FUNCTION fun_allocationday_view;";
            migrationBuilder.Sql(sp);
        }
    }
}
