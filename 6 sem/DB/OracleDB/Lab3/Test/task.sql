CREATE OR REPLACE PROCEDURE compare_schemas (
    dev_schema_name IN VARCHAR2,
    prod_schema_name IN VARCHAR2
) AS
    TYPE string_list_t IS
        TABLE OF VARCHAR2(100);
    dev_tables        string_list_t := string_list_t();
    prod_tables       string_list_t := string_list_t();
    visited_tables    string_list_t := string_list_t();
    sorted_tables     string_list_t := string_list_t();
    dev_procs         string_list_t := string_list_t();
    prod_procs        string_list_t := string_list_t();
    dev_only_procs    string_list_t := string_list_t();
    prod_only_procs   string_list_t := string_list_t();
    different_procs   string_list_t := string_list_t();
    dev_funcs         string_list_t := string_list_t();
    prod_funcs        string_list_t := string_list_t();
    dev_only_funcs    string_list_t := string_list_t();
    prod_only_funcs   string_list_t := string_list_t();
    different_funcs   string_list_t := string_list_t();
    dev_indexes       string_list_t := string_list_t();
    prod_indexes      string_list_t := string_list_t();
    dev_only_indexes  string_list_t := string_list_t();
    prod_only_indexes string_list_t := string_list_t();
    ddl_script        CLOB := '';
    counter           NUMBER;

    FUNCTION are_tables_identical(
        p_table_name IN VARCHAR2
    ) RETURN BOOLEAN IS
        v_identical          BOOLEAN := true;
        dev_constraints_set  string_list_t;
        prod_constraints_set string_list_t;
        dev_columns_set      string_list_t;
        prod_columns_set     string_list_t;
    BEGIN
    
        SELECT constraint_name BULK COLLECT INTO dev_constraints_set
        FROM all_constraints
        WHERE owner = dev_schema_name
            AND table_name = p_table_name
            AND constraint_name NOT LIKE 'SYS%'
        ORDER BY constraint_name;
        
        SELECT constraint_name BULK COLLECT INTO prod_constraints_set
        FROM all_constraints
        WHERE owner = prod_schema_name
            AND table_name = p_table_name
            AND constraint_name NOT LIKE 'SYS%'
        ORDER BY constraint_name;
        
        SELECT column_name BULK COLLECT INTO dev_columns_set
        FROM all_tab_columns
        WHERE owner = dev_schema_name
            AND table_name = p_table_name
            AND column_name NOT LIKE 'SYS%'
        ORDER BY column_name;
        
        SELECT column_name BULK COLLECT INTO prod_columns_set
        FROM all_tab_columns
        WHERE owner = prod_schema_name
            AND table_name = p_table_name
            AND column_name NOT LIKE 'SYS%'
        ORDER BY column_name;
        
        v_identical := true;
        IF dev_constraints_set MULTISET EXCEPT prod_constraints_set IS NOT EMPTY OR prod_constraints_set MULTISET EXCEPT dev_constraints_set IS NOT EMPTY THEN
            v_identical := false;
        END IF;

        IF dev_columns_set MULTISET EXCEPT prod_columns_set IS NOT EMPTY OR prod_columns_set MULTISET EXCEPT dev_columns_set IS NOT EMPTY THEN
            v_identical := false;
        END IF;

        IF v_identical THEN
            RETURN v_identical;
        END IF;

        ddl_script  := '';
        DECLARE
            data_type      VARCHAR2(100);
            data_length    NUMBER;
            data_precision NUMBER;
        BEGIN
            FOR i IN 1..dev_columns_set.count LOOP
                IF dev_columns_set(i) NOT MEMBER OF prod_columns_set THEN
                    SELECT data_type,
                        data_length,
                        data_precision INTO data_type,
                        data_length,
                        data_precision
                    FROM all_tab_columns
                    WHERE owner = dev_schema_name
                        AND table_name = p_table_name
                        AND column_name = dev_columns_set(i);
                    ddl_script := ddl_script || 'ALTER TABLE ' || p_table_name || ' ADD COLUMN ' || dev_columns_set(i) || ' ' || data_type;
                    IF data_type IN ('VARCHAR2', 'NVARCHAR2', 'RAW') AND data_length IS NOT NULL THEN
                        ddl_script := ddl_script || '(' || data_length || ')';
                    ELSIF data_type IN ('NUMBER') AND data_precision IS NOT NULL THEN
                        ddl_script := ddl_script || '(' || data_precision || ')';
                    END IF;

                    ddl_script := ddl_script || chr(10);
                END IF;
            END LOOP;
        END;

        FOR i IN 1..dev_constraints_set.count LOOP
            IF dev_constraints_set(i) NOT MEMBER OF prod_constraints_set THEN
                DECLARE
                    ddl_script      CLOB;
                    constraint_type VARCHAR2(20);
                BEGIN
                    SELECT constraint_type INTO constraint_type
                    FROM all_constraints
                    WHERE owner = dev_schema_name
                        AND table_name = p_table_name
                        AND constraint_name = dev_constraints_set(i);
                    ddl_script := ddl_script || dbms_metadata.get_ddl(CASE WHEN constraint_type = 'R' THEN 'REF_CONSTRAINT' ELSE 'CONSTRAINT' END, dev_constraints_set(i), dev_schema_name);
                    ddl_script := ddl_script || chr(10);
                END;
            END IF;
        END LOOP;

        FOR i IN 1..prod_columns_set.count LOOP
            IF prod_columns_set(i) NOT MEMBER OF dev_columns_set THEN
                ddl_script := ddl_script || 'ALTER TABLE ' || prod_schema_name || '.' || p_table_name || ' DROP COLUMN ' || prod_columns_set(i) || ';' || chr(10);
            END IF;
        END LOOP;

        FOR i IN 1..prod_constraints_set.count LOOP
            IF prod_columns_set(i) NOT MEMBER OF dev_constraints_set THEN
                ddl_script := ddl_script || 'ALTER TABLE ' || prod_schema_name || '.' || p_table_name || ' DROP CONSTRAINT ' || prod_constraints_set(i) || ';' || chr(10);
            END IF;
        END LOOP;

        dbms_output.put_line('DDL-script for updating table ' || p_table_name || ' {' || chr(10));
        ddl_script  := replace(ddl_script, dev_schema_name, prod_schema_name);
        dbms_output.put_line(ddl_script);
        RETURN v_identical;
    END are_tables_identical;

    PROCEDURE dfs_sort(
        p_table_name IN VARCHAR2
    ) IS
        CURSOR fk_cur IS
        SELECT cc.table_name AS child_table
        FROM all_constraints pc
            JOIN all_constraints cc
            ON pc.constraint_name = cc.r_constraint_name
        WHERE pc.constraint_type = 'P'
            AND cc.constraint_type = 'R'
            AND pc.owner = dev_schema_name
            AND cc.owner = dev_schema_name
            AND pc.table_name = p_table_name;
        v_child_table VARCHAR2(100);
    BEGIN
        IF p_table_name NOT MEMBER OF visited_tables THEN
            visited_tables.extend;
            visited_tables(visited_tables.last) := p_table_name;
            FOR fk_rec IN fk_cur LOOP
                v_child_table := fk_rec.child_table;
                dfs_sort(v_child_table);
            END LOOP;

            sorted_tables.extend;
            sorted_tables(sorted_tables.last) := p_table_name;
        END IF;
    END dfs_sort;
    
