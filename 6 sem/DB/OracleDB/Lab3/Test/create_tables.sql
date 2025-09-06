
-- DEV SCHEME
DROP TABLE dev.students;

DROP TABLE dev.groups;

DROP TABLE dev.uni;

CREATE TABLE dev.uni (
    uni_id NUMBER NOT NULL,
    uni_name VARCHAR2(20) NOT NULL,
    CONSTRAINT uni_id_pk PRIMARY KEY (uni_id)
);
--CREATE TABLE dev.test (
--    test_id NUMBER NOT NULL
--);
CREATE TABLE dev.groups (
    gr_id NUMBER NOT NULL,
    gr_name VARCHAR2(20) NOT NULL,
    uni_id NUMBER NOT NULL,
    CONSTRAINT gr_id_pk PRIMARY KEY (gr_id),
    CONSTRAINT uni_id_fk FOREIGN KEY (uni_id) REFERENCES dev.uni (uni_id)
);

ALTER TABLE dev.groups ADD gr_motto VARCHAR2(200) NOT NULL;

CREATE INDEX gr_motto_idx ON dev.groups (gr_motto);

CREATE TABLE dev.students (
    st_id NUMBER NOT NULL,
    st_name VARCHAR2(20) NOT NULL,
    gr_id NUMBER NOT NULL,
    CONSTRAINT st_id_pk PRIMARY KEY (st_id),
    CONSTRAINT gr_id_fk FOREIGN KEY (gr_id) REFERENCES dev.groups (gr_id)
);

-- DROP LOOPS

ALTER TABLE dev.cc DROP CONSTRAINT fk_c_a;

ALTER TABLE dev.account DROP CONSTRAINT h_id_fk;

DROP TABLE dev.aa;

DROP TABLE dev.bb;

DROP TABLE dev.cc;

DROP TABLE dev.human;

DROP TABLE dev.account;

CREATE TABLE dev.human (
    h_id NUMBER NOT NULL,
    acc_id NUMBER UNIQUE NOT NULL,
    CONSTRAINT h_id_pk PRIMARY KEY (h_id)
);

CREATE TABLE dev.account (
    acc_id NUMBER NOT NULL,
    h_id NUMBER UNIQUE NOT NULL,
    CONSTRAINT acc_id_pk PRIMARY KEY (acc_id),
    CONSTRAINT h_id_fk FOREIGN KEY (h_id) REFERENCES dev.human (h_id)
);

ALTER TABLE dev.human ADD CONSTRAINT acc_id_fk FOREIGN KEY (acc_id) REFERENCES dev.account (acc_id);

CREATE TABLE dev.aa (
    id NUMBER PRIMARY KEY,
    b_id NUMBER NOT NULL
);

CREATE TABLE dev.bb (
    id NUMBER PRIMARY KEY,
    a_id NUMBER NOT NULL
);

CREATE TABLE dev.cc (
    id NUMBER PRIMARY KEY,
    a_id NUMBER NOT NULL

);

ALTER TABLE dev.aa ADD CONSTRAINT fk_a_b FOREIGN KEY (b_id) REFERENCES dev.bb (id);

--ALTER TABLE dev.bb ADD CONSTRAINT fk_b_c FOREIGN KEY (c_id) REFERENCES dev.cc (id);

ALTER TABLE dev.cc ADD CONSTRAINT fk_c_a FOREIGN KEY (a_id) REFERENCES dev.aa (id);

-- PROD SCHEME
DROP TABLE prod.students;

DROP TABLE prod.groups;

DROP TABLE prod.uni;

CREATE TABLE prod.uni (
    uni_id NUMBER NOT NULL,
    uni_name VARCHAR2(20) NOT NULL,
    CONSTRAINT uni_id_pk PRIMARY KEY (uni_id)
);

CREATE INDEX test_index ON prod.students (st_name);

CREATE TABLE prod.groups (
    gr_id NUMBER NOT NULL,
    gr_name VARCHAR2(20) NOT NULL,
    uni_id NUMBER NOT NULL,
    st_count NUMBER NOT NULL,
    CONSTRAINT gr_id_pk PRIMARY KEY (gr_id),
    CONSTRAINT uni_id_fk FOREIGN KEY (uni_id) REFERENCES prod.uni (uni_id)
);

CREATE TABLE prod.students (
    st_id NUMBER NOT NULL,
    st_name VARCHAR2(20) NOT NULL,
    st_surname VARCHAR2(20) NOT NULL,
    gr_id NUMBER NOT NULL,
    CONSTRAINT st_id_pk PRIMARY KEY (st_id),
    CONSTRAINT gr_id_fk FOREIGN KEY (gr_id) REFERENCES prod.groups (gr_id)
);

ALTER TABLE prod.students ADD CONSTRAINT st_name_length_check CHECK (length(st_name) >= 10);

CREATE INDEX st_name_idx ON prod.students (st_name);