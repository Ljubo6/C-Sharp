CREATE DATABASE ReportService

--01. DDL

CREATE TABLE Users(
	Id INT PRIMARY KEY IDENTITY,
	Username NVARCHAR(30) NOT NULL UNIQUE,
	[Password] NVARCHAR(50) NOT NULL,
	[Name] NVARCHAR(50),
	Gender CHAR CHECK(Gender IN ('M','F')),
	BirthDate DATETIME,
	Age INT,
	Email NVARCHAR(50) NOT NULL
)

CREATE TABLE Departments(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(25),
	LastName  NVARCHAR(25),
	Gender CHAR CHECK(Gender IN ('M','F')),
	BirthDate DATETIME,
	Age INT,
	DepartmentId INT NOT NULL FOREIGN KEY REFERENCES Departments(Id)
)

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id)
)

CREATE TABLE [Status](
	Id INT PRIMARY KEY IDENTITY,
	[Label] VARCHAR(30) NOT NULL
)


CREATE TABLE Reports(
	Id INT PRIMARY KEY IDENTITY,
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(Id),
	StatusId INT NOT NULL FOREIGN KEY REFERENCES Status(Id),
	OpenDate DATETIME NOT NULL,
	CloseDate DATETIME,
	[Description] VARCHAR(200),
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id)
)

--02. Insert

INSERT INTO Employees (FirstName,LastName,Gender,Birthdate,DepartmentId) VALUES
('Marlo',	'O’Malley',	'M','9/21/1958',1),
('Niki',	'Stanaghan','F','11/26/1969',4),
('Ayrton',	'Senna',	'M','03/21/1960',9),
('Ronnie',	'Peterson',	'M','02/14/1944',9),
('Giovanna','Amati',	'F','07/20/1959',5)


INSERT INTO Reports(CategoryId,StatusId,OpenDate,CloseDate,Description,UserId,EmployeeId) VALUES
(1,	1,	'04/13/2017',	NULL,	'Stuck Road on Str.133', 6 , 2),
(6,	3,	'09/05/2015',	'12/06/2015',	'Charity trail running', 3 , 5),
(14,2,	'09/07/2015',	NULL,	'Falling bricks on Str.58', 5 , 2),
(4,	3,	'07/03/2017',	'07/06/2017',	'Cut off streetlight on Str.11', 1 , 1)

--03. Update
UPDATE Reports
SET StatusId = 2
WHERE StatusId = 1 AND CategoryId = 4

--04. Delete

DELETE FROM Reports
WHERE StatusId = 4

--05. Users by Age

SELECT u.Username,u.Age 
	FROM Users AS u
ORDER BY u.Age , u.Username DESC

--06. Unassigned Reports

SELECT r.Description,r.OpenDate 
	FROM   Reports AS r
	WHERE r.EmployeeId IS NULL
ORDER BY r.OpenDate,r.Description

--07. Employees & Reports

SELECT e.FirstName,e.LastName,r.Description,FORMAT(r.OpenDate,'yyyy-MM-dd') AS OpenDate
	FROM Employees AS e
	JOIN Reports AS r ON r.EmployeeId = e.Id
ORDER BY e.Id,r.OpenDate,r.Id

--08. Most Reported Category

SELECT c.Name AS CategoryName,COUNT(r.Id) AS ReportNumber
	FROM Categories AS c
	JOIN Reports AS r ON r.CategoryId = c.Id
GROUP BY c.Name
ORDER BY ReportNumber DESC,CategoryName

--09. Employees in Category

SELECT c.Name AS CategoryName, COUNT(e.Id) AS [Employees Number]
	FROM Categories AS c
	JOIN Departments AS d ON d.Id = c.DepartmentId
	JOIN Employees AS e ON e.DepartmentId = d.Id
GROUP BY c.Name
ORDER BY c.Name

