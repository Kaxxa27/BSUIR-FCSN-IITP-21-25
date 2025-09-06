-- Секция 1: Заполнение таблицы ролей
-- Вставляем данные в таблицу "role"
INSERT INTO "role" (name, description) 
VALUES ('admin', 'Administrator with full access'),
       ('user', 'Standard user with limited access');

-- Секция 2: Заполнение таблицы уровня лояльности
-- Вставляем данные в таблицу "loyalty_level"
INSERT INTO "loyalty_level" (level_name, required_points, discount_percentage, description) 
VALUES ('Bronze', 0, 0.00, 'New customer'),
       ('Silver', 100, 5.00, 'Frequent customer'),
       ('Gold', 500, 10.00, 'Premium customer');

-- Секция 3: Заполнение таблицы пользователей
-- Вставляем данные в таблицу "user"
INSERT INTO "user" (username, password_hash, role_id, current_loyalty_level_id) 
VALUES ('john_doe', '########################################################################_hashed_password_1', 1, 1),
       ('jane_smith', '########################################################################_hashed_password_2', 2, 2);

-- Секция 4: Заполнение таблицы профилей пользователей
-- Вставляем данные в таблицу "user_profile"
INSERT INTO "user_profile" (first_name, last_name, email, phone_number, birth_date, address, user_id) 
VALUES ('John', 'Doe', 'john.doe@example.com', '1234567890', '1985-04-12', '123 Main St', 1),
       ('Jane', 'Smith', 'jane.smith@example.com', '0987654321', '1990-05-23', '456 Oak St', 2);

-- Секция 5: Заполнение таблицы баллов лояльности
-- Вставляем данные в таблицу "loyalty_points"
INSERT INTO "loyalty_points" (points, user_id) 
VALUES (50, 1),
       (150, 2);

-- Секция 6: Заполнение таблицы типов транзакций лояльности
-- Вставляем данные в таблицу "loyalty_transaction_type"
INSERT INTO "loyalty_transaction_type" (type_name, description) 
VALUES ('earn', 'Earning points for actions'),
       ('redeem', 'Redeeming points for rewards');

-- Секция 7: Заполнение таблицы транзакций лояльности
-- Вставляем данные в таблицу "loyalty_transaction"
INSERT INTO "loyalty_transaction" (transaction_date, points, description, transaction_type_id, user_id) 
VALUES ('2024-11-01 10:00:00', 50, 'Purchased an item', 1, 1),
       ('2024-11-02 11:30:00', 20, 'Redeemed points for discount', 2, 2);

-- Секция 8: Заполнение таблицы локаций
-- Вставляем данные в таблицу "location"
INSERT INTO "location" (name) 
VALUES ('New York'), 
       ('Los Angeles');

-- Секция 9: Заполнение таблицы типов транспорта
-- Вставляем данные в таблицу "transport_type"
INSERT INTO "transport_type" (name, description) 
VALUES ('Bus', 'Standard bus for passengers'),
       ('Train', 'High-speed train');

-- Секция 10: Заполнение таблицы перевозчиков
-- Вставляем данные в таблицу "carrier"
INSERT INTO "carrier" (name, legal_address, registration_date, email, phone_number) 
VALUES ('Carrier A', '123 Transport Ave', '2024-01-01', 'contact@carrier-a.com', '123-456-7890'),
       ('Carrier B', '456 Logistics St', '2024-02-01', 'contact@carrier-b.com', '987-654-3210');

-- Секция 11: Заполнение таблицы транспорта
-- Вставляем данные в таблицу "transport"
INSERT INTO "transport" (transport_type_id, model, color, capacity, carrier_id) 
VALUES (1, 'Bus Model X', 'Red', 40, 1),
       (2, 'Train Model Y', 'Blue', 200, 2);

-- Секция 12: Заполнение таблицы удобств транспорта
-- Вставляем данные в таблицу "transport_feature"
INSERT INTO "transport_feature" (feature_name, description) 
VALUES ('Wi-Fi', 'Free Wi-Fi service'),
       ('AC', 'Air Conditioning');

