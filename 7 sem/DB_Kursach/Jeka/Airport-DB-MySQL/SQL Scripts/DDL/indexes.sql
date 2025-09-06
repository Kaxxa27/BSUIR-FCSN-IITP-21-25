USE aerobooking;

-- Индексы для таблицы user
CREATE INDEX idx_user_username ON user(username);
CREATE INDEX idx_user_email ON user(email);

-- Индексы для таблицы passport
CREATE INDEX idx_passport_user_id ON passport(user_id);
CREATE INDEX idx_passport_nationality_id ON passport(nationality_id);
CREATE INDEX idx_passport_country_id ON passport(country_id);
CREATE INDEX idx_passport_gender_id ON passport(gender_id);

-- Индексы для таблицы country
CREATE INDEX idx_country_code ON country(code);

-- Индексы для таблицы city
CREATE INDEX idx_city_country_id ON city(country_id);

-- Индексы для таблицы airport
CREATE INDEX idx_airport_city_id ON airport(city_id);
CREATE INDEX idx_airport_code_IATA ON airport(code_IATA);

-- Индексы для таблицы plane
CREATE INDEX idx_plane_registration_number ON plane(registration_number);

-- Индексы для таблицы airline
CREATE INDEX idx_airline_country_id ON airline(country_id);

-- Индексы для таблицы flight
CREATE INDEX idx_flight_airport_id ON flight(airport_id);
CREATE INDEX idx_flight_plane_id ON flight(plane_id);
CREATE INDEX idx_flight_airline_id ON flight(airline_id);
CREATE INDEX idx_flight_flight_number ON flight(flight_number);

-- Индексы для таблицы seat
CREATE INDEX idx_seat_plane_id ON seat(plane_id);
CREATE INDEX idx_seat_seatclass_id ON seat(seatclass_id);

-- Индексы для таблицы ticket
CREATE INDEX idx_ticket_flight_id ON ticket(flight_id);
CREATE INDEX idx_ticket_seat_id ON ticket(seat_id);
CREATE INDEX idx_ticket_passport_id ON ticket(passport_id);

-- Индексы для таблицы schedule
CREATE INDEX idx_schedule_flight_id ON schedule(flight_id);

-- Индексы для таблицы baggage
CREATE INDEX idx_baggage_ticket_id ON baggage(ticket_id);

-- Индексы для таблицы event
CREATE INDEX idx_event_user_id ON event(user_id);
CREATE INDEX idx_event_event_type_id ON event(event_type_id);

-- Индексы для таблицы plane_maintenance
CREATE INDEX idx_plane_maintenance_plane_id ON plane_maintenance(plane_id);

-- Индексы для таблицы crew
CREATE INDEX idx_crew_nationality_id ON crew(nationality_id);

-- Индексы для таблицы crew_assignment
CREATE INDEX idx_crew_assignment_crew_id ON crew_assignment(crew_id);
CREATE INDEX idx_crew_assignment_flight_id ON crew_assignment(flight_id);

-- Индексы для таблицы route
CREATE INDEX idx_route_source_airport_id ON route(source_airport_id);
CREATE INDEX idx_route_destination_airport_id ON route(destination_airport_id);

-- Индексы для таблицы ticket_meal
CREATE INDEX idx_ticket_meal_ticket_id ON ticket_meal(ticket_id);
CREATE INDEX idx_ticket_meal_meal_id ON ticket_meal(meal_id);

-- Индексы для таблицы payment
CREATE INDEX idx_payment_ticket_id ON payment(ticket_id);

-- Индексы для таблицы review
CREATE INDEX idx_review_user_id ON review(user_id);
CREATE INDEX idx_review_flight_id ON review(flight_id);

-- Индексы для таблицы notification
CREATE INDEX idx_notification_user_id ON notification(user_id);
CREATE INDEX idx_notification_is_read ON notification(is_read);

-- Индексы для таблицы service
CREATE INDEX idx_service_name ON service(name);
