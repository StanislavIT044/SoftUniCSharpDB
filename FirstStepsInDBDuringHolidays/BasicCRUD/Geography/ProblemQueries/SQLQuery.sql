USE [Geography]

--Problem22
SELECT P.PeakName
  FROM Peaks AS P
  ORDER BY p.PeakName

--Problem23
SELECT TOP 30
     c.CountryName,
     c.Population
  FROM Countries AS c
  WHERE c.ContinentCode = 'EU'
  ORDER BY c.Population DESC

--Problem24
SELECT c.CountryName,
	   c.CountryCode,
	   CASE
	       WHEN c.CurrencyCode = 'EUR' THEN 'Euro'
		   ELSE 'Not Euro'
	   END AS [Currency]
  FROM Countries  AS c
ORDER BY C.CountryName