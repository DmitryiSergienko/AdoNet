CREATE OR ALTER VIEW show_count_products_in_books_and_tools AS
SELECT c.name, SUM(wp.quantity) AS count_products FROM categoriesSet AS c
JOIN product_categoriesSet AS pc ON pc.category_id = c.id
JOIN productsSet AS p ON pc.product_id = p.id
JOIN warehouse_productsSet AS wp ON wp.product_id = p.id
WHERE c.name IN (N'Книги', N'Электроника')
GROUP BY c.name;

SELECT * FROM show_count_products_in_books_and_tools;