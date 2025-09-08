CREATE OR ALTER PROCEDURE [dbo].[stp_search_admin_for_auth]
	@login					NVARCHAR(50),
	@password				NVARCHAR(100)
AS
BEGIN
SELECT login, password FROM adminsSet
WHERE login = @login AND password = @password;
END

EXEC [dbo].[stp_search_admin_for_auth]
    @login = N'maria_fedorova',
    @password = N'masha123';