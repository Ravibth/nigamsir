using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ratedesignationviewupdate19112 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE OR REPLACE VIEW public.designationgraderateview
                         AS
                         SELECT DISTINCT ON (desig.designation_id, competency.""CompetencyId"") desig.designation_id,
                            desig.designation_name,
                            desig.description,
                            desig.isactive,
                            ratemaster.createdat,
                            ratemaster.modifiedat,
                            ratemaster.createdby,
                            ratemaster.modifiedby,
                            desig.grade,
                            ratemaster.""RatePerHour"",
                            competency.""CompetencyName"",
                            competency.""CompetencyId""
                           FROM ""Designations"" desig
                             JOIN ""RateDesignationMaster"" ratemaster ON desig.grade = ratemaster.grade
                             JOIN ""Competencies"" competency ON lower(competency.""CompetencyId"") = lower(ratemaster.""CompetencyId"")
                          ORDER BY desig.designation_id; ";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" drop view ""DesignationGradeRateView"";");

        }
    }
}
