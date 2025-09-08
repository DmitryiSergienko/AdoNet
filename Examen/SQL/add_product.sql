CREATE OR ALTER PROCEDURE add_product
	@name			NVARCHAR(200),
	@description	NVARCHAR(1000),
	@price			DECIMAL(18,2)
AS
BEGIN
	INSERT INTO productsSet (name, description, price) VALUES
	(@name, @description, @price);
END

EXEC add_product N'Чипсы', N'Очень вкусные чипсы', 145.00;

-- Для отображения, если нужно
SELECT * FROM productsSet;

-- Для очистки, если нужно
DELETE FROM productsSet WHERE id = 6;