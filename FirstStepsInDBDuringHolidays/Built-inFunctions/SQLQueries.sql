USE SoftUni

--Problem01
SELECT FirstName, LastName
  FROM Employees
 WHERE FirstName LIKE 'SA%'


--Problem02
 SELECT FirstName, LastName
  FROM Employees
 WHERE LastName LIKE '%ei%'

--Problem03
SELECT FirstName
  FROM Employees
 WHERE DepartmentID IN(3, 10) AND DATEPART(YEAR, HireDate) BETWEEN 1995 AND 2005

--Problem04
SELECT FirstName, LastName
  FROM Employees
 WHERE JobTitle NOT LIKE '%engineer%'