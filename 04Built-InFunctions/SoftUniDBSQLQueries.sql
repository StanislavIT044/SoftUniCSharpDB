USE SoftUni

--Problem01
SELECT FirstName, LastName
  FROM Employees
 WHERE FirstName Like 'Sa%'

--Problem02
SELECT FirstName, LastName
  FROM Employees
 WHERE LastName Like '%ei%'

--Problem03
SELECT FirstName
  FROM Employees
 WHERE DepartmentID IN (3, 10) AND DATEPART(YEAR, HireDate) BETWEEN '1995' AND '2005'
 
--Problem04
SELECT FirstName, LastName
  FROM Employees
 WHERE JobTitle NOT LIKE '%engineer%'

--Problem05
SELECT [Name]
  FROM Towns
 WHERE LEN([Name]) BETWEEN 5 AND 6
ORDER BY [Name]