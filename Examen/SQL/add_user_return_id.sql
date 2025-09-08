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
END
GO

DECLARE @new_id INT;

EXEC [dbo].[add_user_return_id]
	@login = 'newuser',
    @password = '12345678',
    @name = N'Федор',
    @surname = N'Федоров',
    @patronymic = N'Федорович',
    @mail = 'newuser@mail.ru',
    @phone_number = '+79428328282',
    @UserID = @new_id OUTPUT;

-- Для отображения, если нужно
SELECT * FROM usersSet;

-- Для очистки, если нужно
DELETE FROM usersSet WHERE id >= 7;