BEGIN
    SELECT table_name BULK COLLECT INTO dev_tables
    FROM all_tables
    WHERE owner = dev_schema_name;
    
    SELECT table_name BULK COLLECT INTO prod_tables
    FROM all_tables
    WHERE owner = prod_schema_name;
    
    FOR i IN 1..dev_tables.count LOOP
        dfs_sort(dev_tables(i));
    END LOOP;

    SELECT COUNT( * ) INTO counter
    FROM (WITH table_hierarchy AS (
            SELECT child_owner,
                child_table,
                parent_owner,
                parent_table
            FROM (
                    SELECT owner             AS child_owner,
                        table_name        AS child_table,
                        r_owner           AS parent_owner,
                        r_constraint_name AS constraint_name
                    FROM all_constraints
                    WHERE constraint_type = 'R'
                        AND owner = 'DEV'
                )
                JOIN (
                    SELECT owner           AS parent_owner,
                        constraint_name,
                        table_name      AS parent_table
                    FROM all_constraints
                    WHERE constraint_type = 'P'
                        AND owner = 'DEV'
                )
                USING (parent_owner,
                constraint_name)
        )
            SELECT DISTINCT child_owner,
                child_table
            FROM (
                    SELECT *
                    FROM table_hierarchy
                    WHERE (child_owner, child_table) IN (
                            SELECT parent_owner,
                                parent_table
                            FROM table_hierarchy
                        )
                ) a
            WHERE CONNECT_BY_ISCYCLE = 1
            CONNECT BY NOCYCLE (PRIOR child_owner, PRIOR child_table) = ((parent_owner, parent_table))
        );
    IF counter > 0 THEN
        dbms_output.put_line('Циклические зависимости найдены.');
    END IF;

    dbms_output.put_line(chr(10) || 'Table in DEV but not in PROD or with different structure =========================');
    ddl_script        := '';
    FOR i IN REVERSE 1..sorted_tables.count LOOP
        IF sorted_tables(i) NOT MEMBER OF prod_tables THEN
            dbms_output.put_line( dbms_metadata.get_ddl('TABLE', sorted_tables(i), dev_schema_name) || chr(10));
        ELSIF NOT are_tables_identical(sorted_tables(i)) THEN
            dbms_output.put_line( sorted_tables(i) || ' }' || chr(10));
        END IF;
    END LOOP;

    ddl_script        := replace(ddl_script, dev_schema_name, prod_schema_name);
    dbms_output.put_line(ddl_script);
    
    SELECT object_name BULK COLLECT INTO dev_procs
    FROM all_objects
    WHERE object_type = 'PROCEDURE'
        AND owner = dev_schema_name;
        
    SELECT object_name BULK COLLECT INTO prod_procs
    FROM all_objects
    WHERE object_type = 'PROCEDURE'
        AND owner = prod_schema_name;
    FOR i IN 1..dev_procs.count LOOP
        IF dev_procs(i) MEMBER OF prod_procs THEN
            SELECT COUNT( * ) INTO counter
            FROM (
                    SELECT argument_name,
                        position,
                        data_type,
                        in_out
                    FROM all_arguments
                    WHERE owner = dev_schema_name
                        AND object_name = dev_procs(i) MINUS
                        SELECT argument_name,
                            position,
                            data_type,
                            in_out
                        FROM all_arguments
                        WHERE owner = prod_schema_name
                            AND object_name = dev_procs(i)
                        UNION
                        ALL
                        SELECT argument_name,
                            position,
                            data_type,
                            in_out
                        FROM all_arguments
                        WHERE owner = prod_schema_name
                            AND object_name = dev_procs(i) MINUS
                            SELECT argument_name,
                                position,
                                data_type,
                                in_out
                            FROM all_arguments
                            WHERE owner = dev_schema_name
                                AND object_name = dev_procs(i)
                )             ;
            IF counter > 0 THEN
                different_procs.extend;
                different_procs(different_procs.last) := dev_procs(i);
            END IF;
        END IF;
    END LOOP;

    dev_only_procs    := dev_procs MULTISET EXCEPT prod_procs;
    prod_only_procs   := prod_procs MULTISET EXCEPT dev_procs;
    dbms_output.put_line('Only DEV procedures =========================');
    ddl_script        := '';
    FOR i IN 1..dev_only_procs.count LOOP
        ddl_script := ddl_script || dbms_metadata.get_ddl('PROCEDURE', dev_only_procs(i), dev_schema_name);
    END LOOP;

    ddl_script        := replace(ddl_script, dev_schema_name, prod_schema_name);
    dbms_output.put_line(ddl_script);
    dbms_output.put_line('Only PROD procedures =========================');
    ddl_script        := '';
    FOR i IN 1..prod_only_procs.count LOOP
        ddl_script := ddl_script || dbms_metadata.get_ddl('PROCEDURE', prod_only_procs(i), prod_schema_name);
    END LOOP;

    ddl_script        := replace(ddl_script, dev_schema_name, prod_schema_name);
    dbms_output.put_line(ddl_script);
    dbms_output.put_line(chr(10) || 'Procedures that have different parameters =========================');
    ddl_script        := '';
    FOR i IN 1..different_procs.count LOOP
        ddl_script := ddl_script || 'DROP PROCEDURE' || prod_schema_name || '.' || different_procs(i) || ';';
        ddl_script := ddl_script || dbms_metadata.get_ddl('PROCEDURE', different_procs(i), dev_schema_name);
    END LOOP;

    ddl_script        := replace(ddl_script, dev_schema_name, prod_schema_name);
    dbms_output.put_line(ddl_script);
    SELECT object_name BULK COLLECT INTO dev_funcs
    FROM all_objects
    WHERE object_type = 'FUNCTION'
        AND owner = dev_schema_name;
    SELECT object_name BULK COLLECT INTO prod_funcs
    FROM all_objects
    WHERE object_type = 'FUNCTION'
        AND owner = prod_schema_name;
    FOR i IN 1..dev_funcs.count LOOP
        IF dev_funcs(i) MEMBER OF prod_funcs THEN
            SELECT COUNT( * ) INTO counter
            FROM (
                    SELECT argument_name,
                        position,
                        data_type,
                        in_out
                    FROM all_arguments
                    WHERE owner = dev_schema_name
                        AND object_name = dev_funcs(i) MINUS
                        SELECT argument_name,
                            position,
                            data_type,
                            in_out
                        FROM all_arguments
                        WHERE owner = prod_schema_name
                            AND object_name = dev_funcs(i)
                        UNION
                        ALL
                        SELECT argument_name,
                            position,
                            data_type,
                            in_out
                        FROM all_arguments
                        WHERE owner = prod_schema_name
                            AND object_name = dev_funcs(i) MINUS
                            SELECT argument_name,
                                position,
                                data_type,
                                in_out
                            FROM all_arguments
                            WHERE owner = dev_schema_name
                                AND object_name = dev_funcs(i)
                )             ;
            IF counter > 0 THEN
                different_funcs.extend;
                different_funcs(different_funcs.last) := dev_funcs(i);
            END IF;
        END IF;
    END LOOP;

    dev_only_funcs    := dev_funcs MULTISET EXCEPT prod_funcs;
    prod_only_funcs   := prod_funcs MULTISET EXCEPT dev_funcs;
    dbms_output.put_line(chr(10) || 'Only DEV functions =========================');
    ddl_script        := '';
    FOR i IN 1..dev_only_funcs.count LOOP
        ddl_script := ddl_script || dbms_metadata.get_ddl('FUNCTION', dev_only_funcs(i), dev_schema_name);
    END LOOP;

    ddl_script        := replace(ddl_script, dev_schema_name, prod_schema_name);
    dbms_output.put_line(ddl_script);
    dbms_output.put_line(chr(10) || 'Only PROD functions =========================');
    ddl_script        := '';
    FOR i IN 1..prod_only_funcs.count LOOP
        ddl_script := ddl_script || dbms_metadata.get_ddl('FUNCTION', prod_only_funcs(i), prod_schema_name);
    END LOOP;

    ddl_script        := replace(ddl_script, dev_schema_name, prod_schema_name);
    dbms_output.put_line(ddl_script);
    dbms_output.put_line(chr(10) || 'Functions with different parameters or return type =========================');
    ddl_script        := '';
    FOR i IN 1..different_funcs.count LOOP
        ddl_script := ddl_script || 'DROP FUNCTION' || prod_schema_name || '.' || different_funcs(i) || ';';
        ddl_script := ddl_script || dbms_metadata.get_ddl('FUNCTION', different_funcs(i), dev_schema_name);
    END LOOP;

    ddl_script        := replace(ddl_script, dev_schema_name, prod_schema_name);
    dbms_output.put_line(ddl_script);
    SELECT index_name BULK COLLECT INTO dev_indexes
    FROM all_indexes
    WHERE owner = dev_schema_name
        AND index_name NOT LIKE 'SYS%';
    SELECT index_name BULK COLLECT INTO prod_indexes
    FROM all_indexes
    WHERE owner = prod_schema_name
        AND index_name NOT LIKE 'SYS%';
    dev_only_indexes  := dev_indexes MULTISET EXCEPT prod_indexes;
    prod_only_indexes := prod_indexes MULTISET EXCEPT dev_indexes;
    dbms_output.put_line('Only DEV indexes =========================');
    ddl_script        := '';
    FOR i IN 1..dev_only_indexes.count LOOP
        ddl_script := ddl_script || dbms_metadata.get_ddl('INDEX', dev_only_indexes(i), dev_schema_name);
    END LOOP;

    ddl_script        := replace(ddl_script, dev_schema_name, prod_schema_name);
    dbms_output.put_line(ddl_script);
    dbms_output.put_line('Only PROD indexes =========================');
    ddl_script        := '';
    FOR i IN 1..prod_only_indexes.count LOOP
        ddl_script := ddl_script || 'DROP INDEX' || prod_schema_name || '.' || prod_only_indexes(i) || ';';
    END LOOP;

    ddl_script        := replace(ddl_script, dev_schema_name, prod_schema_name);
    dbms_output.put_line(ddl_script);
END compare_schemas;