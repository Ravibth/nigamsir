using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class generate_where_condition_for_algo_filter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"CREATE OR REPLACE FUNCTION generate_where_condition(
    conditions JSONB
) RETURNS VARCHAR AS $$
DECLARE
    i INTEGER;
    condition JSONB;
    column_key TEXT;
    column_value TEXT;
    where_condition VARCHAR:= '';
BEGIN
    -- Loop through each JSONB object in the array
	for condition in 
		select * from jsonb_array_elements(conditions)
	Loop

        -- Extract column and value from JSONB object
        FOR column_key, column_value IN SELECT * FROM jsonb_each_text(condition) LOOP
			
            where_condition := where_condition || column_key || ' = ' || column_value || ' AND ';
        END LOOP;
    END LOOP;

    -- Remove the trailing 'AND' if any
    IF length(where_condition) > 0 THEN
        where_condition := substring(where_condition from 1 for length(where_condition) - 5);
    END IF;

    RETURN where_condition;
END;
$$ LANGUAGE plpgsql;

";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            drop FUNCTION generate_where_condition;");
        }
    }
}
