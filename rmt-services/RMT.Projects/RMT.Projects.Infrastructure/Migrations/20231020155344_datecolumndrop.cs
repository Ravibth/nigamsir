using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class datecolumndrop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<byte[]>(
               name: "ModifiedAt",
               table: "Projects",
               type: "bytea",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<byte[]>(
               name: "CreatedAt",
               table: "Projects",
               type: "bytea",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<byte[]>(
               name: "ModifiedAt",
               table: "ProjectRoles",
               type: "bytea",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<byte[]>(
               name: "CreatedAt",
               table: "ProjectRoles",
               type: "bytea",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<byte[]>(
               name: "ModifiedAt",
               table: "ProjectJobCodes",
               type: "bytea",
               rowVersion: true,
               nullable: true);


            migrationBuilder.AddColumn<byte[]>(
               name: "CreatedAt",
               table: "ProjectJobCodes",
               type: "bytea",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<byte[]>(
               name: "ModifiedAt",
               table: "ProjectDemands",
               type: "bytea",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<byte[]>(
               name: "CreatedAt",
               table: "ProjectDemands",
               type: "bytea",
               rowVersion: true,
               nullable: true);

            migrationBuilder.AddColumn<byte[]>(
             name: "ModifiedAt",
             table: "ProjectDemandSkills",
             type: "bytea",
             rowVersion: true,
             nullable: true);

            migrationBuilder.AddColumn<byte[]>(
               name: "CreatedAt",
               table: "ProjectDemandSkills",
               type: "bytea",
               rowVersion: true,
               nullable: true);

        }
    }
}
