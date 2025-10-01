using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class requpdatefunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE OR REPLACE FUNCTION FN_UpdateReqOnAlocUpdate() RETURNS trigger AS $TR_UpdateReqOnAlocUpdate$
    BEGIN
		Update ""Requisition""
		Set 
			""StartDate"" = NEW.""StartDate""
			, ""EndDate"" = NEW.""EndDate""   
			, ""TotalHours"" = NEW.""TotalEffort""   
			, ""EffortsPerDay"" = NEW.""TotalEffort""   
			, ""IsPerDayHourAllocation"" = false   
			, ""RequisitionStatus"" = 'Completed'
			, ""ModifiedAt"" = NEW.""ModifiedAt""   
			, ""ModifiedBy"" = NEW.""ModifiedBy"" 
		Where ""Id"" =NEW.""RequisitionId"";
		
		RETURN NEW;
   	END;
$TR_UpdateReqOnAlocUpdate$ LANGUAGE plpgsql;
 ";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop FUNCTION FN_UpdateReqOnAlocUpdate;");
        }
    }
}
