CREATE DATABASE Minions

USE Minions

CREATE TABLE Minions(
Id INT PRIMARY KEY IDENTITY(1,1),
[Name] NVARCHAR(20) NOT NULL,
Age INT 
)

CREATE TABLE Towns(
Id INT PRIMARY KEY IDENTITY(1,1),
[Name] NVARCHAR(20) NOT NULL,
)

ALTER TABLE Minions
ADD TownId INT FOREIGN KEY REFERENCES Towns(Id)

SET IDENTITY_INSERT Towns OFF

INSERT INTO Towns(Id, [Name])
VALUES (1, 'Sofia'),
       (2, 'Plovdiv'),
	   (3, 'Varna')

INSERT INTO Minions(Id, [Name], Age, TownId)
VALUES (1, 'Kevin', 22, 1),
	   (2, 'Bob', 15, 3),
	   (3, 'Steward', NULL, 2)

SET IDENTITY_INSERT Minions ON
--Problems 1, 2, 3, 4

--Problem 5
TRUNCATE TABLE MINIONS

--Problem 6
DROP TABLE Minions
DROP TABLE Towns

--Problem 7
CREATE TABLE People(
Id INT PRIMARY KEY IDENTITY(1, 1),
[Name] NVARCHAR(200) NOT NULL,
Picture NVARCHAR(MAX),
Height DECIMAL(15,2),
[Weight] DECIMAL(15,2),
Gender CHAR(1) NOT NULL,
Birthdate DATE NOT NULL,
Biography NVARCHAR(MAX)
)

SET IDENTITY_INSERT People ON

INSERT INTO People(Id, [Name], Picture, Height, [Weight], Gender, Birthdate, Biography)
VALUES (1, 'Gosho','asdqweasdasd', 1.79999, 100.01, 'm', '1984-02-29', 'I am Gosho'),
	   (2, 'Gosho','asdqweasdasd', 1.79999, 100.01, 'm', '1984-02-29', 'I am Gosho'),
	   (3, 'Gosho','asdqweasdasd', 1.79999, 100.01, 'm', '1984-02-29', 'I am Gosho'),
	   (4, 'Gosho','asdqweasdasd', 1.79999, 100.01, 'm', '1984-02-29', 'I am Gosho'),
	   (5, 'Gosho','asdqweasdasd', 1.79999, 100.01, 'm', '1984-02-29', 'I am Gosho')