CREATE DATABASE Service

--01. DDL

CREATE TABLE Users(
	Id INT PRIMARY KEY IDENTITY,
	Username VARCHAR(30) UNIQUE NOT NULL,
	[Password] VARCHAR(50) NOT NULL,
	[Name] VARCHAR(50),
	Birthdate DATETIME2,
	Age INT CHECK(Age BETWEEN 14 AND 110),
	Email VARCHAR(50) NOT NULL
)

CREATE TABLE Departments(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL
)


CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(25),
	LastName VARCHAR(25),
	Birthdate DATETIME2,
	Age INT CHECK(Age BETWEEN 18 AND 110),
	DepartmentId INT  FOREIGN KEY REFERENCES Departments(Id) NOT NULL
)

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	DepartmentId INT NOT NULL FOREIGN KEY REFERENCES Departments(Id)
)

CREATE TABLE Status(
	Id INT PRIMARY KEY IDENTITY,
	Label VARCHAR(30) NOT NULL
)

CREATE TABLE Reports(
	Id INT PRIMARY KEY IDENTITY,
	CategoryId INT FOREIGN KEY REFERENCES Categories NOT NULL,
	StatusId INT FOREIGN KEY REFERENCES Status(Id) NOT NULL,
	OpenDate DATETIME2 NOT NULL,
	CloseDate DATETIME2,
	[Description] VARCHAR(200) NOT NULL,
	UserId INT FOREIGN KEY REFERENCES Users(Id) NOT NULL,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id)
)


--02. Insert


INSERT INTO Employees(FirstName,LastName,Birthdate,DepartmentId)VALUES
('Marlo',	'O''Malley','1958-9-21',1),
('Niki',	'Stanaghan','1969-11-26',	4),
('Ayrton',	'Senna',	'1960-03-21',	9),
('Ronnie',	'Peterson',	'1944-02-14',	9),
('Giovanna','Amati',	'1959-07-20',	5)

INSERT INTO Reports(CategoryId,StatusId,OpenDate,CloseDate,Description,UserId,EmployeeId)VALUES
(1,	1,	'2017-04-13',NULL,'Stuck Road on Str.133',	6,	2),
(6,	3,	'2015-09-05',	'2015-12-06',	'Charity trail running',	3,	5),
(14,2,	'2015-09-07',	NULL,'Falling bricks on Str.58',	5,	2),
(4,	3,	'2017-07-03',	'2017-07-06',	'Cut off streetlight on Str.11',	1,	1)

--03. Update

UPDATE Reports
SET CloseDate = GETDATE()
WHERE CloseDate IS NULL

--03. Update

DELETE FROM Reports
WHERE StatusId = 4


--05. Unassigned Reports


SELECT r.Description,FORMAT(r.OpenDate,'dd-MM-yyyy') AS OpenDate
FROM Reports AS r
WHERE R.EmployeeId IS NULL
ORDER BY r.OpenDate ,r.Description


--06. Reports & Categories

SELECT r.Description,c.Name AS CategoryName 
FROM Reports AS r
JOIN Categories AS c ON c.Id = r.CategoryId
ORDER BY R.Description,c.Name


--07. Most Reported Category

--V-1
SELECT TOP(5) c.Name AS CategoryName,COUNT(r.CategoryId) AS ReportsNumber
FROM Reports AS r
JOIN Categories AS c ON c.Id = r.CategoryId
GROUP BY c.Name
ORDER BY ReportsNumber DESC
--V2
SELECT TOP(5) h.Name,MAX(h.RANK) AS ReportsNumber
FROM(
SELECT c.Name,ROW_NUMBER() OVER(PARTITION BY c.Name  ORDER BY c.Name  ) AS RANK
FROM Reports AS r
JOIN Categories AS c ON c.Id = r.CategoryId) AS h
GROUP BY h.Name
ORDER BY ReportsNumber DESC



--08. Birthday Report

SELECT u.Username,c.Name 
FROM Users AS u
JOIN Reports AS r ON r.UserId = u.Id
JOIN Categories AS c ON c.Id = r.CategoryId
WHERE MONTH(r.OpenDate) = MONTH(u.Birthdate) AND DAY(r.OpenDate) = DAY(u.Birthdate)
ORDER BY u.Username,c.Name

--09. User per Employee

SELECT e.FirstName + ' ' + e.LastName AS FullName,COUNT(r.Id) AS UsersCount
FROM Employees AS e
LEFT JOIN Reports AS r ON r.EmployeeId = e.Id 
LEFT JOIN Users AS u ON u.Id = r.UserId
GROUP BY e.FirstName,e.LastName
ORDER BY UsersCount DESC, e.FirstName,e.LastName

--10. Full Info

SELECT ISNULL(CONCAT(e.FirstName,' ',e.LastName),'None') AS Employee,
ISNULL(d.Name,'None') AS Department,
ISNULL(c.Name,'None') AS Category,
ISNULL(r.Description,'None') AS Description,
ISNULL(FORMAT(r.OpenDate,'dd.MM.yyyy'),'None') AS OpenDate,
ISNULL(s.Label,'None') AS Status,
ISNULL(u.Name,'None') AS [User]
FROM Reports AS r
Left JOIN Employees AS e ON e.Id = r.EmployeeId
Left JOIN Categories AS c ON c.Id = r.CategoryId
Left JOIN Departments AS d ON d.Id = e.DepartmentId
Left JOIN Status AS s ON s.Id = r.StatusId
Left JOIN Users AS u ON u.Id = r.UserId
ORDER BY e.FirstName DESC,e.LastName DESC,d.Name,c.Name,r.Description,r.OpenDate,s.Label,u.Name 

--11. Hours to Complete

GO
CREATE FUNCTION udf_HoursToComplete(@StartDate DATETIME, @EndDate DATETIME)
RETURNS INT
AS
BEGIN
	
	IF(@StartDate IS NULL OR @EndDate IS NULL)
	BEGIN
		Return(0)
	END
	DECLARE @Result INT = (Select DATEDIFF(HOUR,r.OpenDate,r.CloseDate) FROM Reports AS r WHERE r.OpenDate = @StartDate AND r.CloseDate = @EndDate)
	RETURN @Result
END


GO

--12. Assign Employee

CREATE PROCEDURE usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT) 
AS
BEGIN
	DECLARE @employee INT= (SELECT e.DepartmentId FROM Employees AS e 
							WHERE	e.Id = @EmployeeId)

	DECLARE @report INT = (SELECT c.DepartmentId FROM Reports AS r
						   JOIN Categories AS c ON c.Id = r.CategoryId
				           WHERE r.Id = @ReportId)

	IF(@employee <> @report)
	BEGIN
		RAISERROR('Employee doesn''t belong to the appropriate department!',16,1)
	END

	UPDATE Reports
	SET EmployeeId = @EmployeeId
	WHERE Id = @ReportId	
END

EXEC usp_AssignEmployeeToReport 30, 1
EXEC usp_AssignEmployeeToReport 17, 2