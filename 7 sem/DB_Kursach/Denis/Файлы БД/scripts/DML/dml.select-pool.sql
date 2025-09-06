-- 1. Информация о пользователях с деталями их лояльности и ролями
-- Этот запрос выбирает информацию о пользователях, включая их имя, электронную почту, роль и текущий уровень лояльности.
SELECT 
    u.id AS user_id,
    u.username,
    u_profile.first_name,
    u_profile.last_name,
    u_profile.email,
    r.name AS role,
    ll.level_name AS loyalty_level,
    ll.discount_percentage
FROM 
    "user" u
JOIN 
    "user_profile" u_profile ON u.id = u_profile.user_id
JOIN 
    "role" r ON u.role_id = r.id
JOIN 
    "loyalty_level" ll ON u.current_loyalty_level_id = ll.id
ORDER BY 
    u.username;

-- 2. История транзакций лояльности пользователя
-- Этот запрос получает все транзакции лояльности, совершенные пользователем, включая тип транзакции, количество баллов, дату и описание.
SELECT 
    lt.transaction_date,
    lt.points,
    lt.description AS transaction_description,
    ltt.type_name AS transaction_type,
    u.username AS user_name
FROM 
    "loyalty_transaction" lt
JOIN 
    "user" u ON lt.user_id = u.id
JOIN 
    "loyalty_transaction_type" ltt ON lt.transaction_type_id = ltt.id
ORDER BY 
    lt.transaction_date DESC;

-- 3. Заказы билетов для определенного маршрута
-- Этот запрос выбирает всех пользователей, которые забронировали билеты на определенный маршрут, включая их имя, количество забронированных мест и статус билета.
SELECT 
    tb.id AS booking_id,
    u.username,
    tb.seats_booked,
    ts.status_name AS ticket_status
FROM 
    "ticket_booking" tb
JOIN 
    "user" u ON tb.user_id = u.id
JOIN 
    "ticket_status" ts ON tb.status_id = ts.id
JOIN 
    "route" r ON tb.route_id = r.id
WHERE 
    r.id = 1; -- Замените на ID маршрута

-- 4. Оценки пользователей для маршрутов
-- Этот запрос собирает информацию о рейтингах пользователей для всех маршрутов, включая рейтинг, комментарии, и имя маршрута.
SELECT 
    r.id AS route_id,
    r.departure_time,
    r.arrival_time,
    tr.rating,
    tr.comment,
    u.username AS reviewer
FROM 
    "ticket_rating" tr
JOIN 
    "ticket_booking" tb ON tr.ticket_booking_id = tb.id
JOIN 
    "route" r ON tb.route_id = r.id
JOIN 
    "user" u ON tb.user_id = u.id
ORDER BY 
    r.departure_time, tr.created_at DESC;

-- 5. Сводка по транспортным средствам и их удобствам
-- Этот запрос выводит информацию о транспортных средствах и их удобствах, включая описание удобств и модель транспорта.
SELECT 
    t.model,
    t.color,
    tf.feature_name,
    tf.description AS feature_description
FROM 
    "transport" t
JOIN 
    "transport_feature_link" tfl ON t.id = tfl.transport_id
JOIN 
    "transport_feature" tf ON tfl.transport_feature_id = tf.id
WHERE 
    t.carrier_id = 1 -- Замените на ID перевозчика
ORDER BY 
    t.model, tf.feature_name;

-- 6. Статистика по заявкам в поддержку
-- Этот запрос собирает информацию по всем заявкам в поддержку, включая статус, тему и дату создания, а также информацию о пользователе, который создал запрос.
SELECT 
    sr.id AS support_request_id,
    sr.subject,
    sr.created_at AS request_date,
    sr.status_id,
    srs.status_name AS request_status,
    u.username AS user_name
FROM 
    "support_request" sr
JOIN 
    "support_request_status" srs ON sr.status_id = srs.id
JOIN 
    "user" u ON sr.user_id = u.id
ORDER BY 
    sr.created_at DESC;

-- 7. Пользователи с запросами в поддержку и сообщениями
-- Этот запрос собирает информацию о пользователях, которые создали запросы в поддержку, а также сообщения, связанные с этими запросами.
SELECT 
    u.username AS user_name,
    sr.subject,
    sm.message AS support_message,
    sm.sent_at AS message_sent_at
FROM 
    "support_request" sr
JOIN 
    "support_message" sm ON sr.id = sm.support_request_id
JOIN 
    "user" u ON sr.user_id = u.id
ORDER BY 
    sm.sent_at DESC;

-- 8. Статистика по нотификациям
-- Этот запрос выбирает информацию о нотификациях, включая их статус и время отправки.
SELECT 
    n.message,
    n.created_at AS notification_created,
    n.sent_at AS notification_sent,
    ns.status_name AS notification_status
FROM 
    "notifications" n
JOIN 
    "notification_status" ns ON n.status_id = ns.id
ORDER BY 
    n.sent_at DESC;

-- 9. Полная информация по маршрутам
-- Этот запрос объединяет данные о маршрутах, транспортных средствах, отправных и конечных точках, а также информации о водителе.
SELECT 
    r.id AS route_id,
    r.departure_time,
    r.arrival_time,
    r.price,
    r.distance_km,
    r.estimated_time_minutes,
    dl.name AS departure_location,
    al.name AS arrival_location,
    t.model AS transport_model,
    t.color AS transport_color,
    u.username AS driver
FROM 
    "route" r
JOIN 
    "location" dl ON r.departure_location_id = dl.id
JOIN 
    "location" al ON r.arrival_location_id = al.id
JOIN 
    "transport" t ON r.transport_id = t.id
JOIN 
    "user" u ON r.driver_id = u.id
ORDER BY 
    r.departure_time;
