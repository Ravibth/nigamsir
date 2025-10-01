using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics;
using WCGT.Domain.Entities;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ratedesignationviewupdate1911 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE OR REPLACE VIEW public.designationgraderateview
                         AS
                         SELECT DISTINCT ON (desig.designation_id) desig.designation_id,
                            desig.designation_name,
                            desig.description,
                            desig.isactive,
                            ratemaster.createdat,
                            ratemaster.modifiedat,
                            ratemaster.createdby,
                            ratemaster.modifiedby,
                            desig.grade,
                            ratemaster.""RatePerHour"",
	                        competency.""CompetencyName""
                         FROM ""Designations"" desig
                             JOIN ""RateDesignationMaster"" ratemaster 
	 	                        ON desig.grade = ratemaster.grade
	                         JOIN ""Competencies"" competency 
	 	                        ON LOWER(competency.""CompetencyId"") = LOWER(ratemaster.""CompetencyId"")
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