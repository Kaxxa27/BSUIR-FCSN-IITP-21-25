CREATE OR REPLACE PROCEDURE update_row(
    p_id NUMBER,
    p_new_val NUMBER
) 
IS
BEGIN
    UPDATE MyTable SET val = p_new_val WHERE id = p_id;
    DBMS_OUTPUT.PUT_LINE('Значение столбца val для строки с ID ' || p_id || ' было успешно обновлено.');
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('Строка с ID ' || p_id || ' не найдена.');
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Ошибка при обновлении строки с ID ' || p_id || ': ' || SQLERRM);
END;
/

DECLARE
    v_id NUMBER := 44;
    v_value NUMBER := 666;
BEGIN
    update_row(v_id, v_value);
END;
/

SELECT * FROM MyTable;
