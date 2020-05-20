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
CREATE TABLE Users
(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	Username VARCHAR(30) NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture NVARCHAR(MAX),
	LastLoginTime DATETIME2,
	IsDeleted BIT NOT NULL
)

INSERT INTO Users
VALUES ('Gosho', 'asdfasdfasdg', 'dss', '05-05-2020', 0),
	   ('Gosho', 'asdfasdfasdg', 'dss', '05-05-2020', 0),
	   ('Gosho', 'asdfasdfasdg', 'dss', '05-05-2020', 0),
	   ('Gosho', 'asdfasdfasdg', 'dss', '05-05-2020', 0),
	   ('Gosho', 'asdfasdfasdg', 'dss', '05-05-2020', 0)

--Problem09
ALTER TABLE Users
DROP CONSTRAINT [PK_Users]

ALTER TABLE Users
ADD CONSTRAINT PK_Users PRIMARY KEY (Id, Username)

--Problem10
ALTER TABLE Users
ADD CHECK (LEN([Password]) >= 5)

--Problem11
ALTER TABLE Users
ADD CONSTRAINT DF_Users_LastLoginTime 
DEFAULT GETDATE() FOR LastLoginTime

--Problem12
ALTER TABLE Users
DROP CONSTRAINT [PK_Users]

ALTER TABLE Users
ADD CONSTRAINT PK_Users_Id
PRIMARY KEY (Id)

TRUNCATE TABLE Users

ALTER TABLE Users
ADD CONSTRAINT CK_Username_Unique
UNIQUE (Username)

ALTER TABLE Users
ADD CHECK (LEN(Username) >= 3)