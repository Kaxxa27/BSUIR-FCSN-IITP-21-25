DROP TRIGGER update_groups_cval_trigger;
CREATE OR REPLACE TRIGGER update_groups_cval_trigger
AFTER INSERT OR UPDATE OR DELETE ON Students
FOR EACH ROW
DECLARE 
    isExist NUMBER;
BEGIN 
    SELECT * INTO isExist FROM St_Groups WHERE ID = :NEW.St_Groups_ID;
    IF isExist != 0 THEN 
        IF UPDATING THEN
            UPDATE St_Groups
            SET C_VAL = C_VAL + 1
            WHERE id = :NEW.St_Groups_ID;
    
            UPDATE St_Groups
            SET C_VAL = C_VAL - 1
            WHERE id = :OLD.St_Groups_ID;
        END IF;
    
        IF INSERTING THEN
            UPDATE St_Groups
            SET C_VAL = C_VAL + 1
            WHERE id = :NEW.St_Groups_ID;
        END IF;
    
        IF DELETING THEN
            UPDATE St_Groups
            SET C_VAL = C_VAL - 1
            WHERE id = :OLD.St_Groups_ID;
        END IF;
        END IF;
END;
/
