USE aerobooking;


-- update_ticket_price TRIGGER 
select * from seatclass;
select * from ticket
order by price;
--  DROP TRIGGER update_ticket_price;

DELIMITER //
CREATE TRIGGER update_ticket_price
BEFORE INSERT ON ticket
FOR EACH ROW
BEGIN
    DECLARE seatclass_multiplier DECIMAL(10,2) DEFAULT 1;
    
    -- Получаем множитель стоимости класса места
    SELECT CASE sc.name
           WHEN 'Economy' THEN 1
           WHEN 'Business' THEN 1.5
           WHEN 'First Class' THEN 2
           ELSE 1 END
    INTO seatclass_multiplier
    FROM seat s
    JOIN seatclass sc ON s.seatclass_id = sc.id   
    WHERE s.id = NEW.seat_id
    LIMIT 1;
    
    -- Рассчитываем новую стоимость
    SET NEW.price = NEW.price * seatclass_multiplier;
END;
//
DELIMITER ;

-- log_event TRIGGER 

select * from event;
DELIMITER //

CREATE TRIGGER log_user_event
AFTER INSERT ON user
FOR EACH ROW
BEGIN
    INSERT INTO event (name, datetime, description, user_id, event_type_id)
    VALUES ('User Added', NOW(), CONCAT('New user added: [ username: ', NEW.username, ' ]'), NEW.id, 1);
END;
//

DELIMITER ;

-- prevent_flight_registration_on_maintenance TRIGGER

select * from plane;
select * from flight;
select * from ticket;

DELIMITER //

CREATE TRIGGER prevent_flight_registration_on_maintenance
BEFORE INSERT ON flight
FOR EACH ROW
BEGIN
    DECLARE plane_is_under_maintenance INT;
    SELECT is_under_maintenance INTO plane_is_under_maintenance
    FROM plane
    WHERE id = NEW.plane_id;

    IF plane_is_under_maintenance = 1 
    THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'CUSTOMER_ERROR --> Cannot register flight for a plane under maintenance.';
    END IF;
END;

//

DELIMITER ;


