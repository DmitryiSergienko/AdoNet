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
END

EXEC stp_user_update
    @id = 6,
    @login = 'new_login',
    @password = 'new_pass',
    @name = N'Иван',
    @surname = N'Иванов',
    @patronymic = N'Иванович',
    @mail = 'ivan@mail.ru',
    @phone_number = '+79001234567';

SELECT * FROM usersSet;