--10. Users per Employee

 SELECT CONCAT(e.FirstName ,' ',e.LastName) AS Name , COUNT(r.UserId) AS [Users Number]
	FROM Employees AS e
	LEFT JOIN Reports AS r ON r.EmployeeId = e.Id
GROUP BY e.FirstName,e.LastName
ORDER BY [Users Number] DESC,Name

--11. Emergency Patrol


SELECT r.OpenDate,r.Description,u.Email AS [Reporter Email]
	FROM Reports AS r
	JOIN Users AS u ON u.Id = r.UserId
	JOIN Categories AS c ON c.Id = r.CategoryId
	JOIN Departments AS d ON d.Id = c.DepartmentId
	WHERE r.CloseDate IS NULL AND LEN(r.Description) > 20
	AND r.Description LIKE '%str%'
	AND d.Name IN ('Infrastructure','Emergency','Roads Maintenance')
ORDER BY r.OpenDate,u.Email,r.Id

--12. Birthday Report

SELECT c.Name AS CategoryName
	FROM Categories AS c
	JOIN Reports AS r ON r.CategoryId = c.Id
	JOIN Users AS u ON u.Id = r.UserId
	WHERE MONTH(r.OpenDate) = MONTH(u.BirthDate) AND DAY(r.OpenDate) = DAY(u.BirthDate)
	GROUP BY c.Name
	ORDER BY C.Name

--13. Numbers Coincidence

SELECT u.Username
	FROM Categories AS c
	JOIN Reports AS r ON r.CategoryId = c.Id
	JOIN Users AS u ON u.Id = r.UserId
	WHERE (LEFT(u.Username,1) LIKE '[0-9]' AND c.Id = TRY_CAST(LEFT(u.Username,1) AS INT))
	OR (RIGHT(u.Username,1) LIKE '[0-9]' AND c.Id = TRY_CAST(RIGHT(u.Username,1) AS INT))
	GROUP BY U.Username
	ORDER BY u.Username

--14. Open/Closed Statistics

WITH CTE_OpenedReports(EmployeeId,Count)
AS
(
	SELECT e.Id, COUNT(r.Id)
		FROM Employees AS e
		JOIN Reports AS r ON r.EmployeeId = e.Id
		WHERE DATEPART(YEAR,r.OpenDate) = 2016
		GROUP BY e.Id
),

CTE_ClosedReports(EmployeeId,Count) 
AS
(
	SELECT e.Id,COUNT(r.Id)
		FROM Employees AS e
		JOIN Reports AS r ON r.EmployeeId = e.Id
		WHERE DATEPART(YEAR,r.CloseDate) = 2016
		GROUP BY e.Id
)

SELECT CONCAT(e.FirstName,' ',e.LastName) AS Name,CONCAT(ISNULL(c.Count,0),'/',ISNULL(o.Count,0)) AS [Closed Open Reports]
	FROM CTE_ClosedReports AS c
	FULL JOIN CTE_OpenedReports AS o ON o.EmployeeId = c.EmployeeId
	JOIN Employees AS e ON e.Id = c.EmployeeId OR e.Id = o.EmployeeId
ORDER BY Name,e.Id

--15. Average Closing Time

SELECT d.Name AS [Department Name] ,ISNULL(CONVERT (VARCHAR(10),AVG(DATEDIFF(DAY,r.OpenDate,r.CloseDate))),'no info') AS [Average Duration]
	FROM Reports AS r
	JOIN Categories AS c ON c.Id = r.CategoryId
	JOIN Departments AS d ON d.Id = c.DepartmentId
GROUP BY d.Name
ORDER BY d.Name

--16. Favorite Categories

