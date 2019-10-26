CREATE DATABASE Bitbucket

USE Bitbucket

--01. DDL

CREATE TABLE Users(
	Id INT PRIMARY KEY IDENTITY,
	Username NVARCHAR(30) NOT NULL,
	[Password] NVARCHAR(30) NOT NULL,
	Email NVARCHAR(30) NOT NULL
)

CREATE TABLE Repositories(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE RepositoriesContributors(
	RepositoryId INT NOT NULL FOREIGN KEY REFERENCES Repositories(Id),
	ContributorId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	PRIMARY KEY(RepositoryId,ContributorId)
)

CREATE TABLE Issues(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(255) NOT NULL,
	IssueStatus CHAR(6) NOT NULL,
	RepositoryId INT NOT NULL FOREIGN KEY REFERENCES Repositories(Id),
	AssigneeId INT NOT NULL FOREIGN KEY REFERENCES Users(Id)
)

CREATE TABLE Commits(
	Id INT PRIMARY KEY IDENTITY,
	[Message] NVARCHAR(255) NOT NULL,
	IssueId INT  FOREIGN KEY REFERENCES Issues(Id),
	RepositoryId INT NOT NULL FOREIGN KEY REFERENCES Repositories(Id),
	ContributorId INT NOT NULL FOREIGN KEY REFERENCES Users(Id)
)

CREATE TABLE Files(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(100) NOT NULL,
	Size DECIMAL(15,2) NOT NULL,
	ParentId INT FOREIGN KEY REFERENCES Files(Id),
	CommitId INT NOT NULL FOREIGN KEY REFERENCES Commits(Id)
)

--02. Insert

INSERT INTO Files VALUES
('Trade.idk',	2598.0,1,1),
('menu.net',	9238.31,2,2),
('Administrate.soshy',	1246.93,3,3),
('Controller.php',	7353.15,4,4),
('Find.java',	9957.86,5,5),
('Controller.json',	14034.87,3,6),
('Operate.xix',	7662.92,7,7)

INSERT INTO Issues(Title,IssueStatus,RepositoryId,AssigneeId)VALUES
('Critical Problem with HomeController.cs file',	'open',	1,	4),
('Typo fix in Judge.html',	'open',	4,	3),
('Implement documentation for UsersService.cs',	'closed',	8,	2),
('Unreachable code in Index.cs',	'open',	9,	8)

--03. Update

UPDATE Issues
SET IssueStatus = 'closed'
WHERE AssigneeId = 6

--04. Delete


DELETE FROM RepositoriesContributors
WHERE RepositoriesContributors.RepositoryId = 3

DELETE FROM Issues
WHERE Issues.RepositoryId = 3


--05. Commits

SELECT c.Id,c.Message,c.RepositoryId,c.ContributorId FROM Commits AS c 
ORDER BY c.Id,c.Message,c.RepositoryId,c.ContributorId

--06. Heavy HTML

SELECT f.Id,f.Name,f.Size FROM Files AS f
WHERE f.Size > 1000 AND f.Name LIKE '%html%'
ORDER BY f.Size DESC,f.Id,f.Name

--07. Issues and Users

SELECT i.Id,u.Username + ' : '+ i.Title AS IssueAssignee
	FROM Issues AS i
	JOIN Users AS u ON u.Id = i.AssigneeId
	ORDER BY i.Id DESC,IssueAssignee

--08. Non-Directory Files

SELECT f.Id,f.Name,CONVERT(NVARCHAR(50),f.Size )+ 'KB' AS Size
	FROM Files AS f
	LEFT JOIN Files AS f2 ON f2.ParentId = f.Id
	WHERE f2.ParentId  IS NULL
ORDER BY f.Id ,f.Name,Size DESC

--09. Most Contributed Repositories


SELECT TOP(5) r.Id,r.Name,COUNT(c.Id) AS Commits
  FROM Repositories AS r
  JOIN RepositoriesContributors AS rc ON rc.RepositoryId = r.Id
  JOIN Users AS u ON u.Id = rc.ContributorId
  JOIN Commits AS c ON c.RepositoryId = r.Id
GROUP BY r.Id,r.Name
ORDER BY Commits DESC,r.Id,r.Name

--10. User and Files

SELECT u.Username,AVG(f.Size) AS Size
FROM Users AS u
JOIN Commits AS c ON c.ContributorId = u.Id
JOIN Files AS f ON f.CommitId = c.Id
GROUP BY u.Username
ORDER BY Size DESC,u.Username

--11. User Total Commits

GO
CREATE FUNCTION udf_UserTotalCommits(@username NVARCHAR(MAX))
RETURNS INT
AS
BEGIN
	DECLARE @countForUsers INT = (SELECT COUNT(c.Id) 
	                                  FROM Users AS u
									  JOIN Commits AS c ON c.ContributorId = u.Id
									  WHERE u.Username = @username)
	RETURN @countForUsers
END

--12. Find by Extensions

GO
CREATE PROCEDURE usp_FindByExtension(@extension NVARCHAR(MAX))
AS
BEGIN  
	SELECT f.Id,f.Name,CONVERT(NVARCHAR(50),f.Size )+ 'KB' AS Size FROM Files AS f
	       WHERE CHARINDEX(@extension ,f.Name) > 0
	       ORDER BY f.Id,f.Name,Size DESC
		   
END

EXEC usp_FindByExtension 'txt'