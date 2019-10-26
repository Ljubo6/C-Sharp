CREATE DATABASE School
USE School

--01. DDL

CREATE TABLE Students(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	MiddleName NVARCHAR(25),
	LastName NVARCHAR(30) NOT NULL,
	Age INT CHECK(Age BETWEEN 5 AND 100 AND Age > 0),
	[Address] NVARCHAR(50),
	Phone CHAR(10)
)

CREATE TABLE Subjects(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(20) NOT NULL,
	Lessons INT NOT NULL CHECK(Lessons > 0)
)

CREATE TABLE StudentsSubjects(
	Id INT PRIMARY KEY IDENTITY,
	StudentId INT NOT NULL FOREIGN KEY REFERENCES Students(Id),
	SubjectId INT NOT NULL FOREIGN KEY REFERENCES Subjects(Id),
	Grade DECIMAL(15,2) NOT NULL CHECK(Grade BETWEEN 2 AND 6)
)

CREATE TABLE Exams(
	Id INT PRIMARY KEY IDENTITY,
	[Date] DATETIME,
	SubjectId INT NOT NULL FOREIGN KEY REFERENCES Subjects(Id)
)

CREATE TABLE StudentsExams(
	StudentId INT NOT NULL FOREIGN KEY REFERENCES Students(Id),
	ExamId INT NOT NULL FOREIGN KEY REFERENCES Exams(Id),
	Grade DECIMAL(15,2) NOT NULL CHECK(Grade BETWEEN 2 AND 6),
	PRIMARY KEY (StudentId,ExamId)
)

CREATE TABLE Teachers(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	Address NVARCHAR(20) NOT NULL,
	Phone CHAR(10),
	SubjectId INT NOT NULL FOREIGN KEY REFERENCES Subjects(Id)
)

CREATE TABLE StudentsTeachers(
	StudentId INT NOT NULL FOREIGN KEY REFERENCES Students(Id),
	TeacherId INT NOT NULL FOREIGN KEY REFERENCES Teachers(Id)
	PRIMARY KEY (StudentId,TeacherId)
)

--02. Insert

INSERT INTO Teachers (FirstName,LastName,[Address],Phone,SubjectId)VALUES
('Ruthanne','Bamb',	  '84948 Mesta Junction','3105500146',6),
('Gerrard',	'Lowin',  '370 Talisman Plaza',  '3324874824',2),
('Merrile',	'Lambdin','81 Dahle Plaza',      '4373065154',5),
('Bert',	'Ivie',	  '2 Gateway Circle',    '4409584510',4)

INSERT INTO Subjects([Name],Lessons)VALUES
('Geometry',12),
('Health',	10),
('Drama',	 7),
('Sports',	 9)

--03. Update

UPDATE StudentsSubjects
SET Grade = 6.00
WHERE  SubjectId IN (1,2) AND Grade >= 5.50

--04. Delete

DELETE FROM StudentsTeachers
WHERE TeacherId IN (SELECT Id FROM Teachers WHERE Phone LIKE '%72%')

DELETE FROM Teachers
WHERE Phone LIKE '%72%'

--05. Teen Students

SELECT s.FirstName,s.LastName,s.Age 
  FROM Students AS s
WHERE s.Age >= 12
ORDER BY s.FirstName,s.LastName

--06. Cool Addresses

SELECT s.FirstName + ' ' + ISNULL(s.MiddleName ,'') + ' ' +  s.LastName AS [Full Name],s.Address AS [Address]
  FROM Students AS s
  WHERE s.Address LIKE '%road%'
ORDER BY s.FirstName,s.LastName,s.Address

--07. 42 Phones

SELECT s.FirstName,s.Address,s.Phone 
  FROM Students AS s
  WHERE s.MiddleName IS NOT NULL AND s.Phone LIKE '42%'
ORDER BY s.FirstName

--08. Students Teachers 

SELECT s.FirstName,s.LastName,COUNT(st.TeacherId) 
FROM Students AS s
JOIN StudentsTeachers AS st ON st.StudentId = s.Id
GROUP BY s.FirstName,s.LastName

