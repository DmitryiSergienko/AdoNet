CREATE OR ALTER VIEW show_users_without_password AS
SELECT * FROM usersSet
WHERE password IS NULL;

SELECT * FROM show_users_without_password;