CREATE OR REPLACE PROCEDURE rollback_by_date (
    date_time IN TIMESTAMP
) AS
    v_cur timestamp := systimestamp;
BEGIN
    DELETE FROM students;
    DELETE FROM groups;
    DELETE FROM uni;
    FOR log_ IN (
        SELECT *
        FROM uni_logs
        WHERE change_date <= date_time
        ORDER BY action_id
    ) LOOP
        IF log_.change_type = 'INSERT' THEN
            INSERT INTO uni VALUES (
                log_.uni_id,
                log_.uni_name,
                log_.creation_date
            );
        ELSIF log_.change_type = 'DELETE' THEN
            DELETE FROM uni
            WHERE
                uni_id = log_.uni_id;
        ELSIF log_.change_type = 'UPDATE' THEN
            UPDATE uni
            SET
                uni_id = log_.uni_id,
                uni_name = log_.uni_name,
                creation_date = log_.creation_date
            WHERE
                uni_id = log_.uni_id;
        END IF;
    END LOOP;

    FOR log_ IN (
        SELECT *
        FROM groups_logs
        WHERE change_date <= date_time
        ORDER BY action_id
    ) LOOP
        IF log_.change_type = 'INSERT' THEN
            INSERT INTO groups VALUES (
                log_.gr_id,
                log_.gr_name,
                log_.uni_id,
                log_.creation_date
            );
        ELSIF log_.change_type = 'DELETE' THEN
            DELETE FROM groups
            WHERE
                gr_id = log_.gr_id;
        ELSIF log_.change_type = 'UPDATE' THEN
            UPDATE groups
            SET
                gr_id = log_.gr_id,
                gr_name = log_.gr_name,
                uni_id = log_.uni_id,
                creation_date = log_.creation_date
            WHERE
                gr_id = log_.gr_id;
        END IF;
    END LOOP;

    FOR log_ IN (
        SELECT *
        FROM students_logs
        WHERE change_date <= date_time
        ORDER BY action_id
    ) LOOP
        IF log_.change_type = 'INSERT' THEN
            INSERT INTO students VALUES (
                log_.st_id,
                log_.st_name,
                log_.gr_id,
                log_.enter_date
            );
        ELSIF log_.change_type = 'DELETE' THEN
            DELETE FROM students
            WHERE
                st_id = log_.st_id;
        ELSIF log_.change_type = 'UPDATE' THEN
            UPDATE students
            SET
                st_id = log_.st_id,
                st_name = log_.st_name,
                gr_id = log_.gr_id,
                enter_date = log_.enter_date
            WHERE
                st_id = log_.st_id;
        END IF;
    END LOOP;
    
    DELETE FROM uni_logs
    WHERE change_date >= v_cur;
        
    DELETE FROM groups_logs
    WHERE change_date >= v_cur;
        
    DELETE FROM students_logs
    WHERE change_date >= v_cur;

    cur_state_timestamp_pkg.set_time(date_time);
END;
/
 ------------------------------------------------------------------------------------------------------------
CREATE OR REPLACE PROCEDURE create_report(t_begin IN TIMESTAMP, t_end IN TIMESTAMP) AS
    v_result VARCHAR2(4000);
    i_count  NUMBER;
    u_count  NUMBER;
    d_count  NUMBER;
    --v_t_end TIMESTAMP := LEAST(t_end, cur_state_timestamp_pkg.cur_state_time);
    v_t_end TIMESTAMP := t_end;
BEGIN
    v_result := '<table>
                      <tr>
                        <th>Table</th>
                        <th>INSERT</th>
                        <th>UPDATE</th>
                        <th>DELETE</th>
                      </tr>
                      ';
    SELECT COUNT( * ) INTO u_count
    FROM uni_logs
    WHERE change_date BETWEEN t_begin AND v_t_end
        AND change_type = 'UPDATE';
    SELECT COUNT( * ) INTO i_count
    FROM uni_logs
    WHERE change_date BETWEEN t_begin AND v_t_end
        AND change_type = 'INSERT';
    SELECT COUNT( * ) INTO d_count
    FROM uni_logs
    WHERE change_date BETWEEN t_begin AND v_t_end
        AND change_type = 'DELETE';
    v_result := v_result || '<tr>
                               <td>uni</td>
                               <td>' || i_count || '</td>
                               <td>' || u_count || '</td>
                               <td>' || d_count || '</td>
                             </tr>
                              ';
    SELECT COUNT( * ) INTO u_count
    FROM groups_logs
    WHERE change_date BETWEEN t_begin AND v_t_end
        AND change_type = 'UPDATE';
    SELECT COUNT( * ) INTO i_count
    FROM groups_logs
    WHERE change_date BETWEEN t_begin AND v_t_end
        AND change_type = 'INSERT';
    SELECT COUNT( * ) INTO d_count
    FROM groups_logs
    WHERE change_date BETWEEN t_begin AND v_t_end
        AND change_type = 'DELETE';
    v_result := v_result || '<tr>
                               <td>groups</td>
                               <td>' || i_count || '</td>
                               <td>' || u_count || '</td>
                               <td>' || d_count || '</td>
                             </tr>
                              ';
    SELECT COUNT( * ) INTO u_count
    FROM students_logs
    WHERE change_date BETWEEN t_begin AND v_t_end
        AND change_type = 'UPDATE';
    SELECT COUNT( * ) INTO i_count
    FROM students_logs
    WHERE change_date BETWEEN t_begin AND v_t_end
        AND change_type = 'INSERT';
    SELECT COUNT( * ) INTO d_count
    FROM students_logs
    WHERE change_date BETWEEN t_begin AND v_t_end
        AND change_type = 'DELETE';
    v_result := v_result || '<tr>
                               <td>students</td>
                               <td>' || i_count || '</td>
                               <td>' || u_count || '</td>
                               <td>' || d_count || '</td>
                             </tr>
                              ';
    v_result := v_result || '</table>';
    dbms_output.put_line(v_result);
END;