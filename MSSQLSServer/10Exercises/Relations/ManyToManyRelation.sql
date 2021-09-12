CREATE TABLE Students(
	StudentID int PRIMARY KEY IDENTITY(1, 1),
	[Name] varchar(50)
);

CREATE TABLE Exams(
	ExamID int PRIMARY KEY IDENTITY(101, 1),
	[Name] varchar(50)
);

CREATE TABLE StudentsExams(
	StudentID int FOREIGN KEY REFERENCES Students(StudentID),
	ExamID int FOREIGN KEY REFERENCES Exams(ExamID),
	CONSTRAINT PK_Student_Exam PRIMARY KEY(StudentID, ExamID)
);

INSERT INTO Students VALUES
('Mila'),
('Toni'),
('Ron')

INSERT INTO Exams VALUES
('SpringMVC'),
('Neo4j'),
('Oracle 11gRon')

INSERT INTO StudentsExams VALUES
(1 ,101),
(1 ,102),
(2 ,101),
(3 ,103),
(2 ,102),
(2 ,103)