--Problem01
CREATE DATABASE Minions
USE Minions

--Problem02
CREATE TABLE Minions
(
	Id INT PRIMARY KEY,
	[Name] NVARCHAR(20) NOT NULL,
	Age INT
)

CREATE TABLE Towns
(
	Id INT PRIMARY KEY,
	[Name] NVARCHAR(30) NOT NULL
)

--Problem03
ALTER TABLE Minions
ADD TownId INT FOREIGN KEY REFERENCES Towns(Id)

--Problem04
INSERT INTO Towns
VALUES (1, 'Sofia'),
	   (2, 'Plovdiv'),
	   (3, 'Varna')

INSERT INTO Minions
VALUES (1, 'Kevin', 22, 1),
	   (2, 'Bob', 15, 3),
	   (3, 'Steward', NULL, 2)

--Problem05
TRUNCATE TABLE Minions

--Problem06
DROP TABLE Minions
DROP TABLE Towns

--Problem07
CREATE TABLE People
(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	[Name] NVARCHAR(200) NOT NULL,
	Picture NVARCHAR(MAX),
	Height DECIMAL(15, 2),
	[Weight] DECIMAL(15, 2),
	Gender CHAR(1) NOT NULL,
	Birthdate DATE NOT NULL,
	Biography NVARCHAR(MAX)
)

INSERT INTO People
VALUES ('Gosho', 'asdfasdfasdg', 1.79, 1.2, 'm', '05-05-2020', 'asdasasd'),
	   ('Gosho', 'asdfasdfasdg', 1.79, 1.2, 'm', '05-05-2020', 'asdasasd'),
	   ('Gosho', 'asdfasdfasdg', 1.79, 1.2, 'm', '05-05-2020', 'asdasasd'),
	   ('Gosho', 'asdfasdfasdg', 1.79, 1.2, 'm', '05-05-2020', 'asdasasd'),
	   ('Gosho', 'asdfasdfasdg', 1.79, 1.2, 'm', '05-05-2020', 'asdasasd')

--Problem08
--CREATE TABLE Users
--(
	
--)