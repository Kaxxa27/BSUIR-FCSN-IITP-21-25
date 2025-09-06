 CREATE DATABASE IF NOT EXISTS AeroBooking;

USE AeroBooking;

-- Table user
CREATE TABLE IF NOT EXISTS user (
	id INT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(45) NOT NULL UNIQUE,
    password CHAR(64) NOT NULL,
    email VARCHAR(254) NOT NULL UNIQUE,
	is_active TINYINT(1) NOT NULL DEFAULT 1,
	is_admin TINYINT(1) NOT NULL DEFAULT 0,
	is_staff TINYINT(1) NOT NULL DEFAULT 0,
    
	CHECK (CHAR_LENGTH(username) > 0),
    CHECK (CHAR_LENGTH(password) > 0),
    CHECK (email REGEXP '^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}$'),
    CHECK (is_active IN (0, 1)),
    CHECK (is_admin IN (0, 1)),
    CHECK (is_staff IN (0, 1))
);

-- Table nationality
CREATE TABLE IF NOT EXISTS nationality(
	id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(45) NOT NULL UNIQUE,
    
    CHECK (CHAR_LENGTH(name) > 0)
);

-- Table gender
CREATE TABLE IF NOT EXISTS gender(
	id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(45) NOT NULL UNIQUE,
    
	CHECK (CHAR_LENGTH(name) > 0)
);

-- Table country
CREATE TABLE IF NOT EXISTS country(
	id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(45) NOT NULL UNIQUE,
    code CHAR(3) NOT NULL UNIQUE,
    
	CHECK (CHAR_LENGTH(name) > 0),
	CHECK (CHAR_LENGTH(code) > 0)
);

-- Table passport
CREATE TABLE IF NOT EXISTS passport(
	id INT PRIMARY KEY AUTO_INCREMENT,
    first_name VARCHAR(45) NOT NULL,
    last_name VARCHAR(45) NOT NULL,
    birth_date DATE NOT NULL, 
    pass_number VARCHAR(45) NOT NULL,
    identification_number VARCHAR(45) NOT NULL,
    issue_date DATE NOT NULL,
    expiry_date DATE NOT NULL,
    nationality_id INT NOT NULL,
    gender_id INT NOT NULL,
    country_id INT NOT NULL,
    user_id INT,
    
    FOREIGN KEY (nationality_id) REFERENCES nationality(id),
    FOREIGN KEY (gender_id) REFERENCES gender(id),
    FOREIGN KEY (country_id) REFERENCES country(id),
    FOREIGN KEY (user_id) REFERENCES user(id) ON DELETE SET NULL,
    
    CHECK (CHAR_LENGTH(first_name) > 0),
    CHECK (CHAR_LENGTH(last_name) > 0),
    CHECK (CHAR_LENGTH(pass_number) > 0),
    CHECK (CHAR_LENGTH(identification_number) > 0),
    CHECK (expiry_date > issue_date)
);

-- Table city
CREATE TABLE IF NOT EXISTS city(
	id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(45) NOT NULL,
    country_id INT NOT NULL,
    
    FOREIGN KEY (country_id) REFERENCES country(id),
    
	CHECK (CHAR_LENGTH(name) > 0)
);

-- Table airport
CREATE TABLE IF NOT EXISTS airport(
	id INT PRIMARY KEY AUTO_INCREMENT,
	name VARCHAR(100) NOT NULL,
	code_IATA CHAR(3) NOT NULL,
	city_id INT NOT NULL UNIQUE,
    
    FOREIGN KEY (city_id) REFERENCES city(id),
    
    CHECK (CHAR_LENGTH(name) > 0),
    CHECK (CHAR_LENGTH(code_IATA) > 0)
);

-- Table plane
CREATE TABLE IF NOT EXISTS plane(
	id INT PRIMARY KEY AUTO_INCREMENT,
	model VARCHAR(45) NOT NULL,
	capacity INT NOT NULL,
	registration_number VARCHAR(20) NOT NULL UNIQUE,
    is_under_maintenance INT NOT NULL,
    
    CHECK (CHAR_LENGTH(model) > 0),
    CHECK (CHAR_LENGTH(registration_number) > 0),
    CHECK (capacity > 0)
);

