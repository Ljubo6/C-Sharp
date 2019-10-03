CREATE DATABASE ColonialJourney 
USE ColonialJourney 
--01. DDL

CREATE TABLE Planets(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(30) NOT NULL
)

CREATE TABLE Spaceports(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	PlanetId INT NOT NULL FOREIGN KEY REFERENCES Planets(Id)
)
CREATE TABLE Spaceships(
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	Manufacturer VARCHAR(30) NOT NULL,
	LightSpeedRate INT DEFAULT (0)
)
CREATE TABLE Colonists(
	Id INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(20) NOT NULL,
	LastName VARCHAR(20) NOT NULL,
	Ucn VARCHAR(10) UNIQUE NOT NULL,
	BirthDate DATE NOT NULL
)

CREATE TABLE Journeys(
	Id INT PRIMARY KEY IDENTITY,
	JourneyStart DATETIME NOT NULL,
	JourneyEnd DATETIME NOT NULL,
	Purpose VARCHAR(11) CHECK(Purpose  IN ('Medical','Technical','Educational','Military')),
	DestinationSpaceportId INT NOT NULL FOREIGN KEY REFERENCES Spaceports(Id),
	SpaceshipId INT NOT NULL FOREIGN KEY REFERENCES Spaceships(Id)
)
CREATE TABLE TravelCards(
	Id INT PRIMARY KEY IDENTITY,
	CardNumber CHAR(10) NOT NULL UNIQUE,
	JobDuringJourney VARCHAR(8) CHECK(JobDuringJourney IN ('Pilot','Engineer','Trooper','Cleaner','Cook')),
	ColonistId INT NOT NULL FOREIGN KEY REFERENCES Colonists(Id),
	JourneyId INT NOT NULL FOREIGN KEY REFERENCES Journeys(Id),
)

--02. Insert

INSERT INTO Planets ([Name]) VALUES
('Mars'),
('Earth'),
('Jupiter'),
('Saturn')

INSERT INTO Spaceships([Name],Manufacturer,LightSpeedRate) VALUES
('Golf',	'VW',	3),
('WakaWaka',	'Wakanda',	4),
('Falcon9',	'SpaceX',	1),
('Bed',	'Vidolov',	6)

--03. Update

UPDATE Spaceships
SET LightSpeedRate += 1
WHERE Id BETWEEN 8 AND 12

--04. Delete

DELETE FROM TravelCards
WHERE JourneyId IN (1,2,3)

DELETE FROM Journeys
WHERE Id IN (1,2,3)

--05. Select All Travel Cards

SELECT tc.CardNumber,tc.JobDuringJourney
  FROM TravelCards AS tc
ORDER BY tc.CardNumber

--06. Select All Colonists

SELECT c.Id,c.FirstName + ' ' + c.LastName AS [Full Name],c.Ucn
  FROM Colonists AS c
 ORDER BY c.FirstName,c.LastName,c.Id

--07. Select All Military Journeys

SELECT j.Id,CONVERT(VARCHAR ,j.JourneyStart,103) AS JourneyStart ,CONVERT(VARCHAR,j.JourneyEnd ,103) AS JourneyEnd
  FROM Journeys AS j
  WHERE j.Purpose = 'Military'
  ORDER BY j.JourneyStart

--08. Select All Pilots

SELECT c.Id,c.FirstName + ' ' + LastName AS FullName
  FROM Colonists AS c
  JOIN TravelCards AS tc ON tc.ColonistId = c.Id
  WHERE tc.JobDuringJourney = 'Pilot'
  ORDER BY c.Id

--09. Count Colonists

SELECT * FROM Colonists

SELECT COUNT(c.Id) AS Count
  FROM Colonists AS c
  JOIN TravelCards AS tc ON tc.ColonistId = c.Id
  JOIN Journeys AS j ON j.Id = TC.JourneyId
 WHERE j.Purpose = 'Technical'

--10. Select The Fastest Spaceship

SELECT TOP(1) ssh.Name AS SpaceshipName,sp.Name AS  SpaceportName
  FROM Spaceships AS ssh
  JOIN Journeys AS j ON j.SpaceshipId = ssh.Id
  JOIN Spaceports AS sp ON sp.Id = j.DestinationSpaceportId
  ORDER BY ssh.LightSpeedRate DESC

--11. Select Spaceships With Pilots

SELECT ssh.Name,ssh.Manufacturer
  FROM Spaceships AS ssh
  JOIN Journeys AS j ON j.SpaceshipId = ssh.Id
  JOIN TravelCards AS tc ON tc.JourneyId = j.Id
  JOIN Colonists AS c ON c.Id = tc.ColonistId
  WHERE tc.JobDuringJourney = 'Pilot' AND DATEDIFF(YEAR,c.BirthDate,'01/01/2019') < 30
  ORDER BY ssh.Name

--12. Select All Educational Mission

SELECT p.Name AS PlanetName,sp.Name AS SpaceportName
  FROM Planets AS p
  JOIN Spaceports AS sp ON sp.PlanetId = p.Id
  JOIN Journeys AS j ON j.DestinationSpaceportId = sp.Id
  WHERE j.Purpose = 'Educational'
  ORDER BY SpaceportName DESC

