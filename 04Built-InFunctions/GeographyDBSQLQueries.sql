USE [Geography]

--Problem12
SELECT CountryName, ISOCode
  FROM Countries
 WHERE CountryName LIKE '%a%a%a%'
 ORDER BY IsoCode

--Problem13