-- Table airline
CREATE TABLE IF NOT EXISTS airline(
	id INT PRIMARY KEY AUTO_INCREMENT,
	name VARCHAR(45) NOT NULL,
	description TEXT,
    country_id INT NOT NULL,
    
    FOREIGN KEY (country_id) REFERENCES country(id),
    
    CHECK (CHAR_LENGTH(name) > 0)
);

-- Table flight
CREATE TABLE IF NOT EXISTS flight(
  id INT PRIMARY KEY AUTO_INCREMENT,
  flight_number VARCHAR(10) NOT NULL,
  airport_id INT NOT NULL,
  plane_id INT NOT NULL,
  airline_id INT NOT NULL,
  
  FOREIGN KEY (airport_id) REFERENCES airport(id),
  FOREIGN KEY (plane_id) REFERENCES plane(id),
  FOREIGN KEY (airline_id) REFERENCES airline(id),
  
  CHECK (CHAR_LENGTH(flight_number) > 0)
);

-- Table seatclass
CREATE TABLE IF NOT EXISTS seatclass(
	id INT PRIMARY KEY AUTO_INCREMENT,
	name VARCHAR(45) NOT NULL
    
	CHECK (CHAR_LENGTH(name) > 0)
);

-- Table seat, `row` because that is keyword
CREATE TABLE IF NOT EXISTS seat(
	id INT PRIMARY KEY AUTO_INCREMENT,
    number CHAR(1) NOT NULL,
    `row` INT NOT NULL,
    seatclass_id INT NOT NULL,
    plane_id INT NOT NULL,
    
    FOREIGN KEY (seatclass_id) REFERENCES seatclass(id),
    FOREIGN KEY (plane_id) REFERENCES plane(id) ON DELETE CASCADE
);

-- Table ticket 
CREATE TABLE IF NOT EXISTS ticket(
	id INT PRIMARY KEY AUTO_INCREMENT,
	reservation_date DATETIME NOT NULL,
    price DECIMAL(10,3) NOT NULL,
	flight_id INT NOT NULL,
	seat_id INT NOT NULL,
	passport_id INT NOT NULL,
    
    FOREIGN KEY (flight_id) REFERENCES flight(id),
    FOREIGN KEY (seat_id) REFERENCES seat(id),
    FOREIGN KEY (passport_id) REFERENCES passport(id),
    
    CHECK (price >= 0)
);

-- Table schedule 
CREATE TABLE IF NOT EXISTS schedule(
	id INT PRIMARY KEY AUTO_INCREMENT,
	expected_departure TIMESTAMP NOT NULL,
	expected_arrival TIMESTAMP NOT NULL,
	real_departure TIMESTAMP,
	real_arrival TIMESTAMP,
	flight_id INT NOT NULL UNIQUE,
    
	FOREIGN KEY (flight_id) REFERENCES flight(id) ON DELETE CASCADE,
    
    CHECK (expected_departure < expected_arrival),
    CHECK (real_departure IS NULL OR real_departure < real_arrival)
);

-- Table baggage
CREATE TABLE IF NOT EXISTS baggage(
	id INT PRIMARY KEY AUTO_INCREMENT,
	weight DECIMAL(5,3) NOT NULL,
	ticket_id INT,
    
    FOREIGN KEY (ticket_id) REFERENCES ticket(id) ON DELETE SET NULL,

	CHECK (weight >= 0)
);

-- Table event type
CREATE TABLE IF NOT EXISTS event_type(
	id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(45) NOT NULL UNIQUE,
    
    CHECK(CHAR_LENGTH(name) > 0)
);

-- Table event
CREATE TABLE IF NOT EXISTS event(
	id INT PRIMARY KEY AUTO_INCREMENT,
	name VARCHAR(45) NOT NULL,
	datetime TIMESTAMP NOT NULL,
	description TEXT,
	user_id INT,
	event_type_id INT NOT NULL,
    
    FOREIGN KEY (user_id) REFERENCES user(id) ON DELETE SET NULL,
    FOREIGN KEY (event_type_id) REFERENCES event_type(id),
    
	CHECK(CHAR_LENGTH(name) > 0)
);





