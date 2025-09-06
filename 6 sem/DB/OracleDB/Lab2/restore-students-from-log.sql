DROP PROCEDURE restore_students_from_log;
CREATE OR REPLACE PROCEDURE restore_students_from_log(
    restore_time TIMESTAMP DEFAULT NULL,
    time_offset INTERVAL DAY TO SECOND DEFAULT NULL
) 
AS
    restore_timestamp TIMESTAMP;
BEGIN
    IF restore_time IS NULL AND time_offset IS NULL THEN
        RAISE_APPLICATION_ERROR(-20001, 'Не указано время восстановления или временное смещение.');
    END IF;

    IF restore_time IS NOT NULL THEN
        restore_timestamp := restore_time;
    ELSE
        restore_timestamp := SYSTIMESTAMP - time_offset;
    END IF;

    FOR log_record IN (SELECT * FROM Students_LOG WHERE LOG_TIMESTAMP <= restore_timestamp) 
    LOOP
        IF log_record.ACTION = 'INSERT' THEN
            IF log_record.STUDENT_ID IS NOT NULL THEN
                DELETE FROM Students WHERE ID = log_record.STUDENT_ID; 
            END IF;
            INSERT INTO Students (ID, NAME, St_Groups_ID)
            VALUES (log_record.STUDENT_ID, log_record.STUDENT_NAME, log_record.St_Groups_ID);
        ELSIF log_record.ACTION = 'UPDATE' THEN
            UPDATE Students
            SET NAME = log_record.STUDENT_NAME, St_Groups_ID = log_record.St_Groups_ID
            WHERE ID = log_record.STUDENT_ID;
        ELSIF log_record.ACTION = 'DELETE' THEN
            DELETE FROM Students WHERE ID = log_record.STUDENT_ID;
        END IF;
    END LOOP;
END;
/
