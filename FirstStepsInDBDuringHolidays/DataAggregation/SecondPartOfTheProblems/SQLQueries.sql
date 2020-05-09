USE SoftUni

--Problem13
SELECT DepartmentID, SUM(Salary) AS TotalSum
  FROM Employees
GROUP BY DepartmentID

--Problem14
SELECT DepartmentID, MIN(Salary) AS TotalSum
  FROM Employees
 WHERE DepartmentID IN (2, 5, 7) AND HireDate > '01/01/2000'
GROUP BY DepartmentID

--Problem15
SELECT * INTO NewEmployeeTable
  FROM Employees
 WHERE Salary > 30000

DELETE FROM NewEmployeeTable
 WHERE ManagerID = 42

UPDATE NewEmployeeTable						
   SET Salary += 5000
 WHERE DepartmentID = 1

SELECT DepartmentID, AVG(Salary) AS AverageSalary
  FROM NewEmployeeTable
GROUP BY DepartmentID

--Problem16
SELECT DepartmentID, MAX(Salary) AS MaxSalary
  FROM Employees
GROUP BY DepartmentID
HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000

--Problem17
SELECT COUNT(*)
  FROM Employees
 WHERE ManagerID IS NULL

--Problem18
SELECT k.DepartmentID, k.Salary
  FROM(
SELECT DepartmentID, Salary, DENSE_RANK() OVER (PARTITION BY DepartmentID ORDER BY Salary DESC) AS SalaryRank
  FROM Employees) AS k
 WHERE k.SalaryRank = 3

 --Problem19
SELECT TOP(10) FirstName, LastName, DepartmentID, Salary
  FROM Employees AS e
 WHERE Salary > (SELECT AVG(Salary) FROM Employees AS em WHERE em.DepartmentID = e.DepartmentID)
 ORDER BY DepartmentID