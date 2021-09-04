USE SoftUni

--Problem02
SELECT * 
  FROM Departments

--Problem03
SELECT [Name] 
  FROM Departments

--Problem04
SELECT FirstName, LastName, Salary
  FROM Employees

--Problem05
SELECT FirstName, MiddleName, LastName
  FROM Employees

--Problem06
SELECT FirstName + '.' + LastName + '@softuni.bg' AS 'Full Email Address'
  FROM Employees

--Problem07
SELECT DISTINCT Salary
  FROM Employees

--Problem08
SELECT *
  FROM Employees
 WHERE JobTitle = 'Sales Representative'

--Problem09
SELECT FirstName, LastName, JobTitle
  FROM Employees
 WHERE Salary BETWEEN 20000 AND 30000

--Problem10
SELECT FirstName + ' ' + MiddleName + ' ' + LastName AS 'Full Name'
  FROM Employees
 WHERE Salary = 25000 OR Salary = 14000 OR Salary = 12500 OR Salary = 23600 

--Problem11
SELECT FirstName, LastName
  FROM Employees
 WHERE ManagerID IS NULL

--Problem12
SELECT FirstName, LastName, Salary
  FROM Employees
 WHERE Salary > 50000
 ORDER BY Salary DESC

--Problem13
SELECT FirstName, LastName
  FROM Employees
 WHERE DepartmentID != 4

--Problem14
SELECT *
  FROM Employees
 ORDER BY Salary DESC, FirstName, LastName DESC, MiddleName

--Problem15
CREATE VIEW [V_EmployeesSalaries] AS
SELECT FirstName, LastName, Salary
  FROM Employees

--Problem16
CREATE VIEW [V_EmployeeNameJobTitle] AS
SELECT 
	FirstName + ' ' + ISNULL(MiddleName, '') + ' ' + LastName AS [Full Name],
	JobTitle AS [Job Title]
FROM Employees 

SELECT DISTINCT JobTitle
  FROM Employees

UPDATE Employees
   SET Salary = Salary * 1.12
 WHERE DepartmentID IN (1, 2, 4, 11)

SELECT Salary  
  FROM Employees