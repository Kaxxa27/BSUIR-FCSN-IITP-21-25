-- 1. Получение всех билетов с информацией о пользователе и рейсе
SELECT 
    t.id AS ticket_id,
    u.username,
    f.flight_number,
    s.expected_departure,
    t.price
FROM ticket t
JOIN passport p ON t.passport_id = p.id
JOIN user u ON p.user_id = u.id
JOIN flight f ON t.flight_id = f.id
JOIN schedule s ON f.id = s.flight_id;

-- 2. Вывод списка аэропортов и соответствующих городов и стран
SELECT 
    a.name AS airport_name,
    c.name AS city_name,
    co.name AS country_name
FROM airport a
JOIN city c ON a.city_id = c.id
JOIN country co ON c.country_id = co.id;

-- 3. Список всех рейсов с информацией о самолете и авиакомпании
SELECT 
    f.flight_number,
    p.model AS plane_model,
    a.name AS airline_name,
    a2.name AS airport_name
FROM flight f
JOIN plane p ON f.plane_id = p.id
JOIN airline a ON f.airline_id = a.id
JOIN airport a2 ON f.airport_id = a2.id;

-- 4. Пассажиры и их билеты с указанным классом места
SELECT 
    p.first_name,
    p.last_name,
    t.id AS ticket_id,
    sc.name AS seat_class
FROM ticket t
JOIN passport p ON t.passport_id = p.id
JOIN seat s ON t.seat_id = s.id
JOIN seatclass sc ON s.seatclass_id = sc.id;

-- 5. Список пользователей, оставивших отзывы, с информацией о рейтинге
SELECT 
    u.username,
    r.flight_id,
    r.rating,
    r.comments
FROM review r
JOIN user u ON r.user_id = u.id;

-- 6. Сумма всех платежей по каждому билету
SELECT 
    t.id AS ticket_id,
    SUM(p.amount) AS total_payment
FROM payment p
JOIN ticket t ON p.ticket_id = t.id
GROUP BY t.id;

-- 7. Список экипажа для каждого рейса с их ролями
SELECT 
    f.flight_number,
    c.first_name,
    c.last_name,
    ca.role
FROM crew_assignment ca
JOIN crew c ON ca.crew_id = c.id
JOIN flight f ON ca.flight_id = f.id;

-- 8. Получение расписания рейсов с фактическим и ожидаемым временем вылета/прилета
SELECT 
    f.flight_number,
    s.expected_departure,
    s.expected_arrival,
    s.real_departure,
    s.real_arrival
FROM schedule s
JOIN flight f ON s.flight_id = f.id;

-- 9. Получение всех рейсов с указанием маршрутов (откуда и куда летят)
SELECT 
    r.id AS route_id,
    a1.name AS source_airport,
    a2.name AS destination_airport,
    r.distance_km
FROM route r
JOIN airport a1 ON r.source_airport_id = a1.id
JOIN airport a2 ON r.destination_airport_id = a2.id;

-- 10. Список пассажиров, заказавших питание на рейсе, с суммой стоимости
SELECT 
    p.first_name,
    p.last_name,
    t.id AS ticket_id,
    m.name AS meal_name,
    tm.quantity,
    (m.price * tm.quantity) AS total_meal_cost
FROM ticket_meal tm
JOIN meal m ON tm.meal_id = m.id
JOIN ticket t ON tm.ticket_id = t.id
JOIN passport p ON t.passport_id = p.id;
