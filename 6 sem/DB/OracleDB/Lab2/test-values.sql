ALTER TRIGGER update_groups_cval_trigger DISABLE;
--ALTER TRIGGER update_groups_cval_trigger ENABLE;

-- Вставка тестовых значений в таблицу GROUPS
INSERT INTO st_groups (ID, NAME, C_VAL) VALUES (1, 'Группа 1', 0);
INSERT INTO st_groups (NAME, C_VAL) VALUES ('Группа 2', 0);
INSERT INTO st_groups (NAME, C_VAL) VALUES ('Группа 3', 0);
UPDATE st_groups SET ID = 1 WHERE NAME = 'Группа 2';
--INSERT INTO st_groups (ID, NAME, C_VAL) VALUES (77,'Группа 1', 3);


-- Вставка тестовых значений в таблицу STUDENTS

INSERT INTO students (ID, NAME, St_Groups_ID) VALUES (89, 'Студент 1', 1);
INSERT INTO students (NAME, St_Groups_ID) VALUES ('Студент 1', 1);
INSERT INTO students (NAME, St_Groups_ID) VALUES ('Студент 2', 2);
INSERT INTO students (NAME, St_Groups_ID) VALUES ('Студент 3', 3);
INSERT INTO students (NAME, St_Groups_ID) VALUES ('Студент 4', 3);
INSERT INTO students (NAME, St_Groups_ID) VALUES ('Студент 5', 3);
UPDATE Students SET NAME = 'Kaxxa' WHERE ID = 1;
UPDATE Students SET ID = 2 WHERE ID = 3;

--DELETE FROM St_Groups WHERE ID =1;
DELETE FROM Students WHERE ID = 1;
SELECT * FROM Students;
SELECT * FROM St_Groups;
SELECT * FROM Students_LOG;
/
DELETE FROM Students;
--EXECUTE restore_students_from_log(time_offset => INTERVAL '0 00:01:00' DAY TO SECOND);

BEGIN
    restore_students_from_log(restore_time => TIMESTAMP '2024-02-28 15:27:00');
    --restore_students_from_log(time_offset => INTERVAL '100' SECOND)
    
    --restore_students_from_log(time_offset => INTERVAL '100' SECOND);
END;
/