-- Индексы для таблицы "user"
CREATE INDEX IF NOT EXISTS idx_user_username ON "user" USING hash (username); -- Для точного поиска по имени пользователя
CREATE INDEX IF NOT EXISTS idx_user_registration_date ON "user" USING btree (registration_date); -- Для сортировки/фильтрации по дате регистрации

-- Индексы для таблицы "user_profile"
CREATE INDEX IF NOT EXISTS idx_user_profile_email ON "user_profile" USING hash (email); -- Для точного поиска по email
CREATE INDEX IF NOT EXISTS idx_user_profile_phone_number ON "user_profile" USING hash (phone_number); -- Для точного поиска по номеру телефона
CREATE INDEX IF NOT EXISTS idx_user_profile_last_name ON "user_profile" USING btree (last_name); -- Для сортировки по фамилии

-- Индексы для таблицы "loyalty_points"
CREATE INDEX IF NOT EXISTS idx_loyalty_points_user_id ON "loyalty_points" USING btree (user_id); -- Для фильтрации по пользователю
CREATE INDEX IF NOT EXISTS idx_loyalty_points_last_updated ON "loyalty_points" USING btree (last_updated); -- Для сортировки по дате последнего обновления

-- Индексы для таблицы "route"
CREATE INDEX IF NOT EXISTS idx_route_departure_time ON "route" USING btree (departure_time); -- Для сортировки/фильтрации по времени отправления
CREATE INDEX IF NOT EXISTS idx_route_arrival_time ON "route" USING btree (arrival_time); -- Для сортировки/фильтрации по времени прибытия
CREATE INDEX IF NOT EXISTS idx_route_departure_location_id ON "route" USING btree (departure_location_id); -- Для поиска по месту отправления
CREATE INDEX IF NOT EXISTS idx_route_arrival_location_id ON "route" USING btree (arrival_location_id); -- Для поиска по месту прибытия
CREATE INDEX IF NOT EXISTS idx_route_price ON "route" USING btree (price); -- Для сортировки по цене
CREATE INDEX IF NOT EXISTS idx_route_transport_id ON "route" USING btree (transport_id); -- Для фильтрации по транспорту
CREATE INDEX IF NOT EXISTS idx_route_driver_id ON "route" USING btree (driver_id); -- Для фильтрации по водителю

-- Индексы для таблицы "ticket_booking"
CREATE INDEX IF NOT EXISTS idx_ticket_booking_user_id ON "ticket_booking" USING btree (user_id); -- Для фильтрации по пользователю
CREATE INDEX IF NOT EXISTS idx_ticket_booking_route_id ON "ticket_booking" USING btree (route_id); -- Для фильтрации по маршруту
CREATE INDEX IF NOT EXISTS idx_ticket_booking_status_id ON "ticket_booking" USING btree (status_id); -- Для фильтрации по статусу бронирования

-- Индексы для таблицы "support_request"
CREATE INDEX IF NOT EXISTS idx_support_request_user_id ON "support_request" USING btree (user_id); -- Для фильтрации по пользователю
CREATE INDEX IF NOT EXISTS idx_support_request_created_at ON "support_request" USING btree (created_at); -- Для сортировки по дате создания
CREATE INDEX IF NOT EXISTS idx_support_request_status_id ON "support_request" USING btree (status_id); -- Для фильтрации по статусу запроса
CREATE INDEX IF NOT EXISTS idx_support_request_ticket_booking_id ON "support_request" USING btree (ticket_booking_id); -- Для соединения с бронированием

-- Индексы для таблицы "support_message"
CREATE INDEX IF NOT EXISTS idx_support_message_support_request_id ON "support_message" USING btree (support_request_id); -- Для соединения с запросом
CREATE INDEX IF NOT EXISTS idx_support_message_sender_id ON "support_message" USING btree (sender_id); -- Для фильтрации по отправителю сообщения

-- Индексы для таблицы "transport"
CREATE INDEX IF NOT EXISTS idx_transport_transport_type_id ON "transport" USING btree (transport_type_id); -- Для фильтрации по типу транспорта
CREATE INDEX IF NOT EXISTS idx_transport_carrier_id ON "transport" USING btree (carrier_id); -- Для фильтрации по перевозчику
CREATE INDEX IF NOT EXISTS idx_transport_capacity ON "transport" USING btree (capacity); -- Для сортировки по вместимости

-- Индексы для таблицы "transport_feature_link"
CREATE INDEX IF NOT EXISTS idx_transport_feature_link_transport_id ON "transport_feature_link" USING btree (transport_id); -- Для соединения с транспортом
CREATE INDEX IF NOT EXISTS idx_transport_feature_link_feature_id ON "transport_feature_link" USING btree (transport_feature_id); -- Для соединения с функцией транспорта

-- Индексы для таблицы "notifications"
CREATE INDEX IF NOT EXISTS idx_notifications_sent_at ON "notifications" USING btree (sent_at); -- Для сортировки по дате отправки
CREATE INDEX IF NOT EXISTS idx_notifications_status_id ON "notifications" USING btree (status_id); -- Для фильтрации по статусу уведомления

-- Индексы для таблицы "sensitive_log"
CREATE INDEX IF NOT EXISTS idx_sensitive_log_created_at ON "sensitive_log" USING btree (created_at); -- Для сортировки по дате создания
CREATE INDEX IF NOT EXISTS idx_sensitive_log_log_level_id ON "sensitive_log" USING btree (log_level_id); -- Для фильтрации по уровню логирования

-- Индексы для поиска по тексту
CREATE EXTENSION IF NOT EXISTS pg_trgm;
CREATE INDEX IF NOT EXISTS idx_support_request_description_gin ON "support_request" USING gin (description gin_trgm_ops);
