CREATE OR REPLACE FUNCTION count_weekends_between_dates(start_date DATE, end_date DATE)
RETURNS INTEGER AS $$
DECLARE
    curr_date DATE := start_date;
    weekend_count INTEGER := 0;
BEGIN
    WHILE curr_date <= end_date LOOP
        -- Check if curr_date is a Saturday or Sunday
        IF EXTRACT(ISODOW FROM curr_date) IN (6, 7) THEN
            weekend_count := weekend_count + 1;
        END IF;
        
        -- Move to the next day
        curr_date := curr_date + INTERVAL '1 day';
    END LOOP;

    RETURN weekend_count;
END;
$$ LANGUAGE plpgsql;