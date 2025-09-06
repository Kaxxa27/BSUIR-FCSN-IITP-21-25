CREATE OR REPLACE FUNCTION generate_insert_statement(v_id NUMBER) 
RETURN VARCHAR2 
IS
    v_insert_statement VARCHAR2(300);
BEGIN
    v_insert_statement := 'INSERT INTO MyTable (id, val) VALUES (' || v_id || ', ' || ROUND(DBMS_RANDOM.VALUE(1, 10000)) || ');';
    RETURN v_insert_statement;
END;
/

DECLARE
    v_insert_cmd VARCHAR2(300);
BEGIN
    v_insert_cmd := generate_insert_statement(777);
    DBMS_OUTPUT.PUT_LINE(v_insert_cmd);
END;
/