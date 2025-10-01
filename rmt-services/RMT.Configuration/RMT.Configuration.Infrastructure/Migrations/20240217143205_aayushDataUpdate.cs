using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class aayushDataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlQuery = @"update ""MenuMaster"" 
                            set ""InternalName""='User Management' , ""DisplayName""='User Management' 
                            where 
                            ""InternalName"" ilike 'User Onboarding' or ""DisplayName"" ilike 'User Onboarding';";
            migrationBuilder.Sql(sqlQuery);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //Not required
        }
    }
}
