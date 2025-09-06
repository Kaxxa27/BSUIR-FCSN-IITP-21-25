/*
DROP SEQUENCE Students_SEQ;
CREATE SEQUENCE Students_SEQ
START WITH 1
INCREMENT BY 1;
*/

--DROP TRIGGER generate_student_id;
CREATE OR REPLACE TRIGGER generate_student_id
BEFORE INSERT ON Students
FOR EACH ROW
DECLARE 
    max_id NUMBER;
BEGIN
    IF :NEW.ID IS NULL THEN
        SELECT MAX(ID)
        INTO max_id
        FROM Students;
        
        IF max_id IS NOT NULL THEN
            :NEW.ID := max_id + 1;
        ELSE 
            :NEW.ID := 1;
        END IF;
    END IF;
END;
/

/*
DROP SEQUENCE Groups_SEQ;
CREATE SEQUENCE Groups_SEQ
START WITH 1
INCREMENT BY 1;
*/

--DROP TRIGGER generate_groups_id;
CREATE OR REPLACE TRIGGER generate_groups_id
BEFORE INSERT ON St_Groups
FOR EACH ROW
DECLARE 
    max_id NUMBER;
BEGIN
    IF :NEW.ID IS NULL THEN
        SELECT MAX(ID)
        INTO max_id
        FROM St_Groups;
        
        IF max_id IS NOT NULL THEN
            :NEW.ID := max_id + 1;
        ELSE 
            :NEW.ID := 1;
        END IF;
    END IF;
END;
/
