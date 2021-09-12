USE Relations

CREATE TABLE Teachers(
	TeacherID int PRIMARY KEY IDENTITY(101, 1),
	[Name] varchar(50) NOT NULL,
	ManagerID int FOREIGN KEY REFERENCES Teachers(TeacherID)
);

INSERT INTO Teachers([Name]) VALUES
('John'),
('Maya'),
('Silvia'),
('Ted'),
('Mark'),
('Greta')

UPDATE Teachers
SET ManagerID = 106
WHERE TeacherID = 102

UPDATE Teachers
SET ManagerID = 106
WHERE TeacherID = 103

UPDATE Teachers
SET ManagerID = 105
WHERE TeacherID = 104

UPDATE Teachers
SET ManagerID = 101
WHERE TeacherID = 105

UPDATE Teachers
SET ManagerID = 101
WHERE TeacherID = 106