SELECT d.Name AS [Department Name],c.Name AS [Category Name],CONVERT(INT,ROUND(CountPerCategory.Count * 100.0 / CountPerDepartment.Count,0)) AS Percentage
	FROM Departments AS d
	JOIN Categories AS c ON c.DepartmentId = d.Id
	JOIN 
	(SELECT CategoryId,COUNT(1) AS Count 
		FROM Reports
		GROUP BY CategoryId) AS CountPerCategory ON CountPerCategory.CategoryId = c.Id
	JOIN
	(SELECT c.DepartmentId,COUNT(1) AS Count 
		FROM Reports AS r
		JOIN Categories AS c ON c.Id = r.CategoryId
		GROUP BY c.DepartmentId) AS CountPerDepartment ON CountPerDepartment.DepartmentId = c.DepartmentId
	ORDER BY d.Name,c.Name,Percentage

GO
--17. Employee's Load


CREATE FUNCTION udf_GetReportsCount(@employeeId INT, @statusId INT) 
 RETURNS INT 
 AS
 BEGIN
	RETURN(
		SELECT COUNT(r.Id)
			FROM Reports AS r
			WHERE r.EmployeeId = @employeeId AND r.StatusId = @statusId
	)
END

GO

SELECT Id, FirstName, Lastname, dbo.udf_GetReportsCount(1, 2) AS ReportsCount
FROM Employees
ORDER BY Id
GO
--18. Assign Employee

CREATE PROCEDURE usp_AssignEmployeeToReport(@employeeId INT, @reportId INT) AS
BEGIN
	DECLARE @employeeDepartment INT = 
	(
		SELECT e.DepartmentId 
			FROM Employees AS e
			WHERE e.Id = @employeeId
	)
	DECLARE @reportDepartment INT =
	(
		SELECT c.DepartmentId
		FROM Reports AS r
		JOIN Categories AS c ON c.Id = r.CategoryId
		WHERE r.Id = @reportId
	)

	IF(@employeeDepartment != @reportDepartment)
	BEGIN
		RAISERROR('Employee doesn''t belong to the appropriate department!',16,1)
		RETURN
	END

	UPDATE Reports
	SET EmployeeId = @employeeId
	WHERE Id = @reportId
END

GO

EXEC usp_AssignEmployeeToReport 17, 2;
SELECT EmployeeId FROM Reports WHERE id = 2

GO
--19. Close Reports

CREATE TRIGGER tr_Closereport ON Reports AFTER UPDATE AS
BEGIN
	UPDATE Reports
	SET StatusId = 3
	FROM deleted AS d
	JOIN inserted AS i ON i.Id = d.Id
	WHERE i.CloseDate IS NOT NULL
END

UPDATE Reports
SET CloseDate = GETDATE()
WHERE EmployeeId = 5;

--20 Categories Revisiion

SELECT c.Name,
	  COUNT(r.Id) AS ReportsNumber,
	  CASE 
	      WHEN InProgressCount > WaitingCount THEN 'in progress'
		  WHEN InProgressCount < WaitingCount THEN 'waiting'
		  ELSE 'equal'
	  END AS MainStatus
FROM Reports AS r
    JOIN Categories AS c ON c.Id = r.CategoryId
    JOIN Status AS s ON s.Id = r.StatusId
    JOIN (SELECT r.CategoryId, 
		         SUM(CASE WHEN s.Label = 'in progress' THEN 1 ELSE 0 END) as InProgressCount,
		         SUM(CASE WHEN s.Label = 'waiting' THEN 1 ELSE 0 END) as WaitingCount
		  FROM Reports AS r
		  JOIN Status AS s on s.Id = r.StatusId
		  WHERE s.Label IN ('waiting','in progress')
		  GROUP BY r.CategoryId
		 ) AS sc ON sc.CategoryId = c.Id
WHERE s.Label IN ('waiting','in progress') 
GROUP BY C.Name,
	     CASE 
		     WHEN InProgressCount > WaitingCount THEN 'in progress'
		     WHEN InProgressCount < WaitingCount THEN 'waiting'
		     ELSE 'equal'
	     END
ORDER BY C.Name, 
		 ReportsNumber, 
		 MainStatus;
