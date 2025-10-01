using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class allocationdayfunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"create or replace FUNCTION public.fun_allocationday_view(timeoptionname text,injobcode text)
            returns TABLE  (monthname Timestamp   , totaltime bigint )
            AS  $$

            BEGIN
             IF timeoptionname = 'weekly' THEN
              RETURN QUERY SELECT DATE_TRUNC('week',""ConfirmedAllocationStartDate"") AS monthname, SUM(""ConfirmedPerDayHours"")
  					FROM public.""ResourceAllocationDays""
					where ""JobCode""=injobcode
  					GROUP BY monthname;
             ELSIF timeoptionname = 'monthly'  THEN
                 RETURN QUERY SELECT DATE_TRUNC('month',""ConfirmedAllocationStartDate"") AS monthname, SUM(""ConfirmedPerDayHours"")
  					FROM public.""ResourceAllocationDays""
					where ""JobCode""=injobcode
  					GROUP BY monthname;
	         ELSIF timeoptionname = 'daily'  THEN
              RETURN QUERY SELECT DATE_TRUNC('day',""ConfirmedAllocationStartDate"") AS monthname, SUM(""ConfirmedPerDayHours"")
  					FROM public.""ResourceAllocationDays""
					where ""JobCode""=injobcode
  					GROUP BY monthname;
                END IF;
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
