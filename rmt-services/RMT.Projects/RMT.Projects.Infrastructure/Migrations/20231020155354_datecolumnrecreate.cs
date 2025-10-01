using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class datecolumnrecreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<DateTime>(
               name: "ModifiedAt",
               table: "ProjectSkills",
               type: "timestamp with time zone",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<DateTime>(
               name: "CreatedAt",
               table: "ProjectSkills",
               type: "timestamp with time zone",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<DateTime>(
               name: "ModifiedAt",
               table: "Projects",
               type: "timestamp with time zone",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<DateTime>(
               name: "CreatedAt",
               table: "Projects",
               type: "timestamp with time zone",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<DateTime>(
               name: "ModifiedAt",
               table: "ProjectRoles",
               type: "timestamp with time zone",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<DateTime>(
               name: "CreatedAt",
               table: "ProjectRoles",
               type: "timestamp with time zone",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<DateTime>(
               name: "ModifiedAt",
               table: "ProjectJobCodes",
               type: "timestamp with time zone",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<DateTime>(
               name: "CreatedAt",
               table: "ProjectJobCodes",
               type: "timestamp with time zone",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<DateTime>(
               name: "ModifiedAt",
               table: "ProjectDemands",
               type: "timestamp with time zone",
               rowVersion: true,
               nullable: true);


            migrationBuilder.AddColumn<DateTime>(
               name: "CreatedAt",
               table: "ProjectDemands",
               type: "timestamp with time zone",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<DateTime>(
              name: "ModifiedAt",
              table: "ProjectDemandSkills",
              type: "timestamp with time zone",
              rowVersion: true,
              nullable: true);

            migrationBuilder.AddColumn<DateTime>(
               name: "CreatedAt",
               table: "ProjectDemandSkills",
               type: "timestamp with time zone",
               rowVersion: true,
               nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                                     name: "ModifiedAt",
                                     table: "ProjectSkills");

            migrationBuilder.DropColumn(
                                     name: "CreatedAt",
                                     table: "ProjectSkills");

            migrationBuilder.DropColumn(
                                     name: "ModifiedAt",
                                     table: "Projects");

            migrationBuilder.DropColumn(
                                     name: "CreatedAt",
                                     table: "Projects");

            migrationBuilder.DropColumn(
                                     name: "ModifiedAt",
                                     table: "ProjectRoles");

            migrationBuilder.DropColumn(
                                     name: "CreatedAt",
                                     table: "ProjectRoles");

            migrationBuilder.DropColumn(
                                     name: "ModifiedAt",
                                     table: "ProjectJobCodes");

            migrationBuilder.DropColumn(
                                     name: "CreatedAt",
                                     table: "ProjectJobCodes");

            migrationBuilder.DropColumn(
                                     name: "ModifiedAt",
                                     table: "ProjectDemands");

            migrationBuilder.DropColumn(
                                     name: "CreatedAt",
                                     table: "ProjectDemands");

            migrationBuilder.DropColumn(
                                  name: "ModifiedAt",
                                  table: "ProjectDemandSkills");

            migrationBuilder.DropColumn(
                                     name: "CreatedAt",
                                     table: "ProjectDemandSkills");

        }
    }
}
