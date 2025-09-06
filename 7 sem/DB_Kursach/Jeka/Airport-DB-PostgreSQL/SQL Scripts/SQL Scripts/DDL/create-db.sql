DROP SCHEMA public CASCADE;
CREATE SCHEMA public;
GRANT ALL ON SCHEMA public TO public;


-- Table "user"
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(45) NOT NULL UNIQUE,
    password VARCHAR(64) NOT NULL,
    email VARCHAR(254) NOT NULL UNIQUE,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    is_admin BOOLEAN NOT NULL DEFAULT FALSE,
    is_staff BOOLEAN NOT NULL DEFAULT FALSE,
    CHECK (LENGTH(username) > 0),
    CHECK (LENGTH(password) > 0)
);

-- Table nationality
CREATE TABLE nationality (
    id SERIAL PRIMARY KEY,
    name VARCHAR(45) NOT NULL UNIQUE,
    CHECK (LENGTH(name) > 0)
);

-- Table gender
CREATE TABLE gender (
    id SERIAL PRIMARY KEY,
    name VARCHAR(45) NOT NULL UNIQUE,
    CHECK (LENGTH(name) > 0)
);

-- Table country
CREATE TABLE country (
    id SERIAL PRIMARY KEY,
    name VARCHAR(45) NOT NULL UNIQUE,
    code VARCHAR(3) NOT NULL UNIQUE,
    CHECK (LENGTH(name) > 0),
    CHECK (LENGTH(code) > 0)
);

-- Table passport
CREATE TABLE passport (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(45) NOT NULL,
    last_name VARCHAR(45) NOT NULL,
    birth_date DATE NOT NULL,
    pass_number VARCHAR(45) NOT NULL,
    identification_number VARCHAR(45) NOT NULL,
    issue_date DATE NOT NULL,
    expiry_date DATE NOT NULL,
    nationality_id INT NOT NULL REFERENCES nationality(id),
    gender_id INT NOT NULL REFERENCES gender(id),
    country_id INT NOT NULL REFERENCES country(id),
    user_id INT REFERENCES users(id) ON DELETE SET NULL,
    CHECK (expiry_date > issue_date)
);

-- Table city
CREATE TABLE city (
    id SERIAL PRIMARY KEY,
    name VARCHAR(45) NOT NULL,
    country_id INT NOT NULL REFERENCES country(id),
    CHECK (LENGTH(name) > 0)
);

-- Table airport
CREATE TABLE airport (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    code_IATA VARCHAR(3) NOT NULL,
    city_id INT NOT NULL UNIQUE REFERENCES city(id),
    CHECK (LENGTH(name) > 0),
    CHECK (LENGTH(code_IATA) > 0)
);

-- Table plane
CREATE TABLE plane (
    id SERIAL PRIMARY KEY,
    model VARCHAR(45) NOT NULL,
    capacity INT NOT NULL,
    registration_number VARCHAR(20) NOT NULL UNIQUE,
    is_under_maintenance BOOLEAN NOT NULL,
    CHECK (capacity > 0)
);

-- Table airline
CREATE TABLE airline (
    id SERIAL PRIMARY KEY,
    name VARCHAR(45) NOT NULL,
    description TEXT,
    country_id INT NOT NULL REFERENCES country(id),
    CHECK (LENGTH(name) > 0)
);

-- Table flight
CREATE TABLE flight (
    id SERIAL PRIMARY KEY,
    flight_number VARCHAR(10) NOT NULL,
    airport_id INT NOT NULL REFERENCES airport(id),
    plane_id INT NOT NULL REFERENCES plane(id),
    airline_id INT NOT NULL REFERENCES airline(id),
    CHECK (LENGTH(flight_number) > 0)
);

-- Table seatclass
CREATE TABLE seatclass (
    id SERIAL PRIMARY KEY,
    name VARCHAR(45) NOT NULL CHECK (LENGTH(name) > 0)
);

-- Table seat
CREATE TABLE seat (
    id SERIAL PRIMARY KEY,
    number VARCHAR(1) NOT NULL,
    row_num INT NOT NULL,
    seatclass_id INT NOT NULL REFERENCES seatclass(id),
    plane_id INT NOT NULL REFERENCES plane(id) ON DELETE CASCADE
);

