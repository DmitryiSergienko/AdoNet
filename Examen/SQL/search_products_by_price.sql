CREATE OR ALTER PROCEDURE search_products_by_price
	@start_price	INT,
	@end_price		INT
AS
BEGIN
	SELECT * FROM productsSet
	WHERE price >= @start_price AND price <= @end_price
	ORDER BY price DESC;
END;

EXEC search_products_by_price 990, 1500;