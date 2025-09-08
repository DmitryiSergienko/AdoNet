CREATE OR ALTER PROCEDURE search_orders_by_date
	@start_date			DATETIME,
	@end_date			DATETIME
AS
BEGIN
	SELECT * FROM ordersSet
	WHERE order_placement_date BETWEEN @start_date AND @end_date;
END;
GO

EXEC search_orders_by_date 
	@start_date = '2025-01-12 00:00:00', 
	@end_date = '2025-01-16 23:59:59';

SELECT * FROM ordersSet;