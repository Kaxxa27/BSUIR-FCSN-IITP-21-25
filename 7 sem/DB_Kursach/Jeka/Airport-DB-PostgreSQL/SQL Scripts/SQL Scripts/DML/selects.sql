-- 1. Получение информации о пользователях и их национальности
SELECT u.id AS user_id, u.username, u.email, n.name AS nationality
FROM users u
LEFT JOIN passport p ON u.id = p.user_id
LEFT JOIN nationality n ON p.nationality_id = n.id;

-- 2. Список всех рейсов с их авиакомпаниями и аэропортами отправления
SELECT f.id AS flight_id, f.flight_number, a.name AS airline_name, ap.name AS airport_name
FROM flight f
JOIN airline a ON f.airline_id = a.id
JOIN airport ap ON f.airport_id = ap.id;

-- 3. Информация о билетах с местами и классом
SELECT t.id AS ticket_id, t.price, s.number AS seat_number, sc.name AS seat_class
FROM ticket t
JOIN seat s ON t.seat_id = s.id
JOIN seatclass sc ON s.seatclass_id = sc.id;

-- 4. Пассажиры с паспортами и их гражданством
SELECT p.id AS passport_id, p.first_name, p.last_name, n.name AS nationality_name
FROM passport p
JOIN nationality n ON p.nationality_id = n.id;

-- 5. Рейсы с информацией о самолётах и аэропортах отправления
SELECT f.flight_number, pl.model AS plane_model, ap.name AS airport_name
FROM flight f
JOIN plane pl ON f.plane_id = pl.id
JOIN airport ap ON f.airport_id = ap.id;

-- 6. Расписание рейсов с информацией о фактических и ожидаемых времени отправления
SELECT f.flight_number, s.expected_departure, s.expected_arrival, s.real_departure, s.real_arrival
FROM schedule s
JOIN flight f ON s.flight_id = f.id;

-- 7. Список всех экипажей с их ролями на рейсах
SELECT c.first_name, c.last_name, ca.role, f.flight_number
FROM crew_assignment ca
JOIN crew c ON ca.crew_id = c.id
JOIN flight f ON ca.flight_id = f.id;

-- 8. Питание на билетах с их ценой и количеством
SELECT t.id AS ticket_id, m.name AS meal_name, tm.quantity, m.price
FROM ticket_meal tm
JOIN meal m ON tm.meal_id = m.id
JOIN ticket t ON tm.ticket_id = t.id;

-- 9. Оценки пользователей для рейсов
SELECT u.username, r.rating, r.comments, f.flight_number
FROM review r
JOIN users u ON r.user_id = u.id
JOIN flight f ON r.flight_id = f.id;

-- 10. Уведомления пользователей с их статусом прочтения
SELECT u.username, n.message, n.is_read
FROM notification n
JOIN users u ON n.user_id = u.id;

-- Тестирования триггера 
-- 1. Вставляем тестовую запись в таблицу event, чтобы сработал триггер
INSERT INTO event (name, datetime, description, user_id, event_type_id)
VALUES ('Test Event', NOW(), 'This is a test event for notification trigger', 1, 1);

-- 2. Проверяем, что в таблице notification появилось новое уведомление
SELECT * FROM notification WHERE user_id = 1 AND message = 'You have a new message';
