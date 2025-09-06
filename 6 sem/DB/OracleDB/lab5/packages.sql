CREATE OR REPLACE PACKAGE func_package IS

    PROCEDURE roll_back(
        date_time TIMESTAMP
    );

    PROCEDURE roll_back(
        date_time NUMBER
    );

    PROCEDURE report(
        t_begin IN TIMESTAMP
    );

    PROCEDURE report;
END func_package;
/
 ------------------------------------------------------------------------------------------------------------
create OR REPLACE PACKAGE BODY func_package IS

    PROCEDURE roll_back(
        date_time TIMESTAMP
    ) IS
    BEGIN
        rollback_by_date(date_time);
END roll_back;

PROCEDURE roll_back(
    date_time NUMBER
) IS
BEGIN
    DECLARE
        current_time timestamp := systimestamp;
    BEGIN
        current_time := current_time - numtodsinterval(date_time / 1000, 'SECOND');
        rollback_by_date(current_time);
    END;
END roll_back;

PROCEDURE report(
    t_begin IN TIMESTAMP
) IS
    v_cur timestamp := systimestamp;
BEGIN
    create_report(t_begin, v_cur);
    INSERT INTO reports_logs(
        report_date
    ) VALUES(
        v_cur
    );
END report;

PROCEDURE report IS
    v_begin timestamp := to_timestamp('1/1/1 1:1:1', 'YYYY/MM/DD HH:MI:SS');
    v_cur   timestamp := systimestamp;
    v_count NUMBER;
BEGIN
    SELECT COUNT( * ) INTO v_count
    FROM reports_logs;
    IF (v_count > 0) THEN
        SELECT report_date INTO v_begin
        FROM reports_logs
        WHERE id = (
                SELECT MAX(id)
                FROM reports_logs
            );
    END IF;

    create_report(v_begin, v_cur);
    INSERT INTO reports_logs(
        report_date
    ) VALUES(
        v_cur
    );
END report;
END func_package;