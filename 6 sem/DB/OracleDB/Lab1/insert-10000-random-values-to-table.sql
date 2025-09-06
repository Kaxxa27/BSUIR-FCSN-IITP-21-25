-- Anonymous block to write 10,000 random records to the MyTable
DECLARE
    v_counter NUMBER := 1;
BEGIN
    WHILE v_counter <= 10
    LOOP
        INSERT INTO MyTable (id, val)
        VALUES (v_counter, ROUND(DBMS_RANDOM.VALUE(1, 10)));
        v_counter := v_counter + 1;
    END LOOP;
END;