-- Table ticket
CREATE TABLE ticket (
    id SERIAL PRIMARY KEY,
    reservation_date TIMESTAMP NOT NULL,
    price DECIMAL(10,3) NOT NULL,
    flight_id INT NOT NULL REFERENCES flight(id),
    seat_id INT NOT NULL REFERENCES seat(id),
    passport_id INT NOT NULL REFERENCES passport(id),
    CHECK (price >= 0)
);

-- Table schedule
CREATE TABLE schedule (
    id SERIAL PRIMARY KEY,
    expected_departure TIMESTAMP NOT NULL,
    expected_arrival TIMESTAMP NOT NULL,
    real_departure TIMESTAMP,
    real_arrival TIMESTAMP,
    flight_id INT NOT NULL UNIQUE REFERENCES flight(id) ON DELETE CASCADE,
    CHECK (expected_departure < expected_arrival),
    CHECK (real_departure IS NULL OR real_departure < real_arrival)
);

-- Table baggage
CREATE TABLE baggage (
    id SERIAL PRIMARY KEY,
    weight DECIMAL(5,3) NOT NULL,
    ticket_id INT REFERENCES ticket(id) ON DELETE SET NULL,
    CHECK (weight >= 0)
);

-- Table event_type
CREATE TABLE event_type (
    id SERIAL PRIMARY KEY,
    name VARCHAR(45) NOT NULL UNIQUE CHECK (LENGTH(name) > 0)
);

-- Table event
CREATE TABLE event (
    id SERIAL PRIMARY KEY,
    name VARCHAR(45) NOT NULL,
    datetime TIMESTAMP NOT NULL,
    description TEXT,
    user_id INT REFERENCES users(id) ON DELETE SET NULL,
    event_type_id INT NOT NULL REFERENCES event_type(id),
    CHECK (LENGTH(name) > 0)
);

-- Table crew
CREATE TABLE crew (
    id SERIAL PRIMARY KEY,
    first_name VARCHAR(45) NOT NULL,
    last_name VARCHAR(45) NOT NULL,
    position VARCHAR(45) NOT NULL,
    nationality_id INT REFERENCES nationality(id),
    CHECK (LENGTH(first_name) > 0),
    CHECK (LENGTH(last_name) > 0)
);

-- Table crew_assignment
CREATE TABLE crew_assignment (
    id SERIAL PRIMARY KEY,
    crew_id INT NOT NULL REFERENCES crew(id) ON DELETE CASCADE,
    flight_id INT NOT NULL REFERENCES flight(id) ON DELETE CASCADE,
    role VARCHAR(45) NOT NULL
);

-- Table route
CREATE TABLE route (
    id SERIAL PRIMARY KEY,
    source_airport_id INT NOT NULL REFERENCES airport(id),
    destination_airport_id INT NOT NULL REFERENCES airport(id),
    distance_km DECIMAL(8,2) NOT NULL CHECK (distance_km > 0)
);

-- Table meal
CREATE TABLE meal (
    id SERIAL PRIMARY KEY,
    name VARCHAR(45) NOT NULL UNIQUE,
    description TEXT,
    price DECIMAL(5,2) NOT NULL CHECK (price >= 0)
);

-- Table ticket_meal
CREATE TABLE ticket_meal (
    id SERIAL PRIMARY KEY,
    ticket_id INT NOT NULL REFERENCES ticket(id) ON DELETE CASCADE,
    meal_id INT NOT NULL REFERENCES meal(id) ON DELETE CASCADE,
    quantity INT NOT NULL DEFAULT 1 CHECK (quantity > 0)
);

-- Table payment
CREATE TABLE payment (
    id SERIAL PRIMARY KEY,
    ticket_id INT NOT NULL REFERENCES ticket(id),
    payment_date TIMESTAMP NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    payment_method VARCHAR(45) NOT NULL,
    CHECK (amount >= 0)
);

-- Table review
CREATE TABLE review (
    id SERIAL PRIMARY KEY,
    user_id INT NOT NULL REFERENCES users(id),
    flight_id INT NOT NULL REFERENCES flight(id),
    rating INT NOT NULL CHECK (rating BETWEEN 1 AND 5),
    comments TEXT
);

-- Table notification
CREATE TABLE notification (
    id SERIAL PRIMARY KEY,
    user_id INT NOT NULL REFERENCES users(id),
    message TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_read BOOLEAN DEFAULT FALSE
);
