DROP TRIGGER check_unique_id_students;
CREATE OR REPLACE TRIGGER check_unique_id_students
FOR INSERT OR UPDATE ON Students
COMPOUND TRIGGER

-- Таблица для хранения имен групп, которые должны быть вставлены или обновлены
TYPE t_students_id IS TABLE OF NUMBER INDEX BY PLS_INTEGER;
v_students_id t_students_id;

BEFORE STATEMENT IS
BEGIN
    v_students_id.DELETE;
END BEFORE STATEMENT;

BEFORE EACH ROW IS
BEGIN
    IF INSERTING OR UPDATING THEN
        v_students_id(v_students_id.COUNT + 1) := :NEW.ID;
    END IF;  
    FOR i IN 1 .. v_students_id.COUNT 
    LOOP
        SELECT COUNT(*)
        INTO v_id_count
        FROM Students
        WHERE id = v_students_id(i);
        
        DBMS_OUTPUT.PUT_LINE('Count row ' || v_id_count);
        IF v_id_count <> 1 THEN
            RAISE_APPLICATION_ERROR(-20001, 'This student id exists: ' || v_students_id(i));
        END IF;
    END LOOP;
END AFTER STATEMENT;
END check_unique_id_students;
/

----DROP TRIGGER check_unique_id_students;
--CREATE OR REPLACE TRIGGER check_unique_id_students
--BEFORE INSERT ON Students
--FOR EACH ROW
--DECLARE
--    id_count NUMBER;
--BEGIN
--    SELECT COUNT(*)
--    INTO id_count
--    FROM Students
--    WHERE ID = :NEW.ID;
--
--    IF id_count > 0 THEN
--        RAISE_APPLICATION_ERROR(-20001, 'ID должен быть уникальным в таблице Students');
--    END IF;
--
--END;
--/
--DROP TRIGGER check_unique_id_groups;
CREATE OR REPLACE TRIGGER check_unique_id_groups
BEFORE INSERT ON St_Groups
FOR EACH ROW
DECLARE
    id_count NUMBER;
BEGIN
    SELECT COUNT(*)
    INTO id_count
    FROM St_Groups
    WHERE ID = :NEW.ID;

    IF id_count > 0 THEN
        RAISE_APPLICATION_ERROR(-20002, 'ID должен быть уникальным в таблице St_Groups');
    END IF;
END;

