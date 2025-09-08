USE Examen_ModelFirst;
GO

ALTER TABLE [dbo].[adminsSet] ALTER COLUMN [images_id] INT NULL;
ALTER TABLE [dbo].[usersSet] ALTER COLUMN [images_id] INT NULL;
ALTER TABLE [dbo].[productsSet] ALTER COLUMN [images_id] INT NULL;
GO

CREATE OR ALTER PROCEDURE [dbo].[stp_user_update]
	@id						INT,
	@login					NVARCHAR(50),
	@password				NVARCHAR(100),
	@name					NVARCHAR(100),
	@surname				NVARCHAR(100),
	@patronymic				NVARCHAR(100),
	@mail					NVARCHAR(255),
	@phone_number			NVARCHAR(20)
AS
BEGIN
	UPDATE usersSet
	SET 
        login = @login,
        password = @password,
        name = @name,
        surname = @surname,
        patronymic = @patronymic,
        mail = @mail,
        phone_number = @phone_number
	WHERE id = @id;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[stp_search_user_for_info]
	@login					NVARCHAR(50)
AS
BEGIN
SELECT * FROM usersSet
WHERE login = @login;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[stp_search_user_by_id]
	@id					INT
AS
BEGIN
SELECT * FROM usersSet
WHERE id = @id;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[stp_search_admin_for_info]
	@login					NVARCHAR(50)
AS
BEGIN
SELECT * FROM adminsSet
WHERE login = @login;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[stp_search_admin_by_id]
	@id					INT
AS
BEGIN
SELECT * FROM adminsSet
WHERE id = @id;
END;
GO

CREATE OR ALTER VIEW show_users_without_password AS
SELECT * FROM usersSet
WHERE password IS NULL;
GO

CREATE OR ALTER VIEW show_top_3_users_by_number_of_orders AS
SELECT TOP(3) u.login, COUNT(o.id) as count_orders FROM usersSet AS u
JOIN ordersSet AS o ON o.user_id = u.id
GROUP BY u.login
ORDER BY count_orders DESC;
GO

CREATE OR ALTER VIEW show_top_3_products AS
SELECT TOP(3) p.name, COUNT(op.product_id) AS count FROM productsSet AS p
JOIN order_productsSet AS op ON op.product_id = p.id
GROUP BY p.name
ORDER BY count DESC;
GO

CREATE OR ALTER PROCEDURE show_products_in_portions 
	@skip_rows			INT,
	@show_rows			INT
AS
BEGIN
	SELECT * FROM productsSet
	ORDER BY id
	OFFSET @skip_rows ROWS
	FETCH NEXT @show_rows ROWS ONLY;
END;
GO

CREATE OR ALTER PROCEDURE show_products_in_category
	@category_id	INT
AS
BEGIN
	SELECT p.* FROM productsSet AS p
	JOIN product_categoriesSet AS pc ON pc.product_id = p.id
	JOIN categoriesSet AS c ON pc.category_id = c.id
	WHERE c.id = @category_id;
END;
GO

CREATE VIEW show_number_of_users_orders AS
SELECT u.login, COUNT(o.id) as count_orders FROM usersSet AS u
JOIN ordersSet AS o ON o.user_id = u.id
GROUP BY u.login;
GO

CREATE OR ALTER VIEW show_count_products_in_category AS
SELECT c.name, SUM(wp.quantity) AS count_products FROM categoriesSet AS c
JOIN product_categoriesSet AS pc ON pc.category_id = c.id
JOIN productsSet AS p ON pc.product_id = p.id
JOIN warehouse_productsSet AS wp ON wp.product_id = p.id
GROUP BY c.name;
GO

CREATE OR ALTER VIEW show_count_products_in_books_and_tools AS
SELECT c.name, SUM(wp.quantity) AS count_products FROM categoriesSet AS c
JOIN product_categoriesSet AS pc ON pc.category_id = c.id
JOIN productsSet AS p ON pc.product_id = p.id
JOIN warehouse_productsSet AS wp ON wp.product_id = p.id
WHERE c.name IN (N'Книги', N'Электроника')
GROUP BY c.name;
GO

