CREATE OR ALTER PROCEDURE show_products_in_portions 
	@skip_rows			INT,
	@show_rows			INT
AS
BEGIN
	SELECT * FROM productsSet
	ORDER BY id
	OFFSET @skip_rows ROWS
	FETCH NEXT @show_rows ROWS ONLY;
END;

EXEC show_products_in_portions 2, 2;