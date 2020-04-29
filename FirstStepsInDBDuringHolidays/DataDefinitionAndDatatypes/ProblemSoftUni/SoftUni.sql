CREATE DATABASE SoftUni

USE SoftUni

CREATE TABLE Towns(
Id INT PRIMARY KEY IDENTITY,
[Name]  VARCHAR(20) NOT NULL
)

CREATE TABLE Addresses(
Id INT PRIMARY KEY IDENTITY,
AddressesText  VARCHAR(20) NOT NULL,
TownId INT FOREIGN KEY REFERENCES Towns(Id)
)

CREATE TABLE Departments(
Id INT PRIMARY KEY IDENTITY,
[Name]  VARCHAR(50) NOT NULL
)

CREATE TABLE Employees(
Id INT PRIMARY KEY IDENTITY,
FirstName  VARCHAR(30),
MiddleName  VARCHAR(30),
LastName  VARCHAR(30),
JobTitle  VARCHAR(30),
DepartmentId INT FOREIGN KEY REFERENCES Departments(Id),
HireData DATETIME NOT NULL,
Salary DECIMAL(15, 2) NOT NULL,
AddressId INT FOREIGN KEY REFERENCES Addresses(Id)
)

INSERT INTO Towns([Name])
VALUES ('Sofia'),
	   ('Plovdiv'),
	   ('Varna'),
	   ('Burgas')

INSERT INTO Departments([Name])
VALUES ('Engineering'),
	   ('Sales'),
	   ('Marketing'),
	   ('Software Development'),
	   ('Quality Assurance')

INSERT INTO Employees(FirstName, JobTitle, DepartmentId, HireData, Salary)
VALUES ('Ivan Ivanov Ivanov', '.NET Developer', 4, '01/02/2013', 3500.00),
       ('Petar Petrov Petrov', 'Senior Engineer', 1, '02/03/2004', 4000.00),
	   ('Maria Petrova Ivanova', 'Intern', 5, '01/02/2013', 3500.00),
	   ('Ivan Ivanov Ivanov', '.NET Developer', 5, '01/02/2013', 3500.00),
	   ('Ivan Ivanov Ivanov', '.NET Developer', 4, '01/02/2013', 3500.00)

--Problem 19
SELECT * FROM Towns
SELECT * FROM Departments
SELECT * FROM Employees

--Problem 20
SELECT * FROM Towns
ORDER BY Name 

SELECT * FROM Departments
ORDER BY Name 

SELECT * FROM Employees
ORDER BY Salary DESC

--Problem 21
SELECT Name From Towns
ORDER BY Name

SELECT Name From Departments
ORDER BY Name

SELECT FirstName, LastName, JobTitle, Salary From Employees
ORDER BY Salary DESC


--Problem 22
UPDATE Employees
SET Salary = Salary * 1.1
SELECT Salary FROM Employees