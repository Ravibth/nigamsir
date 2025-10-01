using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class requpdatetrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE TRIGGER TR_UpdateReqOnAlocInsert
AFTER INSERT ON ""PublishedResAllocDetails""
FOR EACH ROW
EXECUTE FUNCTION FN_UpdateReqOnAlocUpdate();";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop TRIGGER TR_UpdateReqOnAlocInsert;");
        }
    }
}
