CREATE OR ALTER PROCEDURE search_products_by_name
	@name			NVARCHAR(200)
AS
BEGIN
	SELECT * FROM productsSet
	WHERE name like '%' + @name + '%';
END;
GO

EXEC search_products_by_name N'Книга';