using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class disabled_revenue_unit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"Update ""ConfigurationGroupMasters"" set ""IsActive"" = false where ""Id"" in (14,33)";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp = @"Update ""ConfigurationGroupMasters"" set ""IsActive"" = true where ""Id"" in (14,33)";
            migrationBuilder.Sql(sp);
        }
    }
}
