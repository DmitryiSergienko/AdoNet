CREATE OR ALTER PROCEDURE [dbo].[stp_search_user_for_info]
	@login					NVARCHAR(50)
AS
BEGIN
SELECT * FROM usersSet
WHERE login = @login;
END

EXEC [dbo].[stp_search_user_for_info] N'Логин2';