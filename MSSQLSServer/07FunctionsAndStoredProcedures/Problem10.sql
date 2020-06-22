CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan (@balance INT)
AS
BEGIN
	SELECT ah.FirstName, ah.LastName FROM AccountHolders AS ah
	  JOIN Accounts AS a ON ah.Id = a.AccountHolderId
	 GROUP BY a.AccountHolderId, ah.FirstName, ah.LastName
	HAVING SUM(a.Balance) > @balance
	 ORDER BY FirstName, LastName
END