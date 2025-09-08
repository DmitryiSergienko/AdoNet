CREATE VIEW show_number_of_users_orders AS
SELECT u.login, COUNT(o.id) as count_orders FROM usersSet AS u
JOIN ordersSet AS o ON o.user_id = u.id
GROUP BY u.login;

SELECT * from show_number_of_users_orders;