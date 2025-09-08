CREATE OR ALTER PROCEDURE [dbo].[stp_search_admin_by_id]
	@id					INT
AS
BEGIN
SELECT * FROM adminsSet
WHERE id = @id;
END

EXEC [dbo].[stp_search_admin_by_id] 3;