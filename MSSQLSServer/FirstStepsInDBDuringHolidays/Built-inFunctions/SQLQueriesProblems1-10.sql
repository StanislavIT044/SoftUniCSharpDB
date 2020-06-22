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

--Problem05
SELECT [Name]
  FROM Towns
 WHERE LEN([Name]) = 5 OR LEN([Name]) = 6
 ORDER BY [Name]

--Problem06
SELECT TownID, [Name]
  FROM Towns
 WHERE [Name] LIKE '[mkbe]%'
ORDER BY [Name]

--Problem07
SELECT TownID, [Name]
  FROM Towns
 WHERE [Name] NOT LIKE '[rbd]%'
ORDER BY [Name]

--Problem08
CREATE VIEW V_EmployeesHiredAfter2000 AS
SELECT FirstName, LastName
  FROM Employees
 WHERE YEAR(HireDate) > 2000 

--Problem09
SELECT FirstName, LastName
  FROM Employees
 WHERE LEN(LastName) = 5

--Problem10
SELECT EmployeeID, FirstName, LastName, Salary,
DENSE_RANK() OVER (PARTITION BY Salary ORDER BY EmployeeID) AS RANK
  FROM Employees
 WHERE Salary BETWEEN 10000 AND 50000
ORDER BY Salary DESC