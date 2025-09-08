CREATE OR ALTER VIEW show_top_3_users_by_number_of_orders AS
SELECT TOP(3) u.login, COUNT(o.id) as count_orders FROM usersSet AS u
JOIN ordersSet AS o ON o.user_id = u.id
GROUP BY u.login
ORDER BY count_orders DESC;

SELECT * FROM show_top_3_users_by_number_of_orders;