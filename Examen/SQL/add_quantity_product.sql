CREATE OR ALTER PROCEDURE add_quantity_product
	@product_id				INT,
	@warehouse_id			INT,
	@quantity				INT
AS
BEGIN
	INSERT INTO warehouse_productsSet (product_id, warehouse_id, quantity) VALUES
	(@product_id, @warehouse_id, @quantity);
END;

EXEC add_quantity_product 6, 1, 10;
EXEC add_quantity_product 6, 2, 7;
EXEC add_quantity_product 6, 3, 8;
	
-- Для отображения, если нужно
SELECT * FROM warehouse_productsSet;

-- Для очистки, если нужно
DELETE FROM warehouse_productsSet WHERE product_id = 6;