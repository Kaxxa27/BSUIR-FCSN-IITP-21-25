CREATE OR REPLACE TRIGGER tr_uni AFTER
    INSERT OR UPDATE OR DELETE ON uni FOR EACH ROW
BEGIN
    IF inserting THEN
        INSERT INTO uni_logs (
            uni_id,
            uni_name,
            creation_date,
            change_date,
            change_type
        ) VALUES (
            :new.uni_id,
            :new.uni_name,
            :new.creation_date,
            systimestamp,
            'INSERT'
        );
    ELSIF deleting THEN
        INSERT INTO uni_logs (
            uni_id,
            uni_name,
            creation_date,
            change_date,
            change_type
        ) VALUES (
            :old.uni_id,
            :old.uni_name,
            :old.creation_date,
            systimestamp,
            'DELETE'
        );
    ELSIF updating THEN
        INSERT INTO uni_logs (
            uni_id,
            uni_name,
            creation_date,
            change_date,
            change_type
        ) VALUES (
            :new.uni_id,
            :new.uni_name,
            :new.creation_date,
            systimestamp,
            'UPDATE'
        );
    END IF;
END;
/
 ------------------------------------------------------------------------------------------------------------
CREATE OR REPLACE TRIGGER tr_groups AFTER INSERT OR UPDATE OR DELETE ON groups FOR EACH ROW BEGIN IF inserting THEN INSERT INTO groups_logs (
    gr_id,
    gr_name,
    uni_id,
    creation_date,
    change_date,
    change_type
) VALUES (
    :new.gr_id,
    :new.gr_name,
    :new.uni_id,
    :new.creation_date,
    systimestamp,
    'INSERT'
);
ELSIF deleting THEN
    INSERT INTO groups_logs (
        gr_id,
        gr_name,
        uni_id,
        creation_date,
        change_date,
        change_type
    ) VALUES (
        :old.gr_id,
        :old.gr_name,
        :old.uni_id,
        :old.creation_date,
        systimestamp,
        'DELETE'
    );
ELSIF updating THEN
    INSERT INTO groups_logs (
        gr_id,
        gr_name,
        uni_id,
        creation_date,
        change_date,
        change_type
    ) VALUES (
        :new.gr_id,
        :new.gr_name,
        :new.uni_id,
        :new.creation_date,
        systimestamp,
        'UPDATE'
    );
END IF;
END;
/
 ------------------------------------------------------------------------------------------------------------
CREATE OR REPLACE TRIGGER tr_students AFTER INSERT OR UPDATE OR DELETE ON students FOR EACH ROW BEGIN IF inserting THEN INSERT INTO students_logs (
    st_id,
    st_name,
    gr_id,
    enter_date,
    change_date,
    change_type
) VALUES (
    :new.st_id,
    :new.st_name,
    :new.gr_id,
    :new.enter_date,
    systimestamp,
    'INSERT'
);
ELSIF deleting THEN
    INSERT INTO students_logs (
        st_id,
        st_name,
        gr_id,
        enter_date,
        change_date,
        change_type
    ) VALUES (
        :old.st_id,
        :old.st_name,
        :old.gr_id,
        :old.enter_date,
        systimestamp,
        'DELETE'
    );
ELSIF updating THEN
    INSERT INTO students_logs (
        st_id,
        st_name,
        gr_id,
        enter_date,
        change_date,
        change_type
    ) VALUES (
        :new.st_id,
        :new.st_name,
        :new.gr_id,
        :new.enter_date,
        systimestamp,
        'UPDATE'
    );
END IF;
END;
/