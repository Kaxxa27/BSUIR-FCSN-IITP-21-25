-- Таблица users
INSERT INTO users (username, password, email, is_active, is_admin, is_staff)
VALUES
('john_doe', 'hashedpassword1', 'john.doe@example.com', TRUE, FALSE, FALSE),
('jane_smith', 'hashedpassword2', 'jane.smith@example.com', TRUE, FALSE, TRUE),
('admin_user', 'hashedpassword3', 'admin@example.com', TRUE, TRUE, TRUE);

-- Таблица nationality
INSERT INTO nationality (name)
VALUES
('Belarusian'),
('American'),
('German');

-- Таблица gender
INSERT INTO gender (name)
VALUES
('Male'),
('Female'),
('Non-Binary');

-- Таблица country
INSERT INTO country (name, code)
VALUES
('Belarus', 'BY'),
('USA', 'US'),
('Germany', 'DE');

-- Таблица passport
INSERT INTO passport (first_name, last_name, birth_date, pass_number, identification_number, issue_date, expiry_date, nationality_id, gender_id, country_id, user_id)
VALUES
('John', 'Doe', '1990-05-20', 'AB1234567', '1234567890', '2010-06-15', '2030-06-15', 1, 1, 1, 1),
('Jane', 'Smith', '1985-03-10', 'CD7654321', '0987654321', '2015-07-01', '2025-07-01', 2, 2, 2, 2);

-- Таблица city
INSERT INTO city (name, country_id)
VALUES
('Minsk', 1),
('New York', 2),
('Berlin', 3);

-- Таблица airport
INSERT INTO airport (name, code_IATA, city_id)
VALUES
('Minsk National Airport', 'MSQ', 1),
('JFK International Airport', 'JFK', 2),
('Berlin Brandenburg Airport', 'BER', 3);

-- Таблица plane
INSERT INTO plane (model, capacity, registration_number, is_under_maintenance)
VALUES
('Boeing 737', 180, 'B737-123', FALSE),
('Airbus A320', 200, 'A320-456', TRUE);

-- Таблица airline
INSERT INTO airline (name, description, country_id)
VALUES
('Belavia', 'National airline of Belarus', 1),
('Delta Airlines', 'Major airline in the USA', 2);

-- Таблица flight
INSERT INTO flight (flight_number, airport_id, plane_id, airline_id)
VALUES
('B215', 1, 1, 1),
('D500', 2, 2, 2);

-- Таблица seatclass
INSERT INTO seatclass (name)
VALUES
('Economy'),
('Business');

-- Таблица seat
INSERT INTO seat (number, row_num, seatclass_id, plane_id)
VALUES
('A', 1, 1, 1),
('B', 1, 2, 1);

-- Таблица ticket
INSERT INTO ticket (reservation_date, price, flight_id, seat_id, passport_id)
VALUES
('2024-12-01 10:00:00', 150.00, 1, 1, 1),
('2024-12-01 12:00:00', 300.00, 2, 2, 2);

-- Таблица schedule
INSERT INTO schedule (expected_departure, expected_arrival, real_departure, real_arrival, flight_id)
VALUES
('2024-12-01 15:00:00', '2024-12-01 18:00:00', NULL, NULL, 1);

-- Таблица baggage
INSERT INTO baggage (weight, ticket_id)
VALUES
(20.5, 1),
(15.0, 2);

-- Таблица event_type
INSERT INTO event_type (name)
VALUES
('Flight Delay'),
('Cancellation');

-- Таблица event
INSERT INTO event (name, datetime, description, user_id, event_type_id)
VALUES
('Delay Announcement', '2024-12-01 14:30:00', 'Flight B215 delayed by 2 hours', 1, 1);

-- Таблица crew
INSERT INTO crew (first_name, last_name, position, nationality_id)
VALUES
('Alice', 'Brown', 'Pilot', 2),
('Bob', 'Taylor', 'Co-Pilot', 3);

-- Таблица crew_assignment
INSERT INTO crew_assignment (crew_id, flight_id, role)
VALUES
(1, 1, 'Captain'),
(2, 1, 'First Officer');

-- Таблица route
INSERT INTO route (source_airport_id, destination_airport_id, distance_km)
VALUES
(1, 2, 6800.50);

-- Таблица meal
INSERT INTO meal (name, description, price)
VALUES
('Sandwich', 'A delicious sandwich', 5.00),
('Salad', 'Fresh salad', 7.50);

-- Таблица ticket_meal
INSERT INTO ticket_meal (ticket_id, meal_id, quantity)
VALUES
(1, 1, 2),
(2, 2, 1);

-- Таблица payment
INSERT INTO payment (ticket_id, payment_date, amount, payment_method)
VALUES
(1, '2024-11-15 10:00:00', 150.00, 'Credit Card'),
(2, '2024-11-16 12:00:00', 300.00, 'PayPal');

-- Таблица review
INSERT INTO review (user_id, flight_id, rating, comments)
VALUES
(1, 1, 5, 'Great flight!'),
(2, 2, 4, 'Good service, but a bit late.');

-- Таблица notification
INSERT INTO notification (user_id, message)
VALUES
(1, 'Your flight is delayed.'),
(2, 'Your ticket is confirmed.');
