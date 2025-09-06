USE aerobooking;

SELECT * FROM user;

SELECT username, password FROM user;

SELECT * FROM city
WHERE country_id = 1;

select * from country cr
INNER JOIN city ct 
ON c.id = ct.country_id
WHERE c.name = 'Belarus';

SELECT * FROM nationality n
WHERE n.name LIKE 'B%';

SELECT * FROM country
ORDER BY name
LIMIT 3 OFFSET 5;

SELECT * FROM passport p
INNER JOIN gender g
ON p.gender_id = g.id
WHERE (p.first_name LIKE '%gen%' AND g.name = 'Male');

SELECT DISTINCT country_id from city;

SELECT COUNT(*) from city;

SELECT * FROM airport;


SELECT * FROM user;
UPDATE user 
SET username = 'Kaxxa27'
WHERE id = 1;

UPDATE user 
SET username = 'Dead', password = 'BOOM!'
WHERE id = 1 AND is_active != 1;

SELECT * FROM ticket;
UPDATE ticket
SET price = 425
WHERE id IN (2, 3);

SELECT MAX(price) FROM ticket;
SELECT MIN(price) FROM ticket;

SELECT * FROM airline
WHERE airline.name LIKE '%A%'
ORDER BY country_id DESC;
