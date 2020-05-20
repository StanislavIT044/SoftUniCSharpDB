CREATE DATABASE Movies
 
USE Movies

CREATE TABLE Directors
(
	Id INT PRIMARY KEY IDENTITY,
	DirectorName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Genres
(
	Id INT PRIMARY KEY IDENTITY,
	GenreName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Categories
(
	Id INT PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Movies
(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(50) NOT NULL,
	DirectorId INT FOREIGN KEY REFERENCES Directors(Id) NOT NULL,
	CopyrightYear DATE NOT NULL,
	[Length] DATETIME2 NOT NULL,
	GenreId INT FOREIGN KEY REFERENCES Genres(Id) NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	Rating DECIMAL(3, 1),
	Notes NVARCHAR(MAX)
)

INSERT INTO Directors
VALUES ('Gosho', NULL),
	   ('Pesho', NULL),
	   ('Sashe', NULL),
	   ('Stafata', NULL),
	   ('Avgustin', NULL)

INSERT INTO Genres
VALUES ('Strashen film', NULL),
	   ('Smeshen film', NULL),
	   ('Nqkyv film', NULL),
	   ('Drama', NULL),
	   ('Komediq', NULL)

INSERT INTO Categories
VALUES ('Category', NULL),
	   ('Category2', NULL),
	   ('Category3', NULL),
	   ('Category4', NULL),
	   ('Category5', NULL)

INSERT INTO Movies
VALUES ('Title1', 1, '02-02-2000', '02:02:02', 2, 2, 3.1, NULL),
	   ('Title2', 1, '02-02-2000', '02:02:02', 2, 2, 3.1, NULL),
	   ('Title3', 1, '02-02-2000', '02:02:02', 2, 2, 3.1, NULL),
	   ('Title4', 1, '02-02-2000', '02:02:02', 2, 2, 3.1, NULL),
	   ('Title5', 1, '02-02-2000', '02:02:02', 2, 2, 3.1, NULL)