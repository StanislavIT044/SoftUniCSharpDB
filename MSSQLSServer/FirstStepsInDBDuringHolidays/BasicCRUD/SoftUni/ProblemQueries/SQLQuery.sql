USE SoftUni

--Problem02
SELECT * FROM Departments

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
SELECT FirstName + '.' + LastName + '@softuni.bg'
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
SELECT FirstName + ' ' + MiddleName  + ' ' + LastName
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
SELECT TOP(5) FirstName, LastName
  FROM Employees
  ORDER BY Salary DESC

--Problem14
SELECT FirstName, LastName
  FROM Employees
  WHERE DepartmentID != 4

--Problem15
SELECT *
  FROM Employees
  ORDER	BY Salary DESC, FirstName, LastName DESC, MiddleName

--Problem16
CREATE VIEW v_EmployeesSalaries AS
SELECT
    e.FirstName,
	e.LastName,
	e.Salary
  FROM Employees AS e

--Problem17
CREATE VIEW V_EmployeeNameJobTitle AS
SELECT
    e.FirstName + ' ' +  ISNULL(e.MiddleName, '') + ' ' + e.LastName AS [Full Name] ,
	e.JobTitle AS [Job Title]
  FROM Employees AS e

--Problem18
SELECT DISTINCT JobTitle
  FROM Employees

--Problem19
SELECT TOP(10) *
  FROM Projects
  ORDER BY StartDate, [Name]

--Problem20
SELECT TOP(7) FirstName, LastName, HireDate
  FROM Employees
  ORDER BY HireDate DESC

--Problem21
UPDATE Employees
  SET Salary += Salary * 0.12
  WHERE DepartmentID IN (1, 2, 4, 11)

SELECT e.Salary
  FROM Employees AS e