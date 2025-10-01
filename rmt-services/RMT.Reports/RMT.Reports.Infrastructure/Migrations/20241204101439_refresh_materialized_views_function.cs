using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Reports.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class refresh_materialized_views_function : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @$"
CREATE OR REPLACE FUNCTION refresh_materialized_views_function(
	view_names text[])
    RETURNS text
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$

DECLARE
    returnMsg text;
    view_name text;
    view_exists boolean;
BEGIN
	returnMsg = '';
    -- Iterate through each materialized view name in the provided array
    FOREACH view_name IN ARRAY view_names LOOP
        -- Check if the materialized view exists in the current database
        SELECT EXISTS (
            SELECT 1
            FROM pg_matviews
            WHERE matviewname = view_name
        ) INTO view_exists;

        IF view_exists THEN
            -- If the materialized view exists, refresh it
            EXECUTE 'REFRESH MATERIALIZED VIEW ' || quote_ident(view_name);
			returnMsg = returnMsg || 'Executed-[' || view_name || '];';
        END IF;
    END LOOP;
	return returnMsg;
END;
$BODY$;
";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS refresh_materialized_views_function(text[])");
        }
    }
}
