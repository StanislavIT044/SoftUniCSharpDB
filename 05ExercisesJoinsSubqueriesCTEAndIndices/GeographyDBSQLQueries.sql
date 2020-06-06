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