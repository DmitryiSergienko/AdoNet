CREATE OR ALTER VIEW show_top_3_products AS
SELECT TOP(3) p.name, COUNT(op.product_id) AS count FROM productsSet AS p
JOIN order_productsSet AS op ON op.product_id = p.id
GROUP BY p.name
ORDER BY count DESC;

SELECT * FROM show_top_3_products;