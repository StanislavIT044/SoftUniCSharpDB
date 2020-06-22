--Problem 16
CREATE DATABASE SoftUni

USE SoftUni

CREATE TABLE Towns(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30)
)

CREATE TABLE Addresses(
	Id INT PRIMARY KEY IDENTITY,
	AddressText NVARCHAR(50),
	TownId INT FOREIGN KEY REFERENCES Towns(Id)
)

CREATE TABLE Departments(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50),
)

CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	MiddleName  NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	JobTitle NVARCHAR(50) NOT NULL,
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id),
	HireDate DATE NOT NULL,
	Salary DECIMAL(15, 3), 
	AddressId INT
)

--Problem18
INSERT INTO Towns
VALUES ('Sofia'),
	   ('Plovdiv'),
	   ('Varna'),
	   ('Burgas')

INSERT INTO Departments
VALUES ('Engineering'),
	   ('Sales'),
	   ('Marketing'),
	   ('Software Development'),
	   ('Quality Assurance')

INSERT INTO Employees
VALUES ('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '2013-02-01', 3500.00, 1),
	   ('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '2004-03-02', 4000.00, 2),
	   ('Maria', 'Petrova', 'Ivanova', 'Intern', 5, '2016-08-28', 525.25, 3),
	   ('Georgi', 'Teziev', 'Ivanov', 'CEO', 2, '2007-12-09', 3000.00, 4),
	   ('Peter', 'Pan', 'Pan', 'Intern', 3, '2016-08-28', 599.88, 5)

--Problem19
SELECT * 
  FROM Towns

SELECT * 
  FROM Departments

SELECT * 
  FROM Employees

--Problem20
SELECT * 
  FROM Towns
ORDER BY [Name]

SELECT * 
  FROM Departments
ORDER BY [Name]

SELECT * 
  FROM Employees
ORDER BY Salary DESC

--Problem21
SELECT [Name]
  FROM Towns
ORDER BY [Name]

SELECT [Name]
  FROM Departments
ORDER BY [Name]

SELECT FirstName, LastName, JobTitle, Salary
  FROM Employees
ORDER BY Salary DESC

--Problem22
UPDATE Employees
SET Salary *= 1.1

SELECT Salary
  FROM Employees