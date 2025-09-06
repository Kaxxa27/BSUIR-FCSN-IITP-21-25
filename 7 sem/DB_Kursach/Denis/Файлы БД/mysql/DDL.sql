-- Очистка схемы
DROP DATABASE IF EXISTS my_database;
CREATE DATABASE my_database;
USE my_database;

-- Таблица ролей пользователей
CREATE TABLE IF NOT EXISTS `role` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE,
    description TEXT
);

-- Таблица уровня лояльности
CREATE TABLE IF NOT EXISTS `loyalty_level` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    level_name VARCHAR(50) NOT NULL UNIQUE,
    required_points INT NOT NULL,
    discount_percentage DECIMAL(5, 2) NOT NULL CHECK (discount_percentage >= 0 AND discount_percentage <= 50),
    description TEXT
);

-- Таблица пользователей
CREATE TABLE IF NOT EXISTS `user` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password_hash TEXT NOT NULL,
    registration_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    role_id INT NOT NULL,
    current_loyalty_level_id INT NOT NULL,
    FOREIGN KEY (role_id) REFERENCES `role`(id) ON DELETE RESTRICT,
    FOREIGN KEY (current_loyalty_level_id) REFERENCES `loyalty_level`(id) ON DELETE RESTRICT
);

-- Таблица профиля пользователя
CREATE TABLE IF NOT EXISTS `user_profile` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    phone_number VARCHAR(15) NOT NULL UNIQUE,
    birth_date DATE NOT NULL,
    address TEXT,

    user_id INT NOT NULL UNIQUE,
    FOREIGN KEY (user_id) REFERENCES `user`(id) ON DELETE CASCADE
);

-- Таблица баллов лояльности
CREATE TABLE IF NOT EXISTS `loyalty_points` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    points INT NOT NULL DEFAULT 0,
    last_updated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    user_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES `user`(id) ON DELETE CASCADE
);

-- Таблица типов транзакции лояльности
CREATE TABLE IF NOT EXISTS `loyalty_transaction_type` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    type_name VARCHAR(20) NOT NULL UNIQUE,
    description TEXT
);

-- Таблица записей о транзакциях лояльности
CREATE TABLE IF NOT EXISTS `loyalty_transaction` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    transaction_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    points INT NOT NULL,
    description TEXT,

    transaction_type_id INT NOT NULL,
    user_id INT NOT NULL,
    FOREIGN KEY (transaction_type_id) REFERENCES `loyalty_transaction_type`(id) ON DELETE RESTRICT,
    FOREIGN KEY (user_id) REFERENCES `user`(id) ON DELETE CASCADE
);

-- Таблица отправной/конечной точки
CREATE TABLE IF NOT EXISTS `location` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE
);

-- Таблица типа транспорта
CREATE TABLE IF NOT EXISTS `transport_type` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE,
    description TEXT
);

-- Таблица перевозчиков
CREATE TABLE IF NOT EXISTS `carrier` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    legal_address TEXT,
    registration_date TIMESTAMP NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    phone_number VARCHAR(15) NOT NULL UNIQUE
);

-- Таблица транспорта
CREATE TABLE IF NOT EXISTS `transport` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    transport_type_id INT NOT NULL,
    model VARCHAR(100),
    color VARCHAR(30),
    capacity INT NOT NULL,

    carrier_id INT NOT NULL,
    FOREIGN KEY (transport_type_id) REFERENCES `transport_type`(id) ON DELETE RESTRICT,
    FOREIGN KEY (carrier_id) REFERENCES `carrier`(id) ON DELETE RESTRICT
);

-- Таблица удобств транспорта
CREATE TABLE IF NOT EXISTS `transport_feature` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    feature_name VARCHAR(100) NOT NULL,
    description TEXT
);

-- Таблица связи транспорта и удобств
CREATE TABLE IF NOT EXISTS `transport_feature_link` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    added_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,

    transport_id INT NOT NULL,
    transport_feature_id INT NOT NULL,
    FOREIGN KEY (transport_id) REFERENCES `transport`(id) ON DELETE CASCADE,
    FOREIGN KEY (transport_feature_id) REFERENCES `transport_feature`(id) ON DELETE CASCADE,
    UNIQUE (transport_id, transport_feature_id)
);

