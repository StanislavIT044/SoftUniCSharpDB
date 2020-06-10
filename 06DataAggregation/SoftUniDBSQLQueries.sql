USE SoftUni

--Problem13
SELECT DepartmentID, SUM(Salary) AS TotalSum
  FROM Employees
 GROUP BY DepartmentID

--Problem14
SELECT DepartmentID, MIN(Salary) AS TotalSum
  FROM Employees
  WHERE HireDate > '01-01-2000'
 GROUP BY DepartmentID
HAVING DepartmentID = 2 OR
	   DepartmentID = 5 OR
	   DepartmentID = 7

--Problem15
SELECT * INTO NewEmployeeTable
  FROM Employees
 WHERE Salary > 30000

DELETE FROM NewEmployeeTable
 WHERE ManagerID = 42

UPDATE NewEmployeeTable
   SET Salary += 5000
 WHERE DepartmentID = 1

SELECT DepartmentID, AVG(Salary)
  FROM NewEmployeeTable
 GROUP BY DepartmentID

--Problem16
SELECT DepartmentID, MAX(Salary) AS MaxSalary
  FROM Employees
 GROUP BY DepartmentID
HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000

--Problem17
SELECT COUNT(*) AS [Count]
  FROM Employees
 WHERE ManagerID IS NULL