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