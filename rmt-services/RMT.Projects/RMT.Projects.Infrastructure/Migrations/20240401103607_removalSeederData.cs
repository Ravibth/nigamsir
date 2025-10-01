using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removalSeederData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // var sql = @"
            //             truncate table ""MasterValues"" cascade;
            //             truncate table ""ProjectDemands"" cascade;
            //             truncate table ""ProjectRoles"" cascade;
            //             truncate table ""ProjectSkills"" cascade;
            //             truncate table ""Projects"" cascade;
            //             ";
            // migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