--13. Planets And Journeys

SELECT p.Name AS PlanetName,COUNT(J.Id) AS JourneysCount
  FROM Planets AS p
  JOIN Spaceports AS sp ON sp.PlanetId = p.Id
  JOIN Journeys AS j ON j.DestinationSpaceportId = sp.Id
  GROUP BY p.Name
  ORDER BY JourneysCount DESC,PlanetName

--14. Extract The Shortest Journey

SELECT TOP(1) j.Id,p.Name AS PlanetName,sp.Name AS SpaceportName,j.Purpose AS JourneyPurpose
  FROM Planets AS p
  JOIN Spaceports AS sp ON sp.PlanetId = p.Id
  JOIN Journeys AS j ON j.DestinationSpaceportId = sp.Id
  ORDER BY (DATEDIFF(DAY,j.JourneyStart,j.JourneyEnd)) 

--15. Select The Less Popular Job

SELECT TOP(1)j.Id AS JourneyId, tc.JobDuringJourney AS JobName
  FROM Planets AS p
  JOIN Spaceports AS sp ON sp.PlanetId = p.Id
  JOIN Journeys AS j ON j.DestinationSpaceportId = sp.Id
  JOIN TravelCards AS tc ON tc.JourneyId = j.Id
  JOIN Colonists AS col ON col.Id = tc.ColonistId
  ORDER BY (DATEDIFF(DAY,j.JourneyStart,j.JourneyEnd)) DESC

--16. Select Special Colonists



SELECT h.JobDuringJourney,c.FirstName + ' ' + c.LastName AS FullName,h.Rank
FROM
(SELECT tc.JobDuringJourney,tc.ColonistId ,DENSE_RANK() OVER(PARTITION BY tc.JobDuringJourney ORDER BY col.BirthDate) AS Rank
  FROM TravelCards AS tc
  JOIN Colonists AS col ON col.Id = tc.ColonistId
  GROUP BY tc.JobDuringJourney,tc.ColonistId, col.BirthDate ) AS h
  JOIN Colonists AS c ON c.Id = h.ColonistId
  WHERE h.Rank = 2
  ORDER BY h.JobDuringJourney

--17. Planets and Spaceports

SELECT p.Name AS [Name],COUNT(sp.Id) AS Count
  FROM Planets AS p
  LEFT JOIN Spaceports AS sp ON sp.PlanetId = p.Id
  GROUP BY p.Name
  ORDER BY Count DESC,[Name] 
  GO
--18. Get Colonists Count

CREATE FUNCTION dbo.udf_GetColonistsCount(@PlanetName VARCHAR (30))
RETURNS INT
AS
BEGIN
	RETURN (SELECT COUNT(col.Id) AS Count 
					FROM Planets AS p
					JOIN Spaceports AS sp ON sp.PlanetId = p.Id
					JOIN Journeys AS j ON j.DestinationSpaceportId = sp.Id
					JOIN TravelCards AS tc ON tc.JourneyId = j.Id
					JOIN Colonists AS col ON col.Id = tc.ColonistId
					WHERE p.Name = @PlanetName)
END

GO

SELECT dbo.udf_GetColonistsCount('Otroyphus')

GO
--19. Change Journey Purpose

CREATE PROCEDURE usp_ChangeJourneyPurpose(@JourneyId INT, @NewPurpose VARCHAR(30))
AS
BEGIN
	DECLARE @TargetJourneyId INT = (SELECT Id FROM Journeys WHERE Id = @JourneyId)
	IF (@TargetJourneyId IS NULL)
	BEGIN
		RAISERROR('The journey does not exist!',16,1)
	END

	DECLARE @TargetPurpose VARCHAR(30) = (SELECT Purpose FROM Journeys WHERE Id = @JourneyId)


	IF(@TargetPurpose = @NewPurpose)
	BEGIN
		RAISERROR('You cannot change the purpose!',16,2)
	END

	UPDATE Journeys
	SET Purpose = @NewPurpose
	WHERE Id = @JourneyId
END

GO

EXEC usp_ChangeJourneyPurpose 1, 'Technical'
SELECT * FROM Journeys

EXEC usp_ChangeJourneyPurpose 2, 'Educational'

EXEC usp_ChangeJourneyPurpose 196, 'Technical'


--20. Deleted Journeys


CREATE TABLE DeletedJourneys(
	Id INT, 
	JourneyStart DATETIME, 
	JourneyEnd DATETIME, 
	Purpose VARCHAR(11), 
	DestinationSpaceportId INT, 
	SpaceshipId INT
)
GO

CREATE TRIGGER tr_JourneyWasDeleted
ON Journeys
AFTER DELETE
AS
BEGIN
	INSERT INTO DeletedJourneys(Id,JourneyStart,JourneyEnd,Purpose,DestinationSpaceportId,SpaceshipId)
	SELECT Id,JourneyStart,JourneyEnd,Purpose,DestinationSpaceportId,SpaceshipId FROM deleted
END

GO

DELETE FROM TravelCards
WHERE JourneyId =  1

DELETE FROM Journeys
WHERE Id =  1
