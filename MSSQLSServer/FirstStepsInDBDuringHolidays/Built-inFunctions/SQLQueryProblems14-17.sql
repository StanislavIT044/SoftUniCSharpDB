USE Diablo

--Problem14
SELECT TOP(50) [Name], FORMAT([Start], 'yyyy-MM-dd') AS [Start]
  FROM Games
 WHERE YEAR([Start]) BETWEEN 2011 AND 2012
ORDER BY [Start], [Name]

--Problem15
SELECT UserName, 
       SUBSTRING(Email, 
			CHARINDEX('@', Email) + 1, 
			LEN(Email)) 
			AS [Email Provirer]
  FROM Users
ORDER BY [Email Provirer], UserName

--Problem16
SELECT Username, IpAddress
  FROM Users
 WHERE IpAddress LIKE '___.1_%._%.___'
ORDER BY UserName

--Problem17
SELECT [Name],
  CASE
  WHEN DATEPART(HOUR, Start) >= 0 AND DATEPART(HOUR, Start) < 12 
	THEN 'Morning' 
  WHEN DATEPART(HOUR, Start) >= 12 AND DATEPART(HOUR, Start) < 18 
	THEN 'Afternoon'
	WHEN DATEPART(HOUR, Start) >= 18 AND DATEPART(HOUR, Start) < 24 
	THEN 'Evening'
  END AS [Part oh the Day],
  CASE
  WHEN Duration <= 3 THEN 'Extra Short'
  WHEN Duration BETWEEN 4 AND 6 THEN 'Short'
  WHEN Duration > 6 THEN 'Long'
  WHEN Duration IS NULL THEN 'Extra Long'
  END AS Duration
  FROM Games
ORDER BY [Name], Duration, [Part oh the Day]