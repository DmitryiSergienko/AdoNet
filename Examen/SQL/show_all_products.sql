CREATE OR ALTER PROCEDURE show_all_products
AS
BEGIN
	SELECT * FROM productsSet;
END

EXEC show_all_products;