CREATE OR ALTER PROCEDURE [dbo].[stp_users_all]
AS
BEGIN
SELECT * FROM usersSet;
END;
GO

CREATE OR ALTER PROCEDURE show_all_products
AS
BEGIN
	SELECT * FROM productsSet;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[stp_search_user_for_auth]
	@login					NVARCHAR(50),
	@password				NVARCHAR(100)
AS
BEGIN
SELECT login, password FROM usersSet
WHERE login = @login AND password = @password;
END;
GO

CREATE OR ALTER PROCEDURE search_products_by_price
	@start_price	INT,
	@end_price		INT
AS
BEGIN
	SELECT * FROM productsSet
	WHERE price >= @start_price AND price <= @end_price
	ORDER BY price DESC;
END;
GO

CREATE OR ALTER PROCEDURE search_products_by_name
	@name			NVARCHAR(200)
AS
BEGIN
	SELECT * FROM productsSet
	WHERE name like '%' + @name + '%';
END;
GO

CREATE OR ALTER PROCEDURE search_orders_by_date
	@start_date			DATETIME,
	@end_date			DATETIME
AS
BEGIN
	SELECT * FROM ordersSet
	WHERE order_placement_date BETWEEN @start_date AND @end_date;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[stp_search_admin_for_auth]
	@login					NVARCHAR(50),
	@password				NVARCHAR(100)
AS
BEGIN
SELECT login, password FROM adminsSet
WHERE login = @login AND password = @password;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[stp_user_delete] 
    @id INT
AS
BEGIN
    DELETE FROM usersSet 
    WHERE id = @id;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[add_user_return_id]
    @login          NVARCHAR(50),
    @password       NVARCHAR(100),
    @name           NVARCHAR(100),
    @surname        NVARCHAR(100),
    @patronymic     NVARCHAR(100) = NULL,
    @mail           NVARCHAR(255),
    @phone_number   NVARCHAR(20),
    @UserID         INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @UserID = 0

    BEGIN TRANSACTION

        INSERT INTO usersSet (login, password, name, surname, patronymic, mail, phone_number)
        VALUES (@login, @password, @name, @surname, @patronymic, @mail, @phone_number)

        SET @UserID = SCOPE_IDENTITY();

    COMMIT TRANSACTION
END;
GO

CREATE OR ALTER PROCEDURE add_user
	@login					NVARCHAR(50),
	@password				NVARCHAR(100),
	@name					NVARCHAR(100),
	@surname				NVARCHAR(100),
	@patronymic				NVARCHAR(100),
	@mail					NVARCHAR(255),
	@phone_number			NVARCHAR(20)
AS
BEGIN
	INSERT INTO usersSet (login, password, name, surname, patronymic, mail, phone_number) VALUES
	(@login, @password, @name, @surname, @patronymic, @mail, @phone_number);
END;
GO

CREATE OR ALTER PROCEDURE add_quantity_product
	@product_id				INT,
	@warehouse_id			INT,
	@quantity				INT
AS
BEGIN
	INSERT INTO warehouse_productsSet (product_id, warehouse_id, quantity) VALUES
	(@product_id, @warehouse_id, @quantity);
END;
GO

CREATE OR ALTER PROCEDURE add_product_categories
	@product_id				INT,
	@category_id			INT
AS
BEGIN
	INSERT INTO product_categoriesSet (product_id, category_id) VALUES
	(@product_id, @category_id);
END;
GO

CREATE OR ALTER PROCEDURE add_product
	@name			NVARCHAR(200),
	@description	NVARCHAR(1000),
	@price			DECIMAL(18,2)
AS
BEGIN
	INSERT INTO productsSet (name, description, price) VALUES
	(@name, @description, @price);
END;
GO

