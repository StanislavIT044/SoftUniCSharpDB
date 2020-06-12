CREATE PROCEDURE usp_EmployeesBySalaryLevel(@salaryLevel VARCHAR(8))
AS
	SELECT FirstName AS [First Name],
		   LastName AS [Last Name]
	  FROM Employees
	 WHERE dbo.ufn_GetSalaryLevel(Salary) = @salaryLevel