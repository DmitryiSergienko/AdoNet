CREATE OR ALTER PROCEDURE [dbo].[stp_users_all]
AS
BEGIN
SELECT * FROM usersSet;
END

EXEC [dbo].[stp_users_all];