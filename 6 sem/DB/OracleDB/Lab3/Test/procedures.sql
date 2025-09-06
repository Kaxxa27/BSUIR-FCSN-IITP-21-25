DROP PROCEDURE dev.hello_world;

CREATE OR REPLACE PROCEDURE dev.hello_world AS
BEGIN
    dbms_output.put_line('Hello world');
END;
/
DROP PROCEDURE dev.ya_ne_sdal;
CREATE OR REPLACE PROCEDURE dev.ya_ne_sdal AS
BEGIN
    dbms_output.put_line('ya_ne_sdald');
END;
/

DROP PROCEDURE dev.cur_date;
CREATE OR REPLACE PROCEDURE dev.cur_date AS
BEGIN
    dbms_output.put_line('cur date');
END;
/
DROP PROCEDURE prod.cur_date;
CREATE OR REPLACE PROCEDURE prod.cur_date AS
BEGIN
    dbms_output.put_line('cur date');
END;
/
DROP PROCEDURE prod.goodbye_world;
CREATE OR REPLACE PROCEDURE prod.goodbye_world AS
BEGIN
    dbms_output.put_line('Goodbye world');
END;
/
DROP PROCEDURE dev.diff_param;
CREATE OR REPLACE PROCEDURE dev.diff_param (param1 IN VARCHAR2) AS
BEGIN
    dbms_output.put_line('Param VARCHAR2: ' || param1);
END;
/
DROP PROCEDURE prod.diff_param;
CREATE OR REPLACE PROCEDURE prod.diff_param (param1 OUT VARCHAR2) AS
BEGIN
    dbms_output.put_line('Param NUMBER: ' || param1);
END;
/
DROP PROCEDURE dev.diff_code;
CREATE OR REPLACE PROCEDURE dev.diff_code (param1 VARCHAR2) AS
BEGIN
    dbms_output.put_line('Param VARCHAR2: ' || param1);
END;
/
DROP PROCEDURE prod.diff_code;
CREATE OR REPLACE PROCEDURE prod.diff_code (param1 VARCHAR2) AS
BEGIN
    dbms_output.put_line('Param param VARCHAR2: ' || param1);
END;