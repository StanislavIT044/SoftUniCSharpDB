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