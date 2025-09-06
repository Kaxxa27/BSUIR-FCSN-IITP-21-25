CREATE OR REPLACE PROCEDURE insert_row(
    p_id NUMBER,
    p_val NUMBER
) 
IS
BEGIN
    INSERT INTO MyTable (id, val) VALUES (p_id, p_val);
    DBMS_OUTPUT.PUT_LINE('Строка с ID ' || p_id || ' была успешно вставлена.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Ошибка при вставке строки с ID ' || p_id || ': ' || SQLERRM);
END;
/

DECLARE
    v_id NUMBER := 44;
    v_value NUMBER := 888;
BEGIN
    insert_row(v_id, v_value);
END;
/

SELECT * FROM MyTable;
