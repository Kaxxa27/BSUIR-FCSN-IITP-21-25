DROP SCHEMA public CASCADE;
CREATE SCHEMA public;
GRANT ALL ON SCHEMA public TO public;

-- Таблица ролей пользователей
CREATE TABLE IF NOT EXISTS "role" (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE,
    description TEXT CHECK (LENGTH(description) <= 500)
);

-- Таблица уровня лояльности
CREATE TABLE IF NOT EXISTS "loyalty_level" (
    id SERIAL PRIMARY KEY,
    level_name VARCHAR(50) NOT NULL UNIQUE,
    required_points INT NOT NULL CHECK (required_points >= 0),
    discount_percentage DECIMAL(5, 2) NOT NULL CHECK (discount_percentage >= 0 AND discount_percentage <= 50),
    description TEXT CHECK (LENGTH(description) <= 500)
);

-- Таблица пользователей
CREATE TABLE IF NOT EXISTS "user" (
    id SERIAL PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE CHECK (LENGTH(username) >= 3),
    password_hash TEXT NOT NULL CHECK (LENGTH(password_hash) >= 60),
    registration_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	
    role_id INT NOT NULL REFERENCES "role"(id) ON DELETE RESTRICT,
    current_loyalty_level_id INT NOT NULL REFERENCES "loyalty_level"(id) ON DELETE RESTRICT
);

-- Таблица профиля пользователя
CREATE TABLE IF NOT EXISTS "user_profile" (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL CHECK (LENGTH(first_name) > 0),
    last_name VARCHAR(50) NOT NULL CHECK (LENGTH(last_name) > 0),
    email VARCHAR(100) NOT NULL UNIQUE CHECK (POSITION('@' IN email) > 0),
    phone_number VARCHAR(15) NOT NULL UNIQUE CHECK (LENGTH(phone_number) >= 10),
    birth_date DATE NOT NULL CHECK (birth_date <= CURRENT_DATE),
    address TEXT CHECK (LENGTH(address) <= 500),
	
    user_id INT NOT NULL UNIQUE REFERENCES "user"(id) ON DELETE CASCADE
);

-- Таблица баллов лояльности
CREATE TABLE IF NOT EXISTS "loyalty_points" (
    id SERIAL PRIMARY KEY,
    points INT NOT NULL DEFAULT 0 CHECK (points >= 0),
    last_updated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	
    user_id INT NOT NULL REFERENCES "user"(id) ON DELETE CASCADE
);

-- Таблица типов транзакции лояльности
CREATE TABLE IF NOT EXISTS "loyalty_transaction_type" (
    id SERIAL PRIMARY KEY,
    type_name VARCHAR(20) NOT NULL UNIQUE CHECK (LENGTH(type_name) > 0),
    description TEXT CHECK (LENGTH(description) <= 500)
);

-- Таблица записей о транзакциях лояльности
CREATE TABLE IF NOT EXISTS "loyalty_transaction" (
    id SERIAL PRIMARY KEY,
    transaction_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    points INT NOT NULL CHECK (points <> 0),
    description TEXT CHECK (LENGTH(description) <= 500),
	
    transaction_type_id INT NOT NULL REFERENCES "loyalty_transaction_type"(id) ON DELETE RESTRICT,
    user_id INT NOT NULL REFERENCES "user"(id) ON DELETE CASCADE
);

-- Таблица отправной/конечной точки
CREATE TABLE IF NOT EXISTS "location" (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE CHECK (LENGTH(name) > 0)
);

-- Таблица типа транспорта
CREATE TABLE IF NOT EXISTS "transport_type" (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE CHECK (LENGTH(name) > 0),
    description TEXT CHECK (LENGTH(description) <= 500)
);

-- Таблица перевозчиков
CREATE TABLE IF NOT EXISTS "carrier" (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL CHECK (LENGTH(name) > 0),
    legal_address TEXT CHECK (LENGTH(legal_address) <= 500),
    registration_date TIMESTAMP NOT NULL CHECK (registration_date <= CURRENT_TIMESTAMP),
    email VARCHAR(100) NOT NULL UNIQUE CHECK (POSITION('@' IN email) > 0),
    phone_number VARCHAR(15) NOT NULL UNIQUE CHECK (LENGTH(phone_number) >= 10)
);

-- Таблица транспорта
CREATE TABLE IF NOT EXISTS "transport" (
    id SERIAL PRIMARY KEY,
    model VARCHAR(100) CHECK (LENGTH(model) > 0),
    color VARCHAR(30),
    capacity INT NOT NULL CHECK (capacity > 0),
	
	transport_type_id INT NOT NULL REFERENCES "transport_type"(id) ON DELETE RESTRICT,
    carrier_id INT NOT NULL REFERENCES "carrier"(id) ON DELETE RESTRICT
);

-- Таблица удобств транспорта
CREATE TABLE IF NOT EXISTS "transport_feature" (
    id SERIAL PRIMARY KEY,
    feature_name VARCHAR(100) NOT NULL CHECK (LENGTH(feature_name) > 0),
    description TEXT CHECK (LENGTH(description) <= 500)
);

-- Таблица связи транспорта и удобств
CREATE TABLE IF NOT EXISTS "transport_feature_link" (
    id SERIAL PRIMARY KEY,
    added_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
	
    transport_id INT NOT NULL REFERENCES "transport"(id) ON DELETE CASCADE,
    transport_feature_id INT NOT NULL REFERENCES "transport_feature"(id) ON DELETE CASCADE,
    UNIQUE (transport_id, transport_feature_id)
);

