USE aerobooking;

INSERT INTO event_type (name)
VALUES
    ('Registration'),
    ('Login'),
    ('Logout');

-- Test data about users
INSERT INTO user (username, password, email, is_active, is_admin, is_staff)
VALUES
	('Kaxxa', 'KaxxaPassword', 'Kaxxa@gmail.com', 1, 0, 0),
    ('Den', 'DenPassword', 'den@mail.ru', 1, 0, 0),
    ('admin', 'admin_password', 'admin@example.com', 1, 1, 0),
    ('manager', 'manager_password', 'manager@example.com', 1, 0, 1),
    ('testProc', 'testProc_password', 'test@example.com', 1, 0, 1);

-- Test data about nationality
INSERT INTO nationality (name) 
VALUES 
	('Belarus'),
	('Russian'),
	('American'),
    ('French'),
    ('German'),
    ('Italian'),
    ('British'),
    ('Spanish');
    
-- Test data about gender
INSERT INTO gender (name)
VALUES
    ('Male'),
    ('Female');
    
-- Test data about country
INSERT INTO country (name, code)
VALUES
    ('Belarus', 'BLR'),
    ('Australia', 'AUS'),
    ('Japan', 'JPN'),
    ('Brazil', 'BRA'),
    ('India', 'IND'),
    ('South Africa', 'ZAF'),
    ('Russia', 'RUS'),
    ('United States', 'USA'),
    ('France', 'FRA'),
    ('Germany', 'DEU'),
    ('Italy', 'ITA'),
    ('United Kingdom', 'GBR'),
    ('Spain', 'ESP'),
    ('Canada', 'CAN');

-- Test data about passport
INSERT INTO passport (first_name, last_name, birth_date, 
pass_number, identification_number, issue_date, expiry_date, 
nationality_id, gender_id, country_id, user_id)
VALUES
    ('Eugene', 'Kakhnouski', '2003-10-29', 
    'AB123456', 'ID123456', '2020-01-01', '2030-01-01',
    1, 1, 1, 1),
    ('Den', 'Konchik', '1963-05-20', 
    'CD789012', 'ID789012', '2019-12-01', '2029-12-01',
    3, 1, 8, 2),
	('Admin', 'AD', '1988-11-13', 
    'AABFNB113457', 'ID77777', '2020-01-01', '2030-01-01',
    5, 2, 10, 3),
    ('Manager', 'MAN', '1955-09-06', 
    'HG54545', 'ID909090', '2019-12-01', '2029-12-01',
    7, 2, 8, 4);
    
-- Test data about city
INSERT INTO city (name, country_id)
VALUES
    ('Minsk', 1), ('Gomel', 1),
    ('Sydney', 2), ('Melbourne', 2),
    ('Tokyo', 3), ('Osaka', 3),
    ('Rio de Janeiro', 4), ('Sao Paulo', 4),
    ('Mumbai', 5), ('Delhi', 5),
    ('Cape Town', 6), ('Johannesburg', 6),
    ('Moscow', 7), ('Saint Petersburg', 7),
    ('New York', 8), ('Los Angeles', 8),
    ('Paris', 9), ('Marseille', 9),
    ('Berlin', 10),('Munich', 10),
    ('Rome', 11), ('Milan', 11),
    ('London', 12), ('Manchester', 12),
    ('Madrid', 13), ('Barcelona', 13),
    ('Toronto', 14),('Vancouver', 14);
    
INSERT INTO airport (name, code_IATA, city_id)
VALUES
    ('Minsk National Airport', 'MSQ', 1),
    ('Gomel Airport', 'GME', 2),
    ('Sydney Kingsford Smith Airport', 'SYD', 3),
    ('Melbourne Airport', 'MEL', 4),
    ('Narita International Airport', 'NRT', 5),
    ('Kansai International Airport', 'KIX', 6),
    ('Rio de Janeiro-Galeão International Airport', 'GIG', 7),
    ('São Paulo-Guarulhos International Airport', 'GRU', 8),
    ('Chhatrapati Shivaji Maharaj International Airport', 'BOM', 9),
    ('Indira Gandhi International Airport', 'DEL', 10),
    ('Cape Town International Airport', 'CPT', 11),
    ('O.R. Tambo International Airport', 'JNB', 12),
    ('Sheremetyevo International Airport', 'SVO', 13),
    ('Pulkovo Airport', 'LED', 14),
    ('John F. Kennedy International Airport', 'JFK', 15),
    ('Los Angeles International Airport', 'LAX', 16),
    ('Charles de Gaulle Airport', 'CDG', 17),
    ('Marseille Provence Airport', 'MRS', 18),
    ('Berlin Brandenburg Airport', 'BER', 19),
    ('Munich Airport', 'MUC', 20),
    ('Leonardo da Vinci-Fiumicino Airport', 'FCO', 21),
    ('Milan Malpensa Airport', 'MXP', 22),
    ('London Heathrow Airport', 'LHR', 23),
    ('Manchester Airport', 'MAN', 24),
    ('Adolfo Suárez Madrid–Barajas Airport', 'MAD', 25),
    ('Barcelona–El Prat Airport', 'BCN', 26),
    ('Toronto Pearson International Airport', 'YYZ', 27),
    ('Vancouver International Airport', 'YVR', 28);
    
INSERT INTO plane (model, capacity, registration_number, is_under_maintenance)
VALUES
    ('Boeing 737', 150, 'N12345', 0),
    ('Airbus A320', 180, 'F67890', 0),
    ('Boeing 747', 350, 'A54321', 0),
    ('Airbus A330', 250, 'B98765', 0),
    ('Embraer E190', 100, 'C45678', 0),
    ('Boeing 777', 300, 'D87654', 0),
    ('Mig-15', 799, '444', 0),
    ('F5C', 1, '666', 0),
    ('Mig-29', 300, '777', 0);
    
INSERT INTO airline (name, description, country_id)
VALUES
    ('Belavia', 'The national airline of Belarus', 1),
    ('Qantas', 'The flag carrier airline of Australia', 2),
    ('Japan Airlines', 'The national airline of Japan', 3),
    ('LATAM Airlines', 'The largest airline in Latin America', 4),
    ('Air India', 'The flag carrier airline of India', 5),
    ('South African Airways', 'The national airline of South Africa', 6),
    ('Aeroflot', 'The flag carrier airline of Russia', 7),
    ('Delta Air Lines', 'A major airline in the United States', 8),
    ('Air France', 'The flag carrier airline of France', 9),
    ('Lufthansa', 'The largest airline in Germany', 10),
    ('Alitalia', 'The flag carrier airline of Italy', 11),
    ('British Airways', 'The flag carrier airline of the United Kingdom', 12),
    ('Iberia', 'The flag carrier airline of Spain', 13),
    ('Air Canada', 'The flag carrier airline of Canada', 14);
    
INSERT INTO flight (flight_number, airport_id, plane_id, airline_id)
VALUES
    ('B2 101', 1, 1, 1),   
    ('QF 101', 3, 2, 2),    
    ('JL 201', 5, 3, 3),    
    ('LA 301', 8, 4, 4),   
    ('AI 401', 9, 5, 5),   
    ('SA 501', 11, 6, 6);
    
INSERT INTO seatclass (name)
VALUES
    ('Economy'),
    ('Business'),
    ('First Class');

INSERT INTO seat (number, `row`, seatclass_id, plane_id)
VALUES
    ('A', 1, 1, 1),
    ('B', 1, 1, 1),
    ('C', 1, 1, 1),
    ('D', 2, 2, 1),
    ('E', 2, 2, 1),
    ('F', 2, 2, 1);

INSERT INTO ticket (reservation_date, price, flight_id, seat_id, passport_id)
VALUES
    ('2023-10-10 10:00:00', 1000, 1, 1, 1),
    ('2023-10-10 10:00:00', 1000, 1, 1, 1),
    ('2023-10-10 10:00:00', 1000, 1, 1, 1),
    ('2023-10-10 10:00:00', 1000, 1, 1, 1),
    ('2023-10-10 10:00:00', 1000, 1, 1, 1),
    ('2023-10-10 10:10:00', 1000, 1, 2, 2),
    ('2023-10-10 10:20:00', 1000, 1, 3, 3),
    
	('2023-10-15 10:00:00', 123, 2, 1, 1),
    ('2023-10-16 10:10:00', 555, 2, 2, 2),
    ('2023-10-17 10:20:00', 20, 2, 3, 3),
    
	('2023-11-10 10:00:00', 1200, 2, 4, 1),
    ('2023-12-10 10:10:00', 5000, 2, 5, 2),
    ('2023-12-10 10:20:00', 444, 2, 6, 3),
    
	('2024-11-10 10:00:00', 67, 5, 1, 1),
    ('2024-10-10 10:10:00', 13, 5, 2, 2),
    ('2024-9-10 10:20:00', 20, 5, 3, 3),
    ('2024-9-10 10:20:00', 20, 5, 3, 3),
    ('2024-9-10 10:20:00', 20, 5, 3, 3),
    ('2024-9-10 10:20:00', 20, 5, 3, 3);
    
INSERT INTO schedule (expected_departure, expected_arrival, 
real_departure, real_arrival, flight_id)
VALUES
    ('2023-10-10 12:00:00', '2023-10-10 15:00:00', NULL, NULL, 1),
    ('2023-10-11 15:30:00', '2023-10-11 18:00:00', NULL, NULL, 2),
    ('2023-10-12 12:45:00', '2023-10-12 15:30:00', NULL, NULL, 3);
    
INSERT INTO baggage (weight, ticket_id)
VALUES
    (20.5, 1),
    (15.0, 2),
    (30.0, 3);
    
-- INSERT INTO event_type (name)
-- VALUES
--     ('Registration'),
--     ('Login'),
--     ('Logout');
    
INSERT INTO event (name, datetime, description, user_id, event_type_id)
VALUES
    ('Registration', '2023-10-10 09:45:00', 'Success registration.', 1, 1),
    ('Login', '2023-10-11 14:30:00', 'Success login.', 1, 2),
    ('Logout', '2023-10-12 12:50:00', 'Success logout.', 1, 3);
