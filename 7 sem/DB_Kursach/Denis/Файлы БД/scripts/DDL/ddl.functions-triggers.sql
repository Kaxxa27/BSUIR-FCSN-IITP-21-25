-- ==================================================================================================
-- СЕКЦИЯ 1: Автообновление доступных мест в маршруте
-- ==================================================================================================

-- Функция для обновления количества доступных мест
CREATE OR REPLACE FUNCTION update_available_seats()
RETURNS TRIGGER AS $$
BEGIN
    IF TG_OP = 'INSERT' THEN
        UPDATE route
        SET available_seats = available_seats - NEW.seats_booked
        WHERE id = NEW.route_id;
    ELSIF TG_OP = 'DELETE' THEN
        UPDATE route
        SET available_seats = available_seats + OLD.seats_booked
        WHERE id = OLD.route_id;
    END IF;
    RETURN NULL;
END;
$$ LANGUAGE plpgsql;

-- Триггер для вызова функции обновления доступных мест
CREATE TRIGGER trg_update_available_seats
AFTER INSERT OR DELETE ON ticket_booking
FOR EACH ROW
EXECUTE FUNCTION update_available_seats();

-- ==================================================================================================
-- СЕКЦИЯ 2: Автообновление уровня лояльности пользователя
-- ==================================================================================================

-- Функция для обновления уровня лояльности пользователя
CREATE OR REPLACE FUNCTION update_loyalty_level()
RETURNS TRIGGER AS $$
DECLARE
    new_level_id INT;
BEGIN
    SELECT id INTO new_level_id
    FROM loyalty_level
    WHERE required_points <= (SELECT points FROM loyalty_points WHERE user_id = NEW.user_id)
    ORDER BY required_points DESC
    LIMIT 1;

    UPDATE "user"
    SET current_loyalty_level_id = new_level_id
    WHERE id = NEW.user_id;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Триггер для вызова функции обновления уровня лояльности
CREATE TRIGGER trg_update_loyalty_level
AFTER UPDATE ON loyalty_points
FOR EACH ROW
EXECUTE FUNCTION update_loyalty_level();

-- ==================================================================================================
-- СЕКЦИЯ 3: Логирование изменений статуса запросов в поддержку
-- ==================================================================================================

-- Функция для логирования изменений в таблице support_request
CREATE OR REPLACE FUNCTION log_support_request_changes()
RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO sensitive_log (log_message, log_level_id, created_at)
    VALUES (
        'Support request ID ' || NEW.id || ' status changed to ' || NEW.status_id,
        1, -- Assuming 1 corresponds to "INFO"
        CURRENT_TIMESTAMP
    );
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Триггер для логирования изменений статуса запроса в поддержку
CREATE TRIGGER trg_log_support_request_changes
AFTER UPDATE ON support_request
FOR EACH ROW
WHEN (OLD.status_id IS DISTINCT FROM NEW.status_id)
EXECUTE FUNCTION log_support_request_changes();

-- ==================================================================================================
-- СЕКЦИЯ 4: Автоматическое создание транзакции лояльности
-- ==================================================================================================

-- Функция для создания записи транзакции лояльности
CREATE OR REPLACE FUNCTION create_loyalty_transaction()
RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO loyalty_transaction (transaction_date, points, description, transaction_type_id, user_id)
    VALUES (CURRENT_TIMESTAMP, NEW.seats_booked * 10, 'Booking points', 1, NEW.user_id); -- Assuming 1 is the ID for "Purchase"

    UPDATE loyalty_points
    SET points = points + (NEW.seats_booked * 10), last_updated = CURRENT_TIMESTAMP
    WHERE user_id = NEW.user_id;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Триггер для создания транзакции лояльности при бронировании билета
CREATE TRIGGER trg_create_loyalty_transaction
AFTER INSERT ON ticket_booking
FOR EACH ROW
EXECUTE FUNCTION create_loyalty_transaction();

-- ==================================================================================================
-- СЕКЦИЯ 5: Проверка доступности мест перед бронированием
-- ==================================================================================================

-- Функция для проверки доступности мест в маршруте перед бронированием
CREATE OR REPLACE FUNCTION check_seat_availability()
RETURNS TRIGGER AS $$
BEGIN
    IF (SELECT available_seats FROM route WHERE id = NEW.route_id) < NEW.seats_booked THEN
        RAISE EXCEPTION 'Not enough available seats for route ID %', NEW.route_id;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Триггер для проверки доступности мест при добавлении бронирования
CREATE TRIGGER trg_check_seat_availability
BEFORE INSERT ON ticket_booking
FOR EACH ROW
EXECUTE FUNCTION check_seat_availability();


-- ==================================================================================================
-- СЕКЦИЯ 6: Автоудаление старых логов
-- ==================================================================================================

-- Функция для удаления логов старше 90 дней
CREATE OR REPLACE FUNCTION cleanup_old_logs()
RETURNS VOID AS $$
BEGIN
    DELETE FROM sensitive_log
    WHERE created_at < CURRENT_DATE - INTERVAL '90 days';
END;
$$ LANGUAGE plpgsql;

-- Задачу для вызова этой функции можно настроить через pgAgent или cron