-- Таблица маршрута
CREATE TABLE IF NOT EXISTS `route` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    price DECIMAL(10, 2) NOT NULL,
    distance_km DECIMAL(10, 2),
    estimated_time_minutes INT NOT NULL,
    total_seats INT NOT NULL,
    available_seats INT NOT NULL,
    departure_time TIMESTAMP NOT NULL,
    arrival_time TIMESTAMP NOT NULL,

    departure_location_id INT NOT NULL,
    arrival_location_id INT NOT NULL,
    transport_id INT NOT NULL,
    driver_id INT NOT NULL,
    FOREIGN KEY (departure_location_id) REFERENCES `location`(id) ON DELETE RESTRICT,
    FOREIGN KEY (arrival_location_id) REFERENCES `location`(id) ON DELETE RESTRICT,
    FOREIGN KEY (transport_id) REFERENCES `transport`(id) ON DELETE RESTRICT,
    FOREIGN KEY (driver_id) REFERENCES `user`(id) ON DELETE RESTRICT
);

-- Таблица статусов билетов
CREATE TABLE IF NOT EXISTS `ticket_status` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    status_name VARCHAR(50) NOT NULL UNIQUE,
    description TEXT
);

-- Таблица бронирования билетов
CREATE TABLE IF NOT EXISTS `ticket_booking` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    seats_booked INT NOT NULL CHECK (seats_booked > 0),

    user_id INT NOT NULL,
    route_id INT NOT NULL,
    status_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES `user`(id) ON DELETE CASCADE,
    FOREIGN KEY (route_id) REFERENCES `route`(id) ON DELETE CASCADE,
    FOREIGN KEY (status_id) REFERENCES `ticket_status`(id) ON DELETE RESTRICT,
    UNIQUE (user_id, route_id)
);

-- Таблица отзывов пользователя по билету
CREATE TABLE IF NOT EXISTS `ticket_rating` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    rating INT NOT NULL CHECK (rating >= 1 AND rating <= 5),
    comment TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    ticket_booking_id INT NOT NULL UNIQUE,
    FOREIGN KEY (ticket_booking_id) REFERENCES `ticket_booking`(id) ON DELETE CASCADE
);

-- Таблица для статуса запроса в поддержку
CREATE TABLE IF NOT EXISTS `support_request_status` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    status_name VARCHAR(50) NOT NULL UNIQUE,
    description TEXT
);

-- Таблица для запроса в поддержку
CREATE TABLE IF NOT EXISTS `support_request` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    subject VARCHAR(255) NOT NULL,
    description TEXT NOT NULL,

    user_id INT NOT NULL,
    status_id INT NOT NULL,
    ticket_booking_id INT,
    FOREIGN KEY (user_id) REFERENCES `user`(id) ON DELETE CASCADE,
    FOREIGN KEY (status_id) REFERENCES `support_request_status`(id) ON DELETE RESTRICT,
    FOREIGN KEY (ticket_booking_id) REFERENCES `ticket_booking`(id) ON DELETE CASCADE
);

-- Таблица сообщения из чата поддержки
CREATE TABLE IF NOT EXISTS `support_message` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    message TEXT NOT NULL,
    sent_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    sender_id INT NOT NULL,
    support_request_id INT NOT NULL,
    FOREIGN KEY (sender_id) REFERENCES `user`(id) ON DELETE RESTRICT,
    FOREIGN KEY (support_request_id) REFERENCES `support_request`(id) ON DELETE CASCADE
);

-- Таблица для часто задаваемых вопросов 
CREATE TABLE IF NOT EXISTS `faq` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    question TEXT NOT NULL,
    answer TEXT NOT NULL,
    category VARCHAR(50) NOT NULL UNIQUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

-- Таблица уровней логирования
CREATE TABLE IF NOT EXISTS `log_level` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    level_name VARCHAR(20) NOT NULL UNIQUE,
    description TEXT
);

-- Таблица для хранения конфиденциальных логов
CREATE TABLE IF NOT EXISTS `sensitive_log` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    log_message TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,

    log_level_id INT NOT NULL,
    FOREIGN KEY (log_level_id) REFERENCES `log_level`(id) ON DELETE RESTRICT
);

-- Таблица статусов нотификаций
CREATE TABLE IF NOT EXISTS `notification_status` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    status_name VARCHAR(50) NOT NULL UNIQUE,
    description TEXT
);

-- Таблица нотификаций
CREATE TABLE IF NOT EXISTS `notifications` (
    id INT AUTO_INCREMENT PRIMARY KEY,
    message TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    sent_at TIMESTAMP NULL,

    status_id INT NOT NULL,
    FOREIGN KEY (status_id) REFERENCES `notification_status`(id) ON DELETE RESTRICT
);

