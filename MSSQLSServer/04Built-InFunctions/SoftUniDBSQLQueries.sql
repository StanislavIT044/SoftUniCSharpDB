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

--Problem06
SELECT TownID, [Name]
  FROM Towns
 WHERE [Name] LIKE '[MKBE]%'
ORDER BY [Name]

--Problem07
SELECT TownID, [Name]
  FROM Towns
 WHERE [Name] NOT LIKE '[RBD]%'
ORDER BY [Name]

--Problem08
CREATE VIEW V_EmployeesHiredAfter2000 AS
SELECT FirstName, LastName
  FROM Employees
 WHERE YEAR(HireDate) > '2000'

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

--Problem11
SELECT *
  FROM (
		SELECT EmployeeID, FirstName, LastName, Salary,
			DENSE_RANK() OVER (PARTITION BY Salary ORDER BY EmployeeID) AS RANK
		FROM Employees
		WHERE Salary BETWEEN 10000 AND 50000) AS NewTable
 WHERE Rank = 2
 ORDER BY Salary DESC