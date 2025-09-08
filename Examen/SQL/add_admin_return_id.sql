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
END
GO

DECLARE @new_id INT;

EXEC [dbo].[add_admin_return_id]
	@login = 'newuser',
    @password = '12345678',
    @name = N'Федор',
    @surname = N'Федоров',
    @patronymic = N'Федорович',
    @mail = 'newuser@mail.ru',
    @phone_number = '+79428328282',
    @AdminID = @new_id OUTPUT;

-- Для отображения, если нужно
SELECT * FROM adminsSet;

-- Для очистки, если нужно
DELETE FROM adminsSet WHERE id >= 7;