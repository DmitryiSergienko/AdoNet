CREATE OR ALTER PROCEDURE [dbo].[stp_user_delete] 
    @id INT
AS
BEGIN
    DELETE FROM usersSet 
    WHERE id = @id;  -- Предполагается, что столбец с ID называется 'id'
END
GO

EXEC [dbo].[stp_user_delete] 7;