--09. Subjects with Students

SELECT t.FirstName + ' ' + t.LastName AS FullName,s.Name + '-' + CONVERT(NVARCHAR(30),s.Lessons) AS Subjects,COUNT(st.StudentId) AS Students
	FROM Teachers AS t
	JOIN Subjects AS s ON s.Id = t.SubjectId
	JOIN StudentsTeachers AS st ON st.TeacherId = t.Id
	GROUP BY t.FirstName,t.LastName,s.Name,s.Lessons
	ORDER BY Students DESC,FullName,Subjects

--10. Students to Go

SELECT st.FirstName + ' ' + st.LastName AS FullName 
  FROM Students AS st
  WHERE st.Id NOT IN (SELECT se.StudentId FROM StudentsExams AS se)
  ORDER BY FullName

--11. Busiest Teachers

SELECT TOP(10) t.FirstName , t.LastName,COUNT(st.StudentId) AS StudentsCount
	FROM Teachers AS t
	JOIN Subjects AS s ON s.Id = t.SubjectId
	JOIN StudentsTeachers AS st ON st.TeacherId = t.Id
	GROUP BY t.FirstName,t.LastName
	ORDER BY StudentsCount DESC,t.FirstName,t.LastName

--12. Top Students

SELECT TOP(10) st.FirstName,st.LastName,CONVERT(DECIMAL(15,2),AVG(se.Grade)) AS Grade
	FROM Students AS st
	JOIN StudentsExams AS se ON se.StudentId = st.Id
	GROUP BY  st.FirstName,st.LastName
	ORDER BY Grade DESC,st.FirstName,st.LastName

--13. Second Highest Grade

SELECT h.FirstName,h.LastName,h.Grade
FROM(
SELECT s.FirstName,s.LastName,ss.Grade,ROW_NUMBER() OVER (PARTITION BY s.FirstName ORDER BY ss.Grade DESC) AS RANK
	FROM Students AS s
	JOIN StudentsSubjects AS ss ON ss.StudentId = s.Id
	) AS h
	WHERE h.RANK = 2
	ORDER BY h.FirstName,h.LastName

--14. Not So In The Studying

SELECT s.FirstName + ' ' + ISNULL(s.MiddleName + ' ','') +  s.LastName AS [Full Name]
  FROM Students AS s
  
  LEFT JOIN StudentsSubjects AS ss ON ss.StudentId = s.Id
  LEFT JOIN Subjects AS sub ON sub.Id = ss.SubjectId
  WHERE ss.SubjectId IS NULL
  ORDER BY [Full Name]

--15. Top Student per Teacher

SELECT j.[Teacher Full Name],j.[Subject Name],j.[Student Full Name],FORMAT(j.TopGrade, 'N2') AS Grade
FROM(
SELECT h.[Teacher Full Name],h.[Subject Name],h.[Student Full Name],h.AverageGrade AS TopGrade,ROW_NUMBER() OVER(PARTITION BY h.[Teacher Full Name] ORDER BY h.AverageGrade DESC) AS RowNumber
FROM(
SELECT t.FirstName + ' ' + t.LastName AS [Teacher Full Name],subj.Name AS [Subject Name],s.FirstName + ' ' +  s.LastName AS [Student Full Name],AVG(ss.Grade) AS AverageGrade
FROM Teachers AS t
JOIN StudentsTeachers AS st ON st.TeacherId = t.Id
JOIN Students AS s ON s.Id = st.StudentId
JOIN StudentsSubjects AS ss ON ss.StudentId = s.Id
JOIN Subjects AS subj ON subj.Id = ss.SubjectId AND subj.Id = t.SubjectId
GROUP BY t.FirstName,t.LastName,subj.Name,s.FirstName,s.LastName
) AS h
)AS j
WHERE j.RowNumber = 1
ORDER BY j.[Subject Name],j.[Teacher Full Name],TopGrade DESC

--16. Average Grade per Subject

