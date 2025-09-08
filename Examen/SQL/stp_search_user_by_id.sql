CREATE OR ALTER PROCEDURE [dbo].[stp_search_user_by_id]
	@id					INT
AS
BEGIN
SELECT * FROM usersSet
WHERE id = @id;
END

EXEC [dbo].[stp_search_user_by_id] 3;