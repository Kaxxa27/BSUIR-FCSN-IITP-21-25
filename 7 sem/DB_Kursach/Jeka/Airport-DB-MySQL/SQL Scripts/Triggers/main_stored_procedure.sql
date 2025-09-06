USE aerobooking;


CALL CreateUser('New', '123', 'New@mail.ru', 1, 0, 0);
SELECT * FROM user;
-- CreateUser PROCEDURE
DELIMITER //
CREATE PROCEDURE CreateUser(
    IN p_username VARCHAR(20),
    IN p_password VARCHAR(100),
    IN p_email VARCHAR(254),
    IN p_is_active TINYINT(1),
    IN p_is_admin TINYINT(0),
    IN p_is_staff TINYINT(0)
)
BEGIN
    DECLARE existing_user INT;

    SELECT COUNT(*) INTO existing_user
    FROM user
    WHERE username = p_username OR email = p_email;
    
    IF existing_user = 0 THEN
        INSERT INTO user(username, password, email, is_active,is_admin, is_staff)
        VALUES (p_username, SHA2(p_password, 256), p_email, p_is_active, p_is_admin, p_is_staff);
        
        SELECT 'User created successfully' AS result;
    ELSE
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Username or email already exists';
    END IF;
END; 
//
DELIMITER ;



-- UpdateUserInfo PROCEDURE

-- DROP PROCEDURE UpdateUserInfo;
DELIMITER //
CREATE PROCEDURE UpdateUserInfo
(IN userID INT, IN newUserName VARCHAR(45), 
 IN newPassword CHAR(64), IN newEmail VARCHAR(45),
 IN newActiveStatus TINYINT(1), IN newAdminStatus TINYINT(1), IN newStaffStatus TINYINT(1))
BEGIN
    UPDATE user
    SET
        username = IFNULL(newUserName, username),
        password = IFNULL(newPassword, password),
        email = IFNULL(newEmail, email),
        is_active = IFNULL(newActiveStatus, is_active),
        is_admin = IFNULL(newAdminStatus, is_admin),
        is_staff = IFNULL(newStaffStatus, is_staff)
    WHERE id = userID;
END;
//
DELIMITER ;


select * from user;
-- Используя оператор CALL
CALL UpdateUserInfo(5, NULL, 'НОВЫЙ', 'opp@mmm.rom', 0, NULL, NULL);


-- CancelTicketReservation PROCEDURE

DELIMITER //
CREATE PROCEDURE CancelTicketReservation(IN ticketID INT)
BEGIN
    DELETE FROM ticket
    WHERE id = ticketID;
END;
//
DELIMITER ;

select * from ticket;

-- CALL CancelTicketReservation(1);


-- GetAverageTicketPrice PROCEDURE

DELIMITER //
CREATE PROCEDURE GetAverageTicketPrice(IN flightNumber VARCHAR(10), OUT averagePrice DECIMAL(10,3))
BEGIN
    SELECT AVG(t.price) INTO averagePrice
    FROM ticket t
    JOIN flight f ON t.flight_id = f.id
    WHERE f.flight_number = flightNumber;
END;
//
DELIMITER ;

select * from ticket;
select * from flight;
CALL GetAverageTicketPrice('QF 101', @averagePrice);
select @averagePrice;
-- Теперь переменная @averagePrice содержит результат