-- Секция 13: Заполнение таблицы связи транспорта и удобств
-- Вставляем данные в таблицу "transport_feature_link"
INSERT INTO "transport_feature_link" (transport_id, transport_feature_id) 
VALUES (1, 1),
       (2, 2);

-- Секция 14: Заполнение таблицы маршрутов
-- Вставляем данные в таблицу "route"
INSERT INTO "route" (price, distance_km, estimated_time_minutes, total_seats, available_seats, departure_time, arrival_time, departure_location_id, arrival_location_id, transport_id, driver_id) 
VALUES (50.00, 350.00, 180, 40, 20, '2024-11-01 08:00:00', '2024-11-01 14:00:00', 1, 2, 1, 1),
       (75.00, 500.00, 240, 200, 150, '2024-11-02 09:00:00', '2024-11-02 15:00:00', 1, 2, 2, 2);

-- Секция 15: Заполнение таблицы статусов билетов
-- Вставляем данные в таблицу "ticket_status"
INSERT INTO "ticket_status" (status_name, description) 
VALUES ('Booked', 'Ticket has been booked'),
       ('Cancelled', 'Ticket has been cancelled'),
       ('Completed', 'Trip completed');

-- Секция 16: Заполнение таблицы бронирования билетов
-- Вставляем данные в таблицу "ticket_booking"
INSERT INTO "ticket_booking" (seats_booked, user_id, route_id, status_id) 
VALUES (2, 1, 1, 1),
       (3, 2, 2, 1);

-- Секция 17: Заполнение таблицы отзывов пользователя по билету
-- Вставляем данные в таблицу "ticket_rating"
INSERT INTO "ticket_rating" (rating, comment, ticket_booking_id) 
VALUES (5, 'Great trip!', 1),
       (4, 'Comfortable but could be improved', 2);

-- Секция 18: Заполнение таблицы статусов запросов в поддержку
-- Вставляем данные в таблицу "support_request_status"
INSERT INTO "support_request_status" (status_name, description) 
VALUES ('Open', 'The request is open'),
       ('Closed', 'The request is closed'),
       ('In Progress', 'The request is being handled');

-- Секция 19: Заполнение таблицы запросов в поддержку
-- Вставляем данные в таблицу "support_request"
INSERT INTO "support_request" (subject, description, user_id, status_id) 
VALUES ('Issue with booking', 'I need help with my booking', 1, 1),
       ('Payment issue', 'Payment not processed correctly', 2, 2);

-- Секция 20: Заполнение таблицы сообщений в чате поддержки
-- Вставляем данные в таблицу "support_message"
INSERT INTO "support_message" (message, sender_id, support_request_id) 
VALUES ('Hello, I need assistance with my booking.', 1, 1),
       ('My payment is not going through, please assist.', 2, 2);

-- Секция 21: Заполнение таблицы FAQ
-- Вставляем данные в таблицу "faq"
INSERT INTO "faq" (question, answer, category) 
VALUES ('How do I book a ticket?', 'You can book tickets via the website or app.', 'Booking'),
       ('What payment methods are accepted?', 'We accept credit cards and PayPal.', 'Payment');

-- Секция 22: Заполнение таблицы уровней логирования
-- Вставляем данные в таблицу "log_level"
INSERT INTO "log_level" (level_name, description) 
VALUES ('INFO', 'Informational logs'),
       ('ERROR', 'Error logs');

-- Секция 23: Заполнение таблицы конфиденциальных логов
-- Вставляем данные в таблицу "sensitive_log"
INSERT INTO "sensitive_log" (log_message, log_level_id) 
VALUES ('User logged in', 1),
       ('Payment failed', 2);

-- Секция 24: Заполнение таблицы статусов уведомлений
-- Вставляем данные в таблицу "notification_status"
INSERT INTO "notification_status" (status_name, description) 
VALUES ('Sent', 'Notification has been sent'),
       ('Pending', 'Notification is pending');

-- Секция 25: Заполнение таблицы уведомлений
-- Вставляем данные в таблицу "notifications"
INSERT INTO "notifications" (message, status_id) 
VALUES ('Your booking is confirmed.', 1),
       ('Your payment has been processed.', 1);
