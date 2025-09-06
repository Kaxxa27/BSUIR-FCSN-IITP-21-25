--DROP TRIGGER cascade_delete_student;
CREATE OR REPLACE TRIGGER cascade_delete_students
BEFORE DELETE ON St_Groups
FOR EACH ROW
BEGIN
    DELETE FROM Students
    WHERE St_Groups_ID = :OLD.ID;
END;
/
