DELETE FROM uni;

DELETE FROM groups;

DELETE FROM students;

DELETE FROM uni_logs;

DELETE FROM groups_logs;

DELETE FROM students_logs;

DELETE FROM reports_logs;

INSERT INTO uni (
    uni_name,
    creation_date
) VALUES(
    'u1',
    TIMESTAMP'2024-03-03 12:00:00'
);

INSERT INTO uni (
    uni_name,
    creation_date
) VALUES(
    '1234sdfhfghj',
     TIMESTAMP'2024-03-15 12:00:00'
);


INSERT INTO uni (
    uni_name,
    creation_date
) VALUES(
    'Denis',
     TIMESTAMP'2024-03-10 12:00:00'
);

INSERT INTO uni (
    uni_name,
    creation_date
) VALUES(
    'u3',
      TIMESTAMP'2024-03-2 12:00:00'
);



UPDATE uni
SET
    creation_date = systimestamp
WHERE
    uni_name = 'u1';

DELETE FROM uni
WHERE
    uni_name = 'u3';

SELECT *
FROM uni_logs
ORDER BY change_date;

SELECT * FROM uni;
SELECT * FROM reports_logs;

SELECT *
FROM uni
ORDER BY uni_id;

CALL func_package.roll_back(to_timestamp(TIMESTAMP'2024-05-03 11:15:02'));

CALL func_package.roll_back(300000);


CALL func_package.report();
CALL func_package.report(TIMESTAMP'2024-05-03 11:15:00')