CREATE OR ALTER PROCEDURE add_category
	@name			NVARCHAR(200),
	@description	NVARCHAR(1000)
AS
BEGIN
	INSERT INTO categoriesSet (name, description) VALUES
	(@name, @description);
	SELECT * FROM categoriesSet
	WHERE id = @@IDENTITY;
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[add_admin_return_id]
    @login          NVARCHAR(50),
    @password       NVARCHAR(100),
    @name           NVARCHAR(100),
    @surname        NVARCHAR(100),
    @patronymic     NVARCHAR(100) = NULL,
    @mail           NVARCHAR(255),
    @phone_number   NVARCHAR(20),
    @AdminID        INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @AdminID = 0

    BEGIN TRANSACTION

        INSERT INTO adminsSet (login, password, name, surname, patronymic, mail, phone_number)
        VALUES (@login, @password, @name, @surname, @patronymic, @mail, @phone_number)

        SET @AdminID = SCOPE_IDENTITY();

    COMMIT TRANSACTION
END;
GO

INSERT INTO status_ordersSet (name, description) VALUES 
    (N'Ожидает оплаты',		N'Заказ создан, ожидается оплата от покупателя.'),
    (N'В обработке',		N'Оплата получена, заказ собирается и подготавливается к отправке.'),
    (N'Доставлен',			N'Заказ успешно доставлен покупателю.');

-- === ADMINS ===
INSERT INTO adminsSet (login, password, name, surname, patronymic, mail, phone_number, registration_date) VALUES
('ekaterina_m',     'kitty2025',     N'Екатерина',     N'Михайлова',     N'Дмитриевна',     'mikhaylova.e@inbox.ru',        '+79112223344', '2025-02-01 10:00:00'),
('dmitry_s',        'securePass1!',  N'Дмитрий',       N'Соколов',       N'Андреевич',      'sokolov.d@workmail.com',       '+79223334455', '2025-02-03 14:20:00'),
('olga_ivanova',    'olga7777',      N'Ольга',         N'Иванова',       N'Павловна',       'ivanova.o@company.org',        '+79334445566', '2025-02-05 09:15:00'),
('aleksey_n',       'qwerty99',      N'Алексей',       N'Никитин',       N'Владимирович',   'nikitin.a@web.ru',             '+79445556677', '2025-02-07 17:40:00'),
('nina_kuznetsova', 'ninaPass2025',  N'Нина',          N'Кузнецова',     N'Сергеевна',      'kuznetsova.n@yandex.com',      '+79556667788', '2025-02-09 12:30:00'),
('user_no_pass',    NULL,            N'Григорий',      N'Лебедев',       N'Романович',      'lebedev.g@tempmail.net',       '+79667778899', '2025-02-10 20:00:00');

-- === USERS ===
INSERT INTO usersSet (login, password, name, surname, patronymic, mail, phone_number, registration_date) VALUES
('ivan_ivanov',     'pass1234',		N'Иван',         N'Иванов',       N'Иванович',     'ivanov@mail.ru',           '+79011234567', '2025-01-10 08:30:00'),
('petr_petrov',     'pass5678',		N'Пётр',         N'Петров',       N'Петрович',     'petrov@mail.ru',           '+79022345678', '2025-01-12 10:15:00'),
('anna_smirnova',   'anna2025',		N'Анна',         N'Смирнова',     N'Олеговна',     'anna.s@domain.com',        '+79033456789', '2025-01-14 13:45:00'),
('sergey_kozlov',   'kozlov777',		N'Сергей',       N'Козлов',       N'Сергеевич',    'kozlov_s@email.ru',        '+79044567890', '2025-01-16 16:20:00'),
('maria_fedorova',  'masha123',		N'Мария',        N'Фёдорова',     N'Алексеевна',   'fedorova.m@site.com',      '+79055678901', '2025-01-18 19:05:00'),
('no_pass',   NULL,		N'Empty123',       N'Empty',       N'Empty',    'empty@email.ru',        '+79044567890', '2025-01-16 16:20:00');

