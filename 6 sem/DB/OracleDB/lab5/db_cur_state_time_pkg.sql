CREATE OR REPLACE PACKAGE cur_state_timestamp_pkg IS
    cur_state_time timestamp;

    PROCEDURE set_time(
        p_value IN TIMESTAMP
    );
END cur_state_timestamp_pkg;
/
CREATE OR REPLACE PACKAGE BODY cur_state_timestamp_pkg IS

    PROCEDURE set_time(
        p_value IN TIMESTAMP
    ) IS
    BEGIN
        cur_state_time := p_value;
END set_time;
END cur_state_timestamp_pkg;