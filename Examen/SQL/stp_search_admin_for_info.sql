CREATE OR ALTER PROCEDURE [dbo].[stp_search_admin_for_info]
	@login					NVARCHAR(50)
AS
BEGIN
SELECT * FROM adminsSet
WHERE login = @login;
END

EXEC [dbo].[stp_search_admin_for_info] N'maria_fedorova';