-- === CATEGORIES ===
INSERT INTO categoriesSet (name, description) VALUES
(N'Электроника',         N'Устройства и гаджеты: смартфоны, ноутбуки, планшеты и аксессуары.'),
(N'Одежда',               N'Повседневная и сезонная одежда для мужчин, женщин и детей.'),
(N'Книги',                N'Художественная литература, учебники, научно-популярные книги и детские издания.'),
(N'Бытовая техника',      N'Техника для дома: холодильники, стиральные машины, пылесосы и утюги.'),
(N'Спорт и отдых',        N'Инвентарь и экипировка для спорта, туризма, фитнеса и активного отдыха.');

-- === PRODUCTS ===
INSERT INTO productsSet (name, description, price) VALUES
(N'Смартфон_XYZ',               N'Современный смартфон с 6.5" экраном и 128 ГБ памяти',     29990.00),
(N'Футболка_хлопковая',         N'Чёрная футболка из 100% хлопка, размер M',                990.00),
(N'Книга_SQL_для_начинающих',  N'Обучение SQL с нуля, 320 страниц',                        1450.00),
(N'Микроволновая_печь',         N'MW-2000, 20 л, 800 Вт, белая',                            5990.00),
(N'Теннисная_ракетка',          N'Профессиональная ракетка, вес 280 г',                     4500.00);

-- === WAREHOUSES ===
INSERT INTO warehousesSet (name, description, address) VALUES
(N'Склад Москва Центральный',  N'Основной склад в Москве, приём, хранение и отгрузка', N'г. Москва, ул. Заводская, д. 10'),
(N'Склад СПб Север',           N'Региональный склад на севере страны',               N'г. Санкт-Петербург, пр. Ленина, д. 45'),
(N'Склад Екатеринбург Урал',   N'Центральный склад на Урале',                        N'г. Екатеринбург, ул. Промышленная, д. 7');

-- === WAREHOUSE_PRODUCTS ===
INSERT INTO warehouse_productsSet (warehouse_id, product_id, quantity) VALUES
(1, 1, 10), (1, 2, 50), (1, 3, 20), (1, 4, 5), (1, 5, 8),
(2, 1, 3), (2, 2, 30), (2, 3, 10), (2, 5, 5),
(3, 2, 20), (3, 3, 15), (3, 4, 3), (3, 5, 6);

-- === PRODUCT_CATEGORIES ===
INSERT INTO product_categoriesSet (product_id, category_id) VALUES
(1, 1), (2, 2), (3, 3), (4, 4), (5, 5);

-- === ORDERS ===
INSERT INTO ordersSet (user_id, total_amount, status_id, address, order_placement_date) VALUES
(1, 29990.00, 1, N'г. Москва, ул. Ленина, д. 10, кв. 1', '2025-02-10 08:30:00'),
(2, 990.00,   2, N'г. Санкт-Петербург, Невский пр., д. 25, кв. 5', '2025-01-10 08:30:00'),
(3, 1450.00, 3, N'г. Казань, ул. Баумана, д. 12, кв. 44', '2025-01-11 08:30:00'),
(4, 5990.00, 1, N'г. Новосибирск, ул. Гоголя, д. 33, кв. 12', '2025-01-12 08:30:00'),
(5, 4500.00, 2, N'г. Екатеринбург, ул. Мира, д. 7, кв. 99', '2025-01-13 08:30:00'),
(1, 1450.00, 1, N'г. Москва, ул. Ленина, д. 10, кв. 1', '2025-01-14 08:30:00'),
(3, 4500.00, 2, N'г. Казань, ул. Баумана, д. 12, кв. 44', '2025-01-15 08:30:00'),
(3, 990.00,  3, N'г. Казань, ул. Баумана, д. 12, кв. 44', '2025-01-16 08:30:00'),
(4, 990.00,  2, N'г. Новосибирск, ул. Гоголя, д. 33, кв. 12', '2025-01-17 08:30:00'),
(5, 29990.00, 1, N'г. Екатеринбург, ул. Мира, д. 7, кв. 99', '2025-01-18 08:30:00'),
(5, 5990.00,  2, N'г. Екатеринбург, ул. Мира, д. 7, кв. 99', '2025-01-19 08:30:00'),
(5, 1450.00,  1, N'г. Екатеринбург, ул. Мира, д. 7, кв. 99', '2025-01-20 08:30:00');

