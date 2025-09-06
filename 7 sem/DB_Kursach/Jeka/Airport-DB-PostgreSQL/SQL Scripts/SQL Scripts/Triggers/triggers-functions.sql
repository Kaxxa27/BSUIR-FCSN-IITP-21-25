-- Функция 1: Получить количество активных пользователей
CREATE OR REPLACE FUNCTION get_active_users_count() RETURNS INT AS $$
BEGIN
    RETURN (SELECT COUNT(*) FROM users WHERE is_active = TRUE);
END;
$$ LANGUAGE plpgsql;

-- Функция 2: Проверить, есть ли пассажир с таким паспортом
CREATE OR REPLACE FUNCTION check_passport_exists(passport_number VARCHAR) RETURNS BOOLEAN AS $$
BEGIN
    RETURN EXISTS (SELECT 1 FROM passport WHERE pass_number = passport_number);
END;
$$ LANGUAGE plpgsql;

-- Функция 3: Получить количество рейсов по авиакомпании
CREATE OR REPLACE FUNCTION get_flights_by_airline(airline_id INT) RETURNS INT AS $$
BEGIN
    RETURN (SELECT COUNT(*) FROM flight WHERE airline_id = airline_id);
END;
$$ LANGUAGE plpgsql;

-- Функция 4: Получить количество багажей по билету
CREATE OR REPLACE FUNCTION get_baggage_count(ticket_id INT) RETURNS INT AS $$
BEGIN
    RETURN (SELECT COUNT(*) FROM baggage WHERE ticket_id = ticket_id);
END;
$$ LANGUAGE plpgsql;

-- Функция 5: Получить список рейсов с задержками
CREATE OR REPLACE FUNCTION get_delayed_flights() RETURNS TABLE(flight_number VARCHAR, expected_arrival TIMESTAMP, real_arrival TIMESTAMP) AS $$
BEGIN
    RETURN QUERY
    SELECT f.flight_number, s.expected_arrival, s.real_arrival
    FROM flight f
    JOIN schedule s ON f.id = s.flight_id
    WHERE s.real_arrival > s.expected_arrival;
END;
$$ LANGUAGE plpgsql;

-- Триггер 1: Автоматическое обновление поля "is_active" при изменении пароля пользователя
CREATE OR REPLACE FUNCTION update_user_is_active() RETURNS TRIGGER AS $$
BEGIN
    IF NEW.password <> OLD.password THEN
        UPDATE users SET is_active = FALSE WHERE id = NEW.id;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER user_password_update
AFTER UPDATE OF password
ON users
FOR EACH ROW
EXECUTE FUNCTION update_user_is_active();

-- Триггер 2: Ограничение на возраст при добавлении паспорта
CREATE OR REPLACE FUNCTION check_passport_age() RETURNS TRIGGER AS $$
BEGIN
    IF NEW.birth_date > CURRENT_DATE - INTERVAL '18 years' THEN
        RAISE EXCEPTION 'Passport can only be issued for users over 18 years old';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER passport_age_check
BEFORE INSERT OR UPDATE ON passport
FOR EACH ROW
EXECUTE FUNCTION check_passport_age();

-- Триггер 3: Обновление статуса рейса при изменении расписания
CREATE OR REPLACE FUNCTION update_flight_status() RETURNS TRIGGER AS $$
BEGIN
    IF NEW.real_departure IS NOT NULL THEN
        UPDATE flight SET status = 'Departed' WHERE id = NEW.flight_id;
    END IF;
    IF NEW.real_arrival IS NOT NULL THEN
        UPDATE flight SET status = 'Arrived' WHERE id = NEW.flight_id;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER flight_status_update
AFTER UPDATE OF real_departure, real_arrival
ON schedule
FOR EACH ROW
EXECUTE FUNCTION update_flight_status();

-- Триггер 4: Уведомление пользователя о новом сообщении
CREATE OR REPLACE FUNCTION notify_user_new_message() RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO notification (user_id, message) VALUES (NEW.user_id, 'You have a new message');
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER new_message_notification
AFTER INSERT ON event
FOR EACH ROW
EXECUTE FUNCTION notify_user_new_message();

-- Триггер 5: Автоматическое добавление рейса при создании маршрута
CREATE OR REPLACE FUNCTION add_flight_for_route() RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO flight (flight_number, airport_id, plane_id, airline_id)
    VALUES (CONCAT('F', NEW.id), NEW.source_airport_id, 1, 1);  -- Пример с использованием фиктивных значений для plane_id и airline_id
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER auto_add_flight
AFTER INSERT ON route
FOR EACH ROW
EXECUTE FUNCTION add_flight_for_route();
