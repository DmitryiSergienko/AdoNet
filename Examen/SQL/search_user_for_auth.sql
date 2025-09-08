CREATE OR ALTER PROCEDURE [dbo].[stp_search_user_for_auth]
	@login					NVARCHAR(50),
	@password				NVARCHAR(100)
AS
BEGIN
SELECT login, password FROM usersSet
WHERE login = @login AND password = @password;
END

EXEC [dbo].[stp_search_user_for_auth]
    @login = N'Baboy1@Baboy.Baboy',
    @password = N'Baboy1231';