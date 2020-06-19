--Problem02
INSERT INTO Employees (FirstName, LastName, Birthdate, DepartmentId)VALUES 
('Marlo', 'O''Malley', '1958-09-21', 1),
('Niki', 'Stanaghan', '1969-11-26', 4),
('Ayrton', 'Senna', '1960-03-21', 9),
('Ronnie', 'Peterson', '1944-02-14', 9),
('Giovanna', 'Amati', '1959-07-20', 5)

INSERT INTO Reports (CategoryId, StatusId, OpenDate, CloseDate, Description, UserId, EmployeeId)VALUES 
(1, 1, '2017-04-13', NULL, 'Stuck Road on Str.133', 6, 2),
(6, 3, '2015-09-05', '2015-12-06', 'Charity trail running', 3, 5),
(14, 2, '2015-09-07', NULL, 'Falling bricks on Str.58', 5, 2),
(4, 3, '2017-07-03', '2017-07-06', 'Cut off streetlight on Str.11', 1, 1)

--Problem03
UPDATE Reports
SET CloseDate = GETDATE()
WHERE CloseDate IS NULL

--Problem04
SELECT [Description], FORMAT(OpenDate, 'dd-MM-yyyy')
  FROM Reports
 ORDER BY Reports.OpenDate ASC, Description ASC

--Problem05
SELECT [Description], FORMAT(OpenDate, 'dd-MM-yyyy')
  FROM Reports
 WHERE EmployeeId IS NULL
ORDER BY Reports.OpenDate ASC, Description ASC

--Problem06
SELECT r.[Description], c.[Name]
  FROM Reports AS r
  JOIN Categories AS c ON c.Id = r.CategoryId
 ORDER BY [Description] ASC, [Name] ASC

--Problem07
SELECT TOP(5) c.[Name], COUNT(c.Id) AS ReportsNumber
  FROM Reports AS r
  JOIN Categories AS c ON r.CategoryId = c.Id
 GROUP BY c.Id, c.[Name]
 ORDER BY ReportsNumber DESC, c.[Name] ASC

--Problem08
SELECT u.Username, c.[Name] AS CategoryName
  FROM Reports AS r
  JOIN Users AS u ON r.UserId = u.Id
  JOIN Categories AS c ON c.Id = r.CategoryId 
 WHERE DATEPART(DAY, r.OpenDate) = DATEPART(DAY, u.Birthdate) AND
	   DATEPART(MONTH, r.OpenDate) = DATEPART(MONTH, u.Birthdate) 
 ORDER BY Username ASC, CategoryName ASC

--Problem09
SELECT e.FirstName + ' ' + LastName AS FullName,
	   (SELECT COUNT(DISTINCT UserId) FROM Reports WHERE EmployeeId = e.Id) AS UsersCount
  FROM Employees AS e
 ORDER BY UsersCount DESC, FullName ASC

--Problem11
CREATE FUNCTION udf_HoursToComplete(@StartDate DATETIME, @EndDate DATETIME)
RETURNS INT
AS
BEGIN
	IF (@StartDate IS NULL)
	BEGIN
		RETURN 0;
	END

	IF (@EndDate IS NULL)
	BEGIN
		RETURN 0;
	END

	RETURN DATEDIFF(hour, @StartDate, @EndDate);
END