-- Таблица для маршрута
CREATE TABLE IF NOT EXISTS "route" (
    id SERIAL PRIMARY KEY,
    price DECIMAL(10, 2) NOT NULL CHECK (price >= 0),
    distance_km DECIMAL(10, 2) CHECK (distance_km >= 0),
    estimated_time_minutes INT NOT NULL CHECK (estimated_time_minutes > 0),
    total_seats INT NOT NULL CHECK (total_seats > 0),
    available_seats INT NOT NULL CHECK (available_seats >= 0 AND available_seats <= total_seats),
    departure_time TIMESTAMP NOT NULL,
    arrival_time TIMESTAMP NOT NULL CHECK (arrival_time > departure_time),
	
    departure_location_id INT NOT NULL REFERENCES "location"(id) ON DELETE RESTRICT,
    arrival_location_id INT NOT NULL REFERENCES "location"(id) ON DELETE RESTRICT,
    transport_id INT NOT NULL REFERENCES "transport"(id) ON DELETE RESTRICT,
    driver_id INT NOT NULL REFERENCES "user"(id) ON DELETE RESTRICT
);

-- Таблица статусов билетов
CREATE TABLE IF NOT EXISTS "ticket_status" (
    id SERIAL PRIMARY KEY,
    status_name VARCHAR(50) NOT NULL UNIQUE CHECK (LENGTH(status_name) > 0),
    description TEXT CHECK (LENGTH(description) <= 500)
);

-- Таблица бронирования билетов
CREATE TABLE IF NOT EXISTS "ticket_booking" (
    id SERIAL PRIMARY KEY,
	seats_booked INT NOT NULL CHECK (seats_booked > 0),
	
    user_id INT NOT NULL REFERENCES "user"(id) ON DELETE CASCADE,
    route_id INT NOT NULL REFERENCES "route"(id) ON DELETE CASCADE,
	status_id INT NOT NULL REFERENCES "ticket_status"(id) ON DELETE RESTRICT,
    UNIQUE (user_id, route_id)
);

-- Таблица отзывов пользователя по билету
CREATE TABLE IF NOT EXISTS "ticket_rating" (
    id SERIAL PRIMARY KEY,
    rating INT CHECK (rating >= 1 AND rating <= 5) NOT NULL,
    comment TEXT CHECK (LENGTH(comment) <= 1000),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	
    ticket_booking_id INT NOT NULL UNIQUE REFERENCES "ticket_booking"(id) ON DELETE CASCADE
);

-- Таблица для статуса запроса в поддержку
CREATE TABLE IF NOT EXISTS "support_request_status" (
    id SERIAL PRIMARY KEY,
    status_name VARCHAR(50) NOT NULL UNIQUE CHECK (LENGTH(status_name) > 0),
    description TEXT CHECK (LENGTH(description) <= 500)
);

-- Таблица для запроса в поддержку
CREATE TABLE IF NOT EXISTS "support_request" (
    id SERIAL PRIMARY KEY,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    subject VARCHAR(255) NOT NULL CHECK (LENGTH(subject) > 0),
    description TEXT NOT NULL CHECK (LENGTH(description) > 0),
	
    user_id INT NOT NULL REFERENCES "user"(id) ON DELETE CASCADE,
    status_id INT NOT NULL REFERENCES "support_request_status"(id) ON DELETE RESTRICT,
    ticket_booking_id INT REFERENCES "ticket_booking"(id) ON DELETE CASCADE
);


-- Таблица сообщения из чата поддержки
CREATE TABLE IF NOT EXISTS "support_message" (
    id SERIAL PRIMARY KEY,
    message TEXT NOT NULL CHECK (LENGTH(message) > 0),
    sent_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	
    sender_id INT NOT NULL REFERENCES "user"(id) ON DELETE RESTRICT,
    support_request_id INT NOT NULL REFERENCES "support_request"(id) ON DELETE CASCADE
);


-- Таблица для часто задаваемых вопросов
CREATE TABLE IF NOT EXISTS "faq" (
    id SERIAL PRIMARY KEY,
    question TEXT NOT NULL CHECK (LENGTH(question) > 0 AND LENGTH(question) <= 1000),
    answer TEXT NOT NULL CHECK (LENGTH(answer) > 0 AND LENGTH(answer) <= 2000),
    category VARCHAR(50) NOT NULL UNIQUE CHECK (LENGTH(category) > 0),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Таблица уровней логирования
CREATE TABLE IF NOT EXISTS "log_level" (
    id SERIAL PRIMARY KEY,
    level_name VARCHAR(20) NOT NULL UNIQUE CHECK (LENGTH(level_name) > 0),
    description TEXT CHECK (LENGTH(description) <= 500)
);

-- Таблица для хранения конфиденциальных логов
CREATE TABLE IF NOT EXISTS "sensitive_log" (
    id SERIAL PRIMARY KEY,
    log_message TEXT NOT NULL CHECK (LENGTH(log_message) > 0),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	
    log_level_id INT NOT NULL REFERENCES "log_level"(id) ON DELETE RESTRICT
);


-- Таблица статусов нотификаций
CREATE TABLE IF NOT EXISTS "notification_status" (
    id SERIAL PRIMARY KEY,
    status_name VARCHAR(50) NOT NULL UNIQUE CHECK (LENGTH(status_name) > 0),
    description TEXT CHECK (LENGTH(description) <= 500)
);

-- Таблица нотификаций
CREATE TABLE IF NOT EXISTS "notifications" (
    id SERIAL PRIMARY KEY,
    message TEXT NOT NULL CHECK (LENGTH(message) > 0 AND LENGTH(message) <= 1000),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    sent_at TIMESTAMP,
	
    status_id INT NOT NULL REFERENCES "notification_status"(id) ON DELETE RESTRICT
);