-- === ORDER_PRODUCTS ===
INSERT INTO order_productsSet (order_id, product_id, quantity, price) VALUES
(1, 1, 1, 29990.00), (1, 3, 1, 1450.00),
(2, 2, 2, 990.00),
(3, 3, 1, 1450.00), (3, 5, 1, 4500.00),
(4, 4, 1, 5990.00),
(5, 5, 2, 4500.00);

-- === ORDER_HISTORY ===
INSERT INTO order_historySet (order_id, old_status, new_status, comment) VALUES
(1, NULL, 1, N'Создан новый заказ'),
(2, NULL, 1, N'Создан новый заказ'),
(2, 1,    2, N'Покупатель подтвердил оплату'),
(3, NULL, 1, N'Создан новый заказ'),
(3, 1,    2, N'Оплата получена'),
(3, 2,    3, N'Заказ доставлен клиенту'),
(4, NULL, 1, N'Создан новый заказ'),
(5, NULL, 1, N'Создан новый заказ'),
(5, 1,    2, N'Оплата подтверждена'),
(6, NULL, 1, N'Повторный заказ: книга по SQL'),
(7, NULL, 2, N'Срочная обработка — приоритетный заказ'),
(8, NULL, 3, N'Заказ доставлен — подарок другу'),
(9, NULL, 2, N'Добавлен в корзину повторно — акция'),
(10, NULL, 1, N'Новый смартфон — обновление техники'),
(11, NULL, 2, N'Микроволновка — в подарок родителям'),
(12, NULL, 1, N'Книга — для учёбы и сертификации');

-- === SELECTS ===
SELECT * FROM imagesSet;
SELECT * FROM adminsSet;
SELECT * FROM usersSet;
SELECT * FROM categoriesSet;
SELECT * FROM productsSet;
SELECT * FROM warehousesSet;
SELECT * FROM warehouse_productsSet;
SELECT * FROM product_categoriesSet;
SELECT * FROM status_ordersSet;
SELECT * FROM ordersSet;
SELECT * FROM order_productsSet;
SELECT * FROM order_historySet;

-- === ОЧИСТКА ТАБЛИЦ (через DELETE) ===
DELETE FROM order_historySet;
DELETE FROM order_productsSet;
DELETE FROM warehouse_productsSet;
DELETE FROM product_categoriesSet;
DELETE FROM ordersSet;
DELETE FROM productsSet;
DELETE FROM warehousesSet;
DELETE FROM categoriesSet;
DELETE FROM status_ordersSet;
DELETE FROM usersSet;
DELETE FROM adminsSet;
DELETE FROM imagesSet;

-- === СБРОС IDENTITY-СЧЁТЧИКОВ — ТОЛЬКО ДЛЯ ТАБЛИЦ С IDENTITY ===
DBCC CHECKIDENT ('adminsSet', RESEED, 0);
DBCC CHECKIDENT ('usersSet', RESEED, 0);
DBCC CHECKIDENT ('productsSet', RESEED, 0);
DBCC CHECKIDENT ('warehousesSet', RESEED, 0);
DBCC CHECKIDENT ('categoriesSet', RESEED, 0);
DBCC CHECKIDENT ('status_ordersSet', RESEED, 0);
DBCC CHECKIDENT ('ordersSet', RESEED, 0);
DBCC CHECKIDENT ('order_productsSet', RESEED, 0);  -- только если есть IDENTITY!
DBCC CHECKIDENT ('order_historySet', RESEED, 0);