CREATE OR REPLACE PROCEDURE delete_row(
    p_id NUMBER
) 
IS
BEGIN
    DELETE FROM MyTable WHERE id = p_id;
    DBMS_OUTPUT.PUT_LINE('Строка с ID ' || p_id || ' была успешно удалена.');
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('Строка с ID ' || p_id || ' не найдена.');
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Ошибка при удалении строки с ID ' || p_id || ': ' || SQLERRM);
END;
/

DECLARE
    v_id NUMBER := 999;
BEGIN
    delete_row(v_id);
END;
/

SELECT * FROM MyTable;
