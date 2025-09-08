
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/08/2025 08:54:19
-- Generated from EDMX file: C:\Users\Sergienko\source\repos\AdoNet\Examen\ModelFirst\Examen_Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Examen_ModelFirst];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_usersorders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ordersSet] DROP CONSTRAINT [FK_usersorders];
GO
IF OBJECT_ID(N'[dbo].[FK_ordersorder_history]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[order_historySet] DROP CONSTRAINT [FK_ordersorder_history];
GO
IF OBJECT_ID(N'[dbo].[FK_status_ordersorders]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ordersSet] DROP CONSTRAINT [FK_status_ordersorders];
GO
IF OBJECT_ID(N'[dbo].[FK_ordersorder_products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[order_productsSet] DROP CONSTRAINT [FK_ordersorder_products];
GO
IF OBJECT_ID(N'[dbo].[FK_productsorder_products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[order_productsSet] DROP CONSTRAINT [FK_productsorder_products];
GO
IF OBJECT_ID(N'[dbo].[FK_warehouseswarehouse_products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[warehouse_productsSet] DROP CONSTRAINT [FK_warehouseswarehouse_products];
GO
IF OBJECT_ID(N'[dbo].[FK_productswarehouse_products]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[warehouse_productsSet] DROP CONSTRAINT [FK_productswarehouse_products];
GO
IF OBJECT_ID(N'[dbo].[FK_new_status]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[order_historySet] DROP CONSTRAINT [FK_new_status];
GO
IF OBJECT_ID(N'[dbo].[FK_old_status]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[order_historySet] DROP CONSTRAINT [FK_old_status];
GO
IF OBJECT_ID(N'[dbo].[FK_productsproduct_categories]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[product_categoriesSet] DROP CONSTRAINT [FK_productsproduct_categories];
GO
IF OBJECT_ID(N'[dbo].[FK_categoriesproduct_categories]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[product_categoriesSet] DROP CONSTRAINT [FK_categoriesproduct_categories];
GO
IF OBJECT_ID(N'[dbo].[FK_productsimages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[imagesSet] DROP CONSTRAINT [FK_productsimages];
GO
IF OBJECT_ID(N'[dbo].[FK_usersimages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[imagesSet] DROP CONSTRAINT [FK_usersimages];
GO
IF OBJECT_ID(N'[dbo].[FK_adminsimages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[imagesSet] DROP CONSTRAINT [FK_adminsimages];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[adminsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[adminsSet];
GO
IF OBJECT_ID(N'[dbo].[usersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[usersSet];
GO
IF OBJECT_ID(N'[dbo].[ordersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ordersSet];
GO
IF OBJECT_ID(N'[dbo].[order_historySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[order_historySet];
GO
IF OBJECT_ID(N'[dbo].[status_ordersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[status_ordersSet];
GO
IF OBJECT_ID(N'[dbo].[order_productsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[order_productsSet];
GO
IF OBJECT_ID(N'[dbo].[productsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[productsSet];
GO
IF OBJECT_ID(N'[dbo].[warehouse_productsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[warehouse_productsSet];
GO
IF OBJECT_ID(N'[dbo].[warehousesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[warehousesSet];
GO
IF OBJECT_ID(N'[dbo].[categoriesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[categoriesSet];
GO
IF OBJECT_ID(N'[dbo].[product_categoriesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[product_categoriesSet];
GO
IF OBJECT_ID(N'[dbo].[imagesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[imagesSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'adminsSet'
CREATE TABLE [dbo].[adminsSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [login] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NULL,
    [name] nvarchar(max)  NOT NULL,
    [surname] nvarchar(max)  NOT NULL,
    [patronymic] nvarchar(max)  NULL,
    [mail] nvarchar(max)  NOT NULL,
    [phone_number] nvarchar(max)  NULL,
    [registration_date] datetime  NULL,
    [images_id] int  NOT NULL
);
GO

-- Creating table 'usersSet'
CREATE TABLE [dbo].[usersSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [login] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NULL,
    [name] nvarchar(max)  NOT NULL,
    [surname] nvarchar(max)  NOT NULL,
    [patronymic] nvarchar(max)  NULL,
    [mail] nvarchar(max)  NOT NULL,
    [phone_number] nvarchar(max)  NULL,
    [registration_date] datetime  NULL,
    [images_id] int  NOT NULL
);
GO

-- Creating table 'ordersSet'
CREATE TABLE [dbo].[ordersSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [order_placement_date] datetime  NULL,
    [total_amount] decimal(18,2)  NOT NULL,
    [address] nvarchar(max)  NULL,
    [user_id] int  NOT NULL,
    [status_id] int  NOT NULL
);
GO

-- Creating table 'order_historySet'
CREATE TABLE [dbo].[order_historySet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [date_of_change] datetime  NULL,
    [comment] nvarchar(max)  NULL,
    [order_id] int  NOT NULL,
    [new_status] int  NOT NULL,
    [old_status] int  NULL
);
GO

-- Creating table 'status_ordersSet'
CREATE TABLE [dbo].[status_ordersSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'order_productsSet'
CREATE TABLE [dbo].[order_productsSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [quantity] int  NOT NULL,
    [price] decimal(18,2)  NOT NULL,
    [order_id] int  NOT NULL,
    [product_id] int  NOT NULL
);
GO

-- Creating table 'productsSet'
CREATE TABLE [dbo].[productsSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [price] decimal(18,2)  NOT NULL,
    [date_added] datetime  NULL,
    [images_id] int  NOT NULL
);
GO

-- Creating table 'warehouse_productsSet'
CREATE TABLE [dbo].[warehouse_productsSet] (
    [quantity] int  NOT NULL,
    [warehouse_id] int  NOT NULL,
    [product_id] int  NOT NULL
);
GO

-- Creating table 'warehousesSet'
CREATE TABLE [dbo].[warehousesSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [address] nvarchar(max)  NULL
);
GO

-- Creating table 'categoriesSet'
CREATE TABLE [dbo].[categoriesSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'product_categoriesSet'
CREATE TABLE [dbo].[product_categoriesSet] (
    [product_id] int  NOT NULL,
    [category_id] int  NOT NULL
);
GO

-- Creating table 'imagesSet'
CREATE TABLE [dbo].[imagesSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [image] varbinary(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'adminsSet'
ALTER TABLE [dbo].[adminsSet]
ADD CONSTRAINT [PK_adminsSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'usersSet'
ALTER TABLE [dbo].[usersSet]
ADD CONSTRAINT [PK_usersSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'ordersSet'
ALTER TABLE [dbo].[ordersSet]
ADD CONSTRAINT [PK_ordersSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'order_historySet'
ALTER TABLE [dbo].[order_historySet]
ADD CONSTRAINT [PK_order_historySet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'status_ordersSet'
ALTER TABLE [dbo].[status_ordersSet]
ADD CONSTRAINT [PK_status_ordersSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'order_productsSet'
ALTER TABLE [dbo].[order_productsSet]
ADD CONSTRAINT [PK_order_productsSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'productsSet'
ALTER TABLE [dbo].[productsSet]
ADD CONSTRAINT [PK_productsSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [warehouse_id], [product_id] in table 'warehouse_productsSet'
ALTER TABLE [dbo].[warehouse_productsSet]
ADD CONSTRAINT [PK_warehouse_productsSet]
    PRIMARY KEY CLUSTERED ([warehouse_id], [product_id] ASC);
GO

-- Creating primary key on [id] in table 'warehousesSet'
ALTER TABLE [dbo].[warehousesSet]
ADD CONSTRAINT [PK_warehousesSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'categoriesSet'
ALTER TABLE [dbo].[categoriesSet]
ADD CONSTRAINT [PK_categoriesSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [product_id], [category_id] in table 'product_categoriesSet'
ALTER TABLE [dbo].[product_categoriesSet]
ADD CONSTRAINT [PK_product_categoriesSet]
    PRIMARY KEY CLUSTERED ([product_id], [category_id] ASC);
GO

-- Creating primary key on [id] in table 'imagesSet'
ALTER TABLE [dbo].[imagesSet]
ADD CONSTRAINT [PK_imagesSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [user_id] in table 'ordersSet'
ALTER TABLE [dbo].[ordersSet]
ADD CONSTRAINT [FK_usersorders]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[usersSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_usersorders'
CREATE INDEX [IX_FK_usersorders]
ON [dbo].[ordersSet]
    ([user_id]);
GO

-- Creating foreign key on [order_id] in table 'order_historySet'
ALTER TABLE [dbo].[order_historySet]
ADD CONSTRAINT [FK_ordersorder_history]
    FOREIGN KEY ([order_id])
    REFERENCES [dbo].[ordersSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ordersorder_history'
CREATE INDEX [IX_FK_ordersorder_history]
ON [dbo].[order_historySet]
    ([order_id]);
GO

-- Creating foreign key on [status_id] in table 'ordersSet'
ALTER TABLE [dbo].[ordersSet]
ADD CONSTRAINT [FK_status_ordersorders]
    FOREIGN KEY ([status_id])
    REFERENCES [dbo].[status_ordersSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_status_ordersorders'
CREATE INDEX [IX_FK_status_ordersorders]
ON [dbo].[ordersSet]
    ([status_id]);
GO

-- Creating foreign key on [order_id] in table 'order_productsSet'
ALTER TABLE [dbo].[order_productsSet]
ADD CONSTRAINT [FK_ordersorder_products]
    FOREIGN KEY ([order_id])
    REFERENCES [dbo].[ordersSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ordersorder_products'
CREATE INDEX [IX_FK_ordersorder_products]
ON [dbo].[order_productsSet]
    ([order_id]);
GO

-- Creating foreign key on [product_id] in table 'order_productsSet'
ALTER TABLE [dbo].[order_productsSet]
ADD CONSTRAINT [FK_productsorder_products]
    FOREIGN KEY ([product_id])
    REFERENCES [dbo].[productsSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_productsorder_products'
CREATE INDEX [IX_FK_productsorder_products]
ON [dbo].[order_productsSet]
    ([product_id]);
GO

-- Creating foreign key on [warehouse_id] in table 'warehouse_productsSet'
ALTER TABLE [dbo].[warehouse_productsSet]
ADD CONSTRAINT [FK_warehouseswarehouse_products]
    FOREIGN KEY ([warehouse_id])
    REFERENCES [dbo].[warehousesSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [product_id] in table 'warehouse_productsSet'
ALTER TABLE [dbo].[warehouse_productsSet]
ADD CONSTRAINT [FK_productswarehouse_products]
    FOREIGN KEY ([product_id])
    REFERENCES [dbo].[productsSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_productswarehouse_products'
CREATE INDEX [IX_FK_productswarehouse_products]
ON [dbo].[warehouse_productsSet]
    ([product_id]);
GO

-- Creating foreign key on [new_status] in table 'order_historySet'
ALTER TABLE [dbo].[order_historySet]
ADD CONSTRAINT [FK_new_status]
    FOREIGN KEY ([new_status])
    REFERENCES [dbo].[status_ordersSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_new_status'
CREATE INDEX [IX_FK_new_status]
ON [dbo].[order_historySet]
    ([new_status]);
GO

-- Creating foreign key on [old_status] in table 'order_historySet'
ALTER TABLE [dbo].[order_historySet]
ADD CONSTRAINT [FK_old_status]
    FOREIGN KEY ([old_status])
    REFERENCES [dbo].[status_ordersSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_old_status'
CREATE INDEX [IX_FK_old_status]
ON [dbo].[order_historySet]
    ([old_status]);
GO

-- Creating foreign key on [product_id] in table 'product_categoriesSet'
ALTER TABLE [dbo].[product_categoriesSet]
ADD CONSTRAINT [FK_productsproduct_categories]
    FOREIGN KEY ([product_id])
    REFERENCES [dbo].[productsSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [category_id] in table 'product_categoriesSet'
ALTER TABLE [dbo].[product_categoriesSet]
ADD CONSTRAINT [FK_categoriesproduct_categories]
    FOREIGN KEY ([category_id])
    REFERENCES [dbo].[categoriesSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_categoriesproduct_categories'
CREATE INDEX [IX_FK_categoriesproduct_categories]
ON [dbo].[product_categoriesSet]
    ([category_id]);
GO

-- Creating foreign key on [images_id] in table 'productsSet'
ALTER TABLE [dbo].[productsSet]
ADD CONSTRAINT [FK_productsimages]
    FOREIGN KEY ([images_id])
    REFERENCES [dbo].[imagesSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_productsimages'
CREATE INDEX [IX_FK_productsimages]
ON [dbo].[productsSet]
    ([images_id]);
GO

-- Creating foreign key on [images_id] in table 'usersSet'
ALTER TABLE [dbo].[usersSet]
ADD CONSTRAINT [FK_usersimages]
    FOREIGN KEY ([images_id])
    REFERENCES [dbo].[imagesSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_usersimages'
CREATE INDEX [IX_FK_usersimages]
ON [dbo].[usersSet]
    ([images_id]);
GO

-- Creating foreign key on [images_id] in table 'adminsSet'
ALTER TABLE [dbo].[adminsSet]
ADD CONSTRAINT [FK_adminsimages]
    FOREIGN KEY ([images_id])
    REFERENCES [dbo].[imagesSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_adminsimages'
CREATE INDEX [IX_FK_adminsimages]
ON [dbo].[adminsSet]
    ([images_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------

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