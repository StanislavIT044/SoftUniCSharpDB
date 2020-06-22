USE SoftUni

--Problem01
SELECT 
		Employees.EmployeeID, 
		Employees.JobTitle, 
		Addresses.AddressID,
		Addresses.AddressText
  FROM Employees
  JOIN Addresses ON Addresses.AddressID = EmployeeID

--Problem02
SELECT TOP(50)
		Employees.FirstName, 
		Employees.LastName, 
		Towns.[Name] AS Town,
		Addresses.AddressText
  FROM Employees
 INNER JOIN Addresses ON Employees.AddressID = Addresses.AddressID
 INNER JOIN Towns ON Addresses.TownID = Towns.TownID 
 ORDER BY FirstName, LastName

--Problem03
SELECT EmployeeID, FirstName, LastName, d.[Name] AS DepartmentName
  FROM Employees AS e
  LEFT JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
 WHERE d.DepartmentID = 3
 ORDER BY EmployeeID

--Problem04
SELECT TOP (5) EmployeeID, FirstName, Salary, d.[Name] AS DepartmentName
  FROM Employees AS e
  JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
 WHERE Salary > 15000
 ORDER BY d.DepartmentID ASC

--Problem05
SELECT TOP (3) e.EmployeeID, e.FirstName
  FROM Employees AS e
  LEFT JOIN EmployeesProjects AS ep ON ep.EmployeeID = e.EmployeeID
 WHERE ep.ProjectID IS NULL
 ORDER BY e.EmployeeID

--Problem06
SELECT e.FirstName, e.LastName, e.HireDate, d.[Name] AS DeptName
  FROM Employees AS e
  JOIN Departments AS d ON d.DepartmentID = E.DepartmentID
 WHERE e.DepartmentID = 3 OR e.DepartmentID = 10 AND HireDate > '01.01.1999'
 ORDER BY HireDate ASC

--Problem07
SELECT TOP (5) e.EmployeeID, e.FirstName, p.[Name]
  FROM Employees AS e
  INNER JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
  JOIN Projects AS p ON p.ProjectID = ep.ProjectID
 WHERE p.StartDate > '08.13.2002' AND p.EndDate IS NULL
 ORDER BY e.EmployeeID

--Problem08
SELECT e.EmployeeID, e.FirstName,
  CASE 
       WHEN p.StartDate > '2005-01-01' THEN NULL
	   ELSE p.[Name]
   END AS ProjenctName
  FROM Employees AS e
  INNER JOIN EmployeesProjects AS ep ON ep.EmployeeID = e.EmployeeID
  INNER JOIN Projects AS p ON p.ProjectID = ep.ProjectID
 WHERE e.EmployeeID = 24

--Problem09
SELECT e.EmployeeID, e.FirstName, e.ManagerID, m.FirstName AS ManagerName
  FROM Employees AS e
  JOIN Employees AS m ON e.ManagerID = m.EmployeeID
 WHERE e.ManagerID = 3 OR e.ManagerID = 7

--Problem10
SELECT TOP (50)
		e.EmployeeID, 
		e.FirstName + ' ' + e.LastName AS EmployeeName, 
		m.FirstName + ' ' + m.LastName AS ManagerName,
		d.[Name] AS DepartmentName
  FROM Employees AS e
  JOIN Employees AS m ON e.ManagerID = m.EmployeeID
  JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
 ORDER BY e.EmployeeID

--Problem11
SELECT TOP(1) AVG(e.Salary) AS MinAverageSalary
  FROM Employees AS e
  JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
 GROUP BY d.[Name]
 ORDER BY AVG(e.Salary)