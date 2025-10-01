using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class designationgraderatev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE OR REPLACE VIEW DesignationGradeRateView AS
                            SELECT DISTINCT ON (desig.designation_id) 
                                desig.designation_id, 
                                desig.designation_name, 
                                desig.description, 
                                desig.isactive, 
                                ratemaster.createdat, 
                                ratemaster.modifiedat, 
                                ratemaster.createdby, 
                                ratemaster.modifiedby, 
                                desig.grade,
                                ratemaster.""RatePerHour""
                            FROM ""Designations"" desig 
                            INNER JOIN ""RateDesignationMaster"" ratemaster
                            ON desig.grade = ratemaster.grade
                            ORDER BY desig.designation_id;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" drop view ""DesignationGradeRateView"";");
        }
    }
}