SELECT s.Name,AVG(ss.Grade) AS AverageGrade
FROM  Subjects AS s
JOIN StudentsSubjects AS ss ON ss.SubjectId = s.Id
GROUP BY s.Name,s.Id
ORDER BY s.Id

--17. Exams Information

SELECT k.Quarter,k.SubjectName,COUNT(k.StudentId) AS StudentsCount
FROM
(
SELECT 
CASE 
	WHEN DATENAME(qq,e.Date) = 1 THEN 'Q1'
	WHEN DATENAME(qq,e.Date) = 2 THEN 'Q2'
	WHEN DATENAME(qq,e.Date) = 3 THEN 'Q3'
	WHEN DATENAME(qq,e.Date) = 4 THEN 'Q4'
	WHEN E.Date IS NULL THEN 'TBA'
	END AS [Quarter],s.Name AS SubjectName,se.StudentId
FROM Exams AS e
JOIN Subjects AS s ON s.Id = e.SubjectId
JOIN StudentsExams AS se ON se.ExamId = e.Id
WHERE se.Grade >= 4) AS k
GROUP BY k.Quarter,k.SubjectName
ORDER BY k.Quarter

GO
--18. Exam Grades

CREATE FUNCTION udf_ExamGradesToUpdate(@studentId INT, @grade DECIMAL(15,2))
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @studentExist INT = (SELECT TOP(1) se.StudentId FROM StudentsExams AS se WHERE se.StudentId = @studentId)
	IF(@studentExist IS NULL)
	BEGIN
		RETURN('The student with provided id does not exist in the school!')
	END
	IF(@grade > 6.00)
	BEGIN
		RETURN('Grade cannot be above 6.00!')
	END
	DECLARE @studentFirstName NVARCHAR(20) = (SELECT TOP(1) s.FirstName FROM Students AS s WHERE s.Id = @studentId)
	DECLARE @upLevelGrade DECIMAL(15,2) = @grade + 0.50
	DECLARE @count INT = (SELECT Count(se.Grade) FROM StudentsExams AS se
							WHERE StudentId = @studentId AND se.Grade >= @grade AND se.Grade <= @upLevelGrade)
	RETURN ('You have to update ' + CAST(@count AS NVARCHAR(20))+ ' grades for the student ' + @studentFirstName)
END

GO
SELECT dbo.udf_ExamGradesToUpdate(12, 6.20)
SELECT dbo.udf_ExamGradesToUpdate(12, 5.50)
SELECT dbo.udf_ExamGradesToUpdate(121, 5.50)
GO
--19. Exclude From School

CREATE PROCEDURE usp_ExcludeFromSchool(@StudentId INT)
AS
BEGIN
	DECLARE @studentExist INT = (SELECT s.Id FROM Students AS s WHERE s.Id = @StudentId)
	IF @studentExist IS NULL
	BEGIN
		RAISERROR ('This school has no student with the provided id!',16,1)
	END

	DELETE FROM StudentsExams
	WHERE StudentId = @StudentId

	DELETE FROM StudentsSubjects
	WHERE StudentId = @StudentId

	DELETE FROM StudentsTeachers
	WHERE StudentId = @StudentId

	DELETE FROM Students
	WHERE Students.Id = @StudentId
END

EXEC usp_ExcludeFromSchool 1
SELECT COUNT(*) FROM Students

EXEC usp_ExcludeFromSchool 301

GO

--20. Deleted Students

CREATE TABLE ExcludedStudents(
	StudentId INT,
	StudentName NVARCHAR(30)
)
GO
CREATE TRIGGER tr_ExcludeStudent
ON Students
AFTER DELETE
AS
	INSERT INTO ExcludedStudents(StudentId,StudentName)
	SELECT Id,FirstName + ' ' + LastName FROM deleted

GO

DELETE FROM StudentsExams
WHERE StudentId = 1

DELETE FROM StudentsTeachers
WHERE StudentId = 1

DELETE FROM StudentsSubjects
WHERE StudentId = 1

DELETE FROM Students
WHERE Id = 1

SELECT * FROM ExcludedStudents
