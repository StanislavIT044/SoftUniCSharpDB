CREATE DATABASE [ExamPreparation01]

USE [ExamPreparation01]

--Problem01
CREATE TABLE Planes(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	[Name] VARCHAR(30) NOT NULL,
	Seats INT NOT NULL,
	[Range] INT NOT NULL
)

CREATE TABLE Flights(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	DepartureTime DATETIME2,
	ArrivalTime DATETIME2,
	Origin VARCHAR(50) NOT NULL,
	Destination VARCHAR(50) NOT NULL,
	PlaneId INT FOREIGN KEY REFERENCES Planes(Id)
)

CREATE TABLE Passengers(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	FirstName VARCHAR(30) NOT NULL,
	LastName VARCHAR(30) NOT NULL,
	Age INT NOT NULL,
	[Address] VARCHAR(30) NOT NULL,
	PassportId VARCHAR(11) NOT NULL, -- ???
)

CREATE TABLE LuggageTypes(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	[Type] VARCHAR(30) NOT NULL
)

CREATE TABLE Luggages(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	LuggageTypeId INT FOREIGN KEY REFERENCES LuggageTypes(Id) NOT NULL,
	PassengerId INT FOREIGN KEY REFERENCES Passengers(Id) NOT NULL
)

CREATE TABLE Tickets(
	Id INT PRIMARY KEY IDENTITY(1, 1),
	PassengerId INT FOREIGN KEY REFERENCES Passengers(Id),
	FlightId INT FOREIGN KEY REFERENCES Flights(Id),
	LuggageId INT FOREIGN KEY REFERENCES Luggages(Id),
	Price DECIMAL(15, 2) NOT NULL
)

--Problem02
INSERT INTO Planes VALUES
('Airbus 336', 112, 5132),
('Airbus 330', 432, 5325),
('Boeing 369', 231, 2355),
('Stelt 297', 254, 2143),
('Boeing 338', 165, 5111),
('Airbus 558', 387, 1342),
('Boeing 128', 345, 5541)

INSERT INTO [LuggageTypes] VALUES
('Crossbody Bag'),
('School Backpack'),
('Shoulder Bag')

--Problem03
UPDATE Tickets 
   SET Price *= 1.13
 WHERE FlightId = (SELECT TOP(1) Id FROM Flights WHERE Destination = 'Carlsbad')

--Problem04
DELETE FROM Tickets
 WHERE FlightId = (SELECT TOP(1) Id FROM Flights WHERE Destination = 'Ayn Halagim')

--Problem05
SELECT * 
  FROM Planes
 WHERE [Name] LIKE '%tr%'
 ORDER BY Id ASC, [Name] ASC, Seats ASC, [Range] ASC

--Problem06
SELECT FlightId, SUM(Price) AS Price
  FROM Tickets
 GROUP BY FlightId
 ORDER BY Price DESC

--Problem07
SELECT p.FirstName + ' ' + p.LastName AS [Full Name],
	   f.Origin,
	   f.Destination
  FROM Passengers AS p
  JOIN Tickets AS t ON t.PassengerId = p.Id
  JOIN Flights AS f ON t.FlightId = f.Id 
 ORDER BY [Full Name] ASC, f.Origin ASC, f.Destination ASC