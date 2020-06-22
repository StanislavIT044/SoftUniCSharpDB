CREATE DATABASE MyDemoDB

USE MyDemoDB

--Problem01
CREATE TABLE Persons
(
	PersonID INT PRIMARY KEY,
	FirstName VARCHAR(20) NULL,
	Salary DECIMAL(15, 2),
	PassportID INT NOT NULL
)

CREATE TABLE Passports
(
	PassportID INT PRIMARY KEY,
	PassportNumber CHAR(8) NOT NULL
)

ALTER TABLE Persons
ADD CONSTRAINT FK_Persons_Passports FOREIGN KEY (PassportID) REFERENCES Passports (PassportID)

ALTER TABLE Persons
ADD UNIQUE (PassportID)

ALTER TABLE Passports
ADD UNIQUE (PassportNumber)

INSERT INTO Passports (PassportID, PassportNumber) VALUES
(101, 'N34FG21B'),
(102, 'K65LO4R7'),
(103, 'ZE657QP2')

INSERT INTO Persons (PersonID, FirstName, Salary, PassportID) VALUES
(1, 'Roberto', 43300.00, 102),
(2, 'Tom', 56100.00, 103),
(3, 'YaFGna', 60200.00, 101)


--Problem02
CREATE TABLE Manufacturers
(
	ManufacturerID INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(15) NOT NULL,
	EstablishedOn DATE NOT NULL
)

CREATE TABLE Models
(
	ModelID INT PRIMARY KEY IDENTITY(101, 1),
	[Name] VARCHAR(15) NOT NULL,
	ManufacturerID INT FOREIGN KEY REFERENCES Manufacturers(ManufacturerID)
)

SET IDENTITY_INSERT Manufacturers OFF

INSERT INTO Manufacturers (ManufacturerID, [Name], EstablishedOn) VALUES
	(1, 'BMW', '07/03/1916'),
	(2, 'Tesla', '01/01/2003'),
	(3, 'Lada', '01/05/1966')

INSERT INTO Models ([Name], ManufacturerID) VALUES
	('X1', 1),
	('i6', 1),
	('Model S', 2),
	('Model X', 2),
	('Model 3', 2),
	('Nova', 3)

SELECT * FROM Models

--Problem03 
CREATE TABLE Students
(
	StudentID INT,
	[Name] VARCHAR(20)
)

CREATE TABLE Exams
(
	ExamID INT,
	[Name] NVARCHAR(20)
)

CREATE TABLE StudentsExams
(
	StudentID INT,
	ExamID INT
)

ALTER TABLE StudentsExams
ALTER COLUMN StudentID INT NOT NULL
ALTER COLUMN ExamID INT NOT NULL

ALTER TABLE StudentsExams
ADD CONSTRAINT PK_StudentsExams PRIMARY KEY (StudentID, ExamID)


ALTER TABLE Students
ALTER COLUMN StudentID INT NOT NULL

ALTER TABLE Students
ADD CONSTRAINT PK_Students PRIMARY KEY (StudentID)


ALTER TABLE Exams
ALTER COLUMN ExamID INT NOT NULL

ALTER TABLE Exams
ADD CONSTRAINT PK_Exams PRIMARY KEY (ExamID)