CREATE DATABASE School

USE School 

--Problem01
CREATE TABLE Students(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	FirstName NVARCHAR(20) NOT NULL,
	MiddleName NVARCHAR(20),
	LastName NVARCHAR(20) NOT NULL,
	Age INT NOT NULL CHECK (Age > 0),
	[Address] NVARCHAR(30),
	Phone NVARCHAR(10)
)

CREATE TABLE Subjects(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(20) NOT NULL,
	Lessons INT NOT NULL CHECK (Lessons > 0)
)

CREATE TABLE StudentsSubjects(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	StudentId INT FOREIGN KEY REFERENCES Students(Id) NOT NULL,
	SubjectId INT FOREIGN KEY REFERENCES Subjects(Id) NOT NULL,
	Grade DECIMAL(15, 2) NOT NULL CHECK (Grade BETWEEN 2 AND 6)
)

CREATE TABLE Exams(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	[Date] DATETIME,
	SubjectId INT FOREIGN KEY REFERENCES Subjects(Id) NOT NULL
)

CREATE TABLE StudentsExams(
	StudentId INT FOREIGN KEY REFERENCES Students(Id) NOT NULL,
	ExamId INT FOREIGN KEY REFERENCES Exams(Id) NOT NULL,
	Grade DECIMAL(15, 2) CHECK (Grade BETWEEN 2 AND 6) NOT NULL

	CONSTRAINT PK_StudentIdExamId PRIMARY KEY (StudentId, ExamId)
)

CREATE TABLE Teachers(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	FirstName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	[Address] NVARCHAR(20) NOT NULL,
	Phone NVARCHAR(10),
	SubjectId INT FOREIGN KEY REFERENCES Subjects(Id) NOT NULL
)

CREATE TABLE StudentsTeachers(
	StudentId INT FOREIGN KEY REFERENCES Students(Id) NOT NULL,
	TeacherId INT FOREIGN KEY REFERENCES Teachers(Id) NOT NULL

	CONSTRAINT PK_StudentIdTeacherId PRIMARY KEY (StudentId, TeacherId)
)

--Problem02
INSERT INTO Teachers (FirstName, LastName, Address, Phone, SubjectId) VALUES
('Ruthanne', 'Bamb', '84948 Mesta Junction', 3105500146, 6),
('Gerrard', 'Lowin', '370 Talisman Plaza', 3324874824, 2),
('Merrile', 'Lambdin', '81 Dahle Plaza', 4373065154, 5),
('Bert', 'Ivie', '2 Gateway Circle', 4409584510, 4)

INSERT INTO Subjects ([Name], Lessons) VALUES
('Geometry', 12),
('Health', 10),
('Drama', 7),
('Sports', 9)

--Problem03
UPDATE StudentsSubjects
SET Grade = 6.00
WHERE SubjectId IN(1, 2) AND Grade >= 5.5

--Problem04
DELETE FROM StudentsTeachers
WHERE TeacherId IN (SELECT * FROM Teachers WHERE Phone LIKE '%72%')

--Problem05
SELECT FirstName, LastName, Age
  FROM Students
 WHERE Age >= 12
 ORDER BY FirstName, LastName

--Problem06
SELECT s.FirstName, s.LastName, COUNT(TeacherId) AS TeacherCount
  FROM Students AS s
  JOIN StudentsTeachers AS st ON st.StudentId = s.Id
 GROUP BY FirstName, LastName

--Problem07
SELECT FirstName + ' ' + LastName AS [Full Name]
  FROM Students AS s
  FULL JOIN StudentsExams AS se ON s.Id = se.StudentId
 WHERE se.Grade IS NULL
 ORDER BY [Full Name]