CREATE TRIGGER LimitNotificationTableSize
ON Notifications
AFTER INSERT
as
DECLARE @tableCount int
SELECT @tableCount = Count(*)
FROM Notifications

IF @tableCount > 15
begin
WITH CTE AS
(
SELECT TOP 6 *
FROM Notifications
ORDER BY nID
)
DELETE FROM CTE
end