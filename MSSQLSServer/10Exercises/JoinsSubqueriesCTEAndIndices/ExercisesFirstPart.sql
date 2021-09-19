USE SoftUni

--01. Employee Address
SELECT TOP(5) e.EmployeeID, e.JobTitle, e.AddressID, a.AddressText
  FROM Employees AS e
  JOIN Addresses AS a ON e.AddressID = a.AddressID
 ORDER BY AddressID

--02. Addresses with Towns
SELECT TOP(50) e.FirstName, e.LastName, t.[Name], a.AddressText
  FROM Employees AS e
  JOIN Addresses AS a ON e.AddressID = a.AddressID
  JOIN Towns AS t ON a.TownID = t.TownID
 ORDER BY e.FirstName, e.LastName

--03. Sales Employees
SELECT e.EmployeeID, e.FirstName, e.LastName, d.[Name]
  FROM Employees AS e
  JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
 WHERE e.DepartmentID = 3
 ORDER BY e.EmployeeID
 
 SELECT * FROM Departments

--04. Employee Departments
SELECT TOP(5) e.EmployeeID, e.FirstName, e.Salary, d.[Name]
  FROM Employees AS e
  JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
 WHERE e.Salary > 15000
 ORDER BY d.DepartmentID

--05. Employees Without Projects
SELECT TOP(3) e.EmployeeID, e.FirstName
  FROM Employees AS e
  LEFT JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
  LEFT JOIN Projects AS p ON ep.ProjectID = p.ProjectID
 WHERE ep.ProjectID IS NULL
 ORDER BY e.EmployeeID

--06. Employees Hired After
SELECT TOP(5) e.FirstName, e.LastName, e.HireDate, d.[Name]
  FROM Employees AS e
  JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
 WHERE e.HireDate > '01-01-1999' AND e.DepartmentID = 10 OR e.DepartmentID = 3
 ORDER BY HireDate 

--07. Employees With Project



