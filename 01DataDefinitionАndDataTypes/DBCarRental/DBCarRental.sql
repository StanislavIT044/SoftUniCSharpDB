CREATE DATABASE CarRental

USE CarRental

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(30) NOT NULL,
	DailyRate DECIMAL(3, 1) NOT NULL,
	WeeklyRate DECIMAL(3, 1) NOT NULL,
	MonthlyRate DECIMAL(3, 1) NOT NULL,
	WeekendRate DECIMAL(3, 1) NOT NULL
)

CREATE TABLE Cars(
	Id INT PRIMARY KEY IDENTITY,
	PlateNumber INT NOT NULL,
	Manufacturer NVARCHAR(30) NOT NULL,
	Model NVARCHAR(30) NOT NULL,
	CarYear DATE NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
	Doors INT,
	Picture NVARCHAR(MAX),
	Condition NVARCHAR,
	Available BIT
)

CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Title NVARCHAR(30),
	Notes NVARCHAR(MAX)
)

CREATE TABLE Customers(
	Id INT PRIMARY KEY IDENTITY,
	DriverLicenceNumber INT NOT NULL,
	FullName NVARCHAR(50) NOT NULL,
	[Address] NVARCHAR(50) NOT NULL,
	ZIPCode INT,
	Notes NVARCHAR(MAX)
)

CREATE TABLE RentalOrders(
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id),
	CarId INT FOREIGN KEY REFERENCES Cars(Id),
	TankLevel INT NOT NULL,
	KilometrageStart DECIMAL(15, 3) NOT NULL,
	KilometrageEnd DECIMAL(15, 3) NOT NULL,
	TotalKilometrage DECIMAL(15, 3) NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL,
	TotalDays INT NOT NULL,
	RateApplied DECIMAL(15, 3),
	TaxRate DECIMAL(15, 3),
	OrderStatus NVARCHAR(30),
	Notes NVARCHAR(MAX)
)

INSERT INTO Categories
VALUES ('CategoryName', 1.1, 1.1, 1.1, 1.1),
	   ('CategoryName1', 1.1, 1.1, 1.1, 1.1),
	   ('CategoryName2', 1.1, 1.1, 1.1, 1.1)

INSERT INTO Cars
VALUES (1, 'Audi', 'A3', '02-02-2000', 1, 3, NULL, NULL, NULL),
	   (2, 'Audi', 'A3', '02-02-2000', 1, 3, NULL, NULL, NULL),
	   (3, 'Audi', 'A3', '02-02-2000', 1, 3, NULL, NULL, NULL)
	  
INSERT INTO Employees
VALUES ('Gosho', 'Goshov', NULL, NULL),
	   ('Gosho', 'Goshov', NULL, NULL),
	   ('Gosho', 'Goshov', NULL, NULL)	   

INSERT INTO Customers
VALUES (123, 'Gosho Goshov', 'asdasdasda', NULL, NULL),
	   (123, 'Gosho Goshov', 'asdasdasda', NULL, NULL),
	   (123, 'Gosho Goshov', 'asdasdasda', NULL, NULL)

INSERT INTO RentalOrders
VALUES (1, 1, 1, 123, 2, 4, 20, '02-02-2020', '03-02-2020', 1, NULL, NULL, NULL, NULL),
	   (1, 1, 1, 123, 2, 4, 20, '02-02-2020', '03-02-2020', 1, NULL, NULL, NULL, NULL),
	   (1, 1, 1, 123, 2, 4, 20, '02-02-2020', '03-02-2020', 1, NULL, NULL, NULL, NULL)