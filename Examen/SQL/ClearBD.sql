USE Examen_ModelFirst; -- ⚠️ ЗАМЕНИ НА ИМЯ СВОЕЙ БД
GO

-- 1. Отключить все внешние ключи
EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
GO

-- 2. Удалить все триггеры
DECLARE @sql NVARCHAR(MAX) = ''
SELECT @sql += 'DROP TRIGGER [' + s.name + '].[' + t.name + ']; '
FROM sys.triggers t
INNER JOIN sys.objects o ON t.parent_id = o.object_id
INNER JOIN sys.schemas s ON o.schema_id = s.schema_id
WHERE t.is_ms_shipped = 0

IF LEN(@sql) > 0
    EXEC sp_executesql @sql
GO

-- 3. Удалить все внешние ключи
DECLARE @sql NVARCHAR(MAX) = ''
SELECT @sql += 'ALTER TABLE [' + SCHEMA_NAME(fk.schema_id) + '].[' + OBJECT_NAME(fk.parent_object_id) + '] DROP CONSTRAINT [' + fk.name + ']; '
FROM sys.foreign_keys fk

IF LEN(@sql) > 0
    EXEC sp_executesql @sql
GO

-- 4. Удалить все таблицы
EXEC sp_MSforeachtable 'DROP TABLE ?'
GO

-- 5. Удалить все хранимые процедуры
DECLARE @sql NVARCHAR(MAX) = ''
SELECT @sql += 'DROP PROCEDURE [' + SCHEMA_NAME(schema_id) + '].[' + name + ']; '
FROM sys.procedures WHERE is_ms_shipped = 0

IF LEN(@sql) > 0
    EXEC sp_executesql @sql
GO

-- 6. Удалить все функции
DECLARE @sql NVARCHAR(MAX) = ''
SELECT @sql += 'DROP FUNCTION [' + SCHEMA_NAME(schema_id) + '].[' + name + ']; '
FROM sys.objects WHERE type IN ('FN', 'IF', 'TF', 'FS', 'FT') AND is_ms_shipped = 0

IF LEN(@sql) > 0
    EXEC sp_executesql @sql
GO

-- 7. Удалить все представления
DECLARE @sql NVARCHAR(MAX) = ''
SELECT @sql += 'DROP VIEW [' + SCHEMA_NAME(schema_id) + '].[' + name + ']; '
FROM sys.views WHERE is_ms_shipped = 0

IF LEN(@sql) > 0
    EXEC sp_executesql @sql
GO

-- 8. Удалить пользовательские типы
DECLARE @sql NVARCHAR(MAX) = ''
SELECT @sql += 'DROP TYPE [' + SCHEMA_NAME(schema_id) + '].[' + name + ']; '
FROM sys.types WHERE is_user_defined = 1

IF LEN(@sql) > 0
    EXEC sp_executesql @sql
GO

-- 9. Удалить последовательности (SQL Server 2012+)
DECLARE @sql NVARCHAR(MAX) = ''
SELECT @sql += 'DROP SEQUENCE [' + SCHEMA_NAME(schema_id) + '].[' + name + ']; '
FROM sys.sequences WHERE is_ms_shipped = 0

IF LEN(@sql) > 0
    EXEC sp_executesql @sql
GO

PRINT '✅ База данных полностью очищена!'