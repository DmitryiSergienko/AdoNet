CREATE OR ALTER VIEW show_count_products_in_category AS
SELECT c.name, SUM(wp.quantity) AS count_products FROM categoriesSet AS c
JOIN product_categoriesSet AS pc ON pc.category_id = c.id
JOIN productsSet AS p ON pc.product_id = p.id
JOIN warehouse_productsSet AS wp ON wp.product_id = p.id
GROUP BY c.name;

SELECT * FROM show_count_products_in_category
ORDER BY count_products DESC;