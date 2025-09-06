DROP TRIGGER check_group_name;
CREATE OR REPLACE TRIGGER check_group_name
FOR INSERT OR UPDATE ON St_Groups
COMPOUND TRIGGER

-- “аблица дл€ хранени€ имен групп, которые должны быть вставлены или обновлены
TYPE t_group_names IS TABLE OF VARCHAR2(100) INDEX BY PLS_INTEGER;
v_group_names t_group_names;

BEFORE STATEMENT IS
BEGIN
    v_group_names.DELETE;
END BEFORE STATEMENT;

BEFORE EACH ROW IS
BEGIN
    IF INSERTING OR UPDATING THEN
        v_group_names(v_group_names.COUNT + 1) := :NEW.name;
    END IF;
END BEFORE EACH ROW;

AFTER STATEMENT IS
    v_name_count NUMBER;
BEGIN
    FOR i IN 1 .. v_group_names.COUNT 
    LOOP
        SELECT COUNT(*)
        INTO v_name_count
        FROM St_Groups
        WHERE name = v_group_names(i);
        
        DBMS_OUTPUT.PUT_LINE('Count row ' || v_name_count);
        IF v_name_count <> 1 THEN
            RAISE_APPLICATION_ERROR(-20001, 'This group name exists: ' || v_group_names(i));
        END IF;
    END LOOP;
END AFTER STATEMENT;
END check_group_name;
/
