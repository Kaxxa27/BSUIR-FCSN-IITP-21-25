CREATE OR REPLACE FUNCTION check_values_comparison 
RETURN VARCHAR2 
IS
    v_even_count NUMBER := 0;
    v_odd_count NUMBER := 0;
BEGIN
    -- Count odd and even values
    SELECT 
        COUNT(CASE WHEN MOD(val, 2) = 0 THEN 1 END),
        COUNT(CASE WHEN MOD(val, 2) != 0 THEN 1 END)
    INTO v_even_count, v_odd_count
    FROM MyTable;
    
    -- Return comprasion result 
    IF v_even_count > v_odd_count THEN
        RETURN 'TRUE';
    ELSIF v_even_count < v_odd_count THEN
        RETURN 'FALSE';
    ELSE
        RETURN 'EQUAL';
    END IF;
END;
/
