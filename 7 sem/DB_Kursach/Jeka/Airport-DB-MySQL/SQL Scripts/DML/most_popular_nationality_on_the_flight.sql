USE aerobooking;

-- START QUERY
WITH num_ntl AS (
	SELECT flight_number, nationality_id, COUNT(*) as num_of_n
	FROM ticket t 
	INNER JOIN flight f ON f.id = t.flight_id 
	INNER JOIN passport p ON p.id = t.passport_id
	INNER JOIN nationality n ON n.id = p.nationality_id
	GROUP BY flight_number, nationality_id
	ORDER BY flight_number
)

SELECT flight_number, name as popular_nat, num_of_n
FROM (
	SELECT flight_number, name, num_of_n, 
    dense_rank() over (partition by flight_number order by num_of_n desc) as nat_rank,
    row_number() over (partition by flight_number order by num_of_n desc) as row_num
	FROM num_ntl
	INNER JOIN nationality n ON n.id = num_ntl.nationality_id
) as res
WHERE row_num = 1;
-- END QUERY


-- Data
SELECT flight_number, nationality_id, n.name
	FROM ticket t 
	INNER JOIN flight f ON f.id = t.flight_id 
	INNER JOIN passport p ON p.id = t.passport_id
	INNER JOIN nationality n ON n.id = p.nationality_id
    ORDER BY flight_number;