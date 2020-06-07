USE [Geography]

--Problem12
SELECT mc.CountryCode, m.MountainRange, p.PeakName, p.Elevation
  FROM Peaks AS p
  JOIN Mountains AS m ON p.MountainId = m.Id
  JOIN MountainsCountries AS mc ON mc.MountainId = p.MountainId
 WHERE mc.CountryCode = 'BG' AND p.Elevation > 2835
 ORDER BY p.Elevation DESC

--Problem13
SELECT CountryCode, COUNT(m.MountainRange) AS MountainRanges 
  FROM Mountains AS m  
  JOIN MountainsCountries AS mc ON mc.MountainId = m.Id
 WHERE mc.CountryCode IN ('BG', 'RU', 'US')
 GROUP BY mc.CountryCode

--Problem14
SELECT TOP(5) c.CountryName, r.RiverName
  FROM Countries AS c
  LEFT JOIN CountriesRivers AS cr ON cr.CountryCode = c.CountryCode
  LEFT JOIN Rivers AS r ON cr.RiverId = r.Id
 WHERE ContinentCode = 'AF'
 ORDER BY CountryName

--Problem15
SELECT ContinentCode, CurrencyCode, CurrencyCount AS CurrencyUsage
  FROM (
	   SELECT ContinentCode, 
       CurrencyCode,
	   CurrencyCount,
	   DENSE_RANK() OVER
	   (PARTITION BY ContinentCode ORDER BY CurrencyCount DESC) AS CurrencyRank
	   FROM (
		       SELECT ContinentCode,
		       	   CurrencyCode,
		       	   COUNT(*) AS [CurrencyCount]
		         FROM Countries
		        GROUP BY ContinentCode, CurrencyCode
				) AS [CurrencyCountQuery]
 WHERE CurrencyCount > 1 ) AS CurrencyRanking
 WHERE CurrencyRank = 1
 ORDER BY ContinentCode 

--Problem16
SELECT * 
  FROM MountainsCountries