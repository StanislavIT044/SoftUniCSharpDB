USE Relations

CREATE TABLE Manufacturers(
	ManufacturerID int PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	[Name] varchar(50) NOT NULL,
	EstablishedOn datetime2 NOT NULL
);

CREATE TABLE Models(
	ModelID int PRIMARY KEY IDENTITY(101, 1) NOT NULL,
	[Name] varchar(50) NOT NULL,
	ManufacturerID int FOREIGN KEY REFERENCES  Manufacturers(ManufacturerID)
);

INSERT INTO Manufacturers VALUES
('BMW', '07-03-1916'),
('Tesla', '01-01-2003'),
('Lada', '01-05-1966')

INSERT INTO Models VALUES
('X1', 1),
('i6', 1),
('Model S', 2),
('Model X', 2),
('Model 3', 2),
('Nova', 3)