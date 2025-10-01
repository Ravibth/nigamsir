CREATE OR REPLACE FUNCTION refresh_materialized_views_function(view_names text[])
RETURNS void AS $$
DECLARE
    view_name text;
    view_exists boolean;
BEGIN
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
        END IF;
    END LOOP;
END;
$$ LANGUAGE plpgsql;

-- Example list of materialized views to refresh
-- select refresh_materialized_views_function(ARRAY['employee_allocation_timesheet', 'employee_working_days', 'project_budget'])