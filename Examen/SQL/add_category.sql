CREATE OR ALTER PROCEDURE add_category
	@name			NVARCHAR(200),
	@description	NVARCHAR(1000)
AS
BEGIN
	INSERT INTO categoriesSet (name, description) VALUES
	(@name, @description);
	SELECT * FROM categoriesSet
	WHERE id = @@IDENTITY;
END

EXEC add_category N'Еда', N'Съедобная еда';

-- Для отображения, если нужно
SELECT * FROM categoriesSet;

-- Для очистки, если нужно
DELETE FROM categoriesSet WHERE id = 6;