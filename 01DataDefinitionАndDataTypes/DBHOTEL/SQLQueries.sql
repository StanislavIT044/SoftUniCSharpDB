CREATE DATABASE Hotel

USE Hotel

CREATE TABLE Employees (
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Title NVARCHAR(1000),
	Notes NVARCHAR(1000)
)

CREATE TABLE Customers (
	AccountNumber INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	PhoneNumber NVARCHAR(30) NOT NULL,
	EmergencyName NVARCHAR(30) NOT NULL,
	EmergencyNumber NVARCHAR(30) NOT NULL,
	Notes NVARCHAR(1000)
)

CREATE TABLE RoomStatus (
	RoomStatus NVARCHAR(30) NOT NULL,
	Notes NVARCHAR(1000)
)

CREATE TABLE RoomTypes (
	RoomType NVARCHAR(30) NOT NULL,
	Notes NVARCHAR(1000)
)

CREATE TABLE BedTypes (
	BedType NVARCHAR(30) NOT NULL,
	Notes NVARCHAR(1000)
)

CREATE TABLE Rooms (
	RoomNumber INT PRIMARY KEY IDENTITY,
	RoomType NVARCHAR(30),
	BedType NVARCHAR(30),
	Rate DECIMAL(15, 3) NOT NULL,
	RoomStatus NVARCHAR(30),
	Notes NVARCHAR(1000)
)

CREATE TABLE Payments (
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	PaymentDate DATE NOT NULL,
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber),
	FirstDateOccupied DATE NOT NULL,
	LastDateOccupied DATE NOT NULL,
	TotalDays INT,
	AmountCharged DECIMAL(15, 3) NOT NULL,
	TaxRate DECIMAL(15, 3) NOT NULL,
	TaxAmount DECIMAL(15, 3) NOT NULL,
	PaymentTotal DECIMAL(15, 3) NOT NULL,
	Notes NVARCHAR(1000)
)

CREATE TABLE Occupancies (
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	DateOccupied DATE,
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber),
	RoomNumber INT FOREIGN KEY REFERENCES Rooms(RoomNumber),
	RateApplied DECIMAL(15, 3),
	PhoneCharge DECIMAL(15, 3),
	Notes NVARCHAR(1000)
)

INSERT INTO Employees(FirstName, LastName, Title, Notes)
VALUES ('Gosho', 'Goshov', 'asd', 'asd'),
	   ('Gosho2', 'Goshov2', 'asd2', 'asd2'),
	   ('Gosho3', 'Goshov3', 'asd3', 'asd3')

INSERT INTO Customers 
	(FirstName,
	LastName, PhoneNumber, 
	EmergencyName, EmergencyNumber, Notes)
VALUES ('Gosho', 'Goshov', '1234', 'Pesho1', '1234', NULL),
	   ('Gosho2', 'Goshov2', '12345', 'Pesho2', '12345', NULL),
	   ('Gosho3', 'Goshov3', '123456', 'Pesho3', '123456', NULL)

INSERT INTO RoomStatus(RoomStatus, Notes)
VALUES ('rented out', 'note'),
	   ('rented out', 'note'),
	   ('rented out', 'note')

INSERT INTO RoomTypes (RoomType, Notes)
VALUES ('Mezonet', 'note'),
	   ('Mezonet', 'note'),
	   ('Mezonet', 'note')

INSERT INTO BedTypes (BedType, Notes)
VALUES ('Meko', 'note'),
	   ('Meko', 'note'),
	   ('Meko', 'note')

INSERT INTO Rooms (RoomType, BedType, Rate, RoomStatus, Notes)
VALUES ('Mezonet', 'Meko', 3.1, 'rented out', NULL),
	   ('Mezonet', 'Meko', 3.1, 'rented out', NULL),
	   ('Mezonet', 'Meko', 3.1, 'rented out', NULL)

INSERT INTO Payments 
		(EmployeeId, PaymentDate,
		AccountNumber, FirstDateOccupied, LastDateOccupied,
		TotalDays, AmountCharged, TaxRate, 
		TaxAmount, PaymentTotal, Notes)
VALUES (1, '02-02-2002', 3, '02-02-2002', '02-03-2002', 1, 2, 2, 2, 2, NULL),
	   (1, '02-02-2002', 3, '02-02-2002', '02-03-2002', 1, 2, 2, 2, 2, NULL),
	   (1, '02-02-2002', 3, '02-02-2002', '02-03-2002', 1, 2, 2, 2, 2, NULL)

INSERT INTO Occupancies 
		(EmployeeId, DateOccupied, AccountNumber,
		RoomNumber, RateApplied, PhoneCharge, Notes)
VALUES (1, '02-02-2002', 1, 1, 2, 123, NULL),
	   (1, '02-02-2002', 1, 1, 2, 123, NULL),
	   (1, '02-02-2002', 1, 1, 2, 123, NULL)