CREATE DATABASE Relations

USE Relations

CREATE TABLE Passports(
	PassportID int PRIMARY KEY IDENTITY(101, 1) NOT NULL,
	PassportNumber varchar(50) NOT NULL
);

CREATE TABLE Persons(
	PersonID int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	FrirstName varchar(50) NOT NULL,
	Salary decimal(15, 2) NOT NULL,
	PassportID int FOREIGN KEY REFERENCES Passports(PassportID) NOT NULL
);

--ALTER TABLE Persons
--ADD UNIQUE (PassportId)

--ALTER TABLE Passports
--ADD UNIQUE (PassportNumber)


INSERT INTO Passports VALUES
('N34FG21B'),
('K65LO4R7'),
('ZE657QP2')

INSERT INTO Persons VALUES
('Roberto', 43300.00, 102),
('Tom', 56100.00, 103),
('Yana', 60200.00, 101)
