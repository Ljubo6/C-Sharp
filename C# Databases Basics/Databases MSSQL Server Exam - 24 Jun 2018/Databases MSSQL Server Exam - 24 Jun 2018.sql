CREATE DATABASE TripService


--01. DDL

CREATE TABLE Cities(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(20) NOT NULL,
	CountryCode CHAR(2) NOT NULL
)

CREATE TABLE  Hotels(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(30) NOT NULL,
	CityId INT NOT NULL FOREIGN KEY REFERENCES Cities(Id),
	EmployeeCount INT NOT NULL,
	BaseRate DECIMAL(15,2) NOT NULL
)

CREATE TABLE Rooms(
	Id INT PRIMARY KEY IDENTITY,
	Price DECIMAL(15,2) NOT NULL,
	Type NVARCHAR(20) NOT NULL,
	Beds INT NOT NULL,
	HotelId INT NOT NULL FOREIGN KEY REFERENCES Hotels(Id)
)

CREATE TABLE Trips(
	Id INT PRIMARY KEY IDENTITY,
	RoomId INT NOT NULL FOREIGN KEY REFERENCES Rooms(Id),
	BookDate DATE NOT NULL,
	ArrivalDate DATE NOT NULL,
	ReturnDate DATE NOT NULL,
	CancelDate DATE,
	
	CONSTRAINT CH_BookDate_ArrivalDate CHECK(BookDate < ArrivalDate),
	CONSTRAINT CH_ArrivalDate_ReturnDate CHECK(ArrivalDate < ReturnDate)
)

CREATE TABLE Accounts(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	MiddleName NVARCHAR(20),
	LastName NVARCHAR(50) NOT NULL,
	CityId INT NOT NULL FOREIGN KEY REFERENCES Cities(Id),
	BirthDate DATE NOT NULL,
	Email VARCHAR(100) NOT NULL UNIQUE
)

CREATE TABLE AccountsTrips(
	 AccountId INT NOT NULL FOREIGN KEY REFERENCES Accounts(Id),
	 TripId INT NOT NULL FOREIGN KEY REFERENCES Trips(Id),
	 Luggage INT NOT NULL,
	 CONSTRAINT PK_AccountsTrips PRIMARY KEY(AccountId,TripId),
	 CONSTRAINT CH_Luggage CHECK(Luggage >= 0)
)

--02. Insert

INSERT INTO Accounts (FirstName, MiddleName, LastName, CityId, BirthDate, Email)VALUES
('John','Smith','Smith',34,'1975-07-21','j_smith@gmail.com'),
('Gosho',NULL,'Petrov',11,'1978-05-16','g_petrov@gmail.com'),
('Ivan','Petrovich','Pavlov',59,'1849-09-26','i_pavlov@softuni.bg'),
('Friedrich','Wilhelm','Nietzsche',2,'1844-10-15','f_nietzsche@softuni.bg')


INSERT INTO Trips (RoomId, BookDate, ArrivalDate, ReturnDate, CancelDate)VALUES
(101,'2015-04-12','2015-04-14','2015-04-20','2015-02-02'),
(102,'2015-07-07','2015-07-15','2015-07-22','2015-04-29'),
(103,'2013-07-17','2013-07-23','2013-07-24',NULL),
(104,'2012-03-17','2012-03-31','2012-04-01','2012-01-10'),
(109,'2017-08-07','2017-08-28','2017-08-29',NULL)
												   
--03.Update

UPDATE Rooms
SET Price *= 1.14
WHERE HotelId IN (5,7,9)

--04. Delete

DELETE FROM AccountsTrips
WHERE AccountId = 47

--05. Bulgarian Cities

SELECT c.Id,c.Name 
	FROM Cities AS c
	WHERE C.CountryCode = 'BG'
ORDER BY c.Name

--06. People Born After 1991

SELECT a.FirstName + ' ' + ISNULL(MiddleName + ' ' ,'') + A.LastName AS [Full Name],DATEPART(YEAR,a.BirthDate) AS BirthYear
	FROM Accounts AS a
	WHERE DATEPART(YEAR,a.BirthDate) > 1991 
	ORDER BY BirthYear DESC , a.FirstName

--07. EEE-Mails

SELECT a.FirstName,a.LastName,FORMAT(a.BirthDate,'MM-dd-yyyy'),c.Name,a.Email
	FROM Accounts AS a
	JOIN Cities AS c ON c.Id = a.CityId
	WHERE A.Email LIKE 'E%'
ORDER BY c.Name DESC

--08. City Statistics

SELECT c.Name AS City,COUNT(h.Id) AS Hotels
	FROM Cities AS c
	LEFT JOIN Hotels AS h ON h.CityId = c.Id
GROUP BY c.Name
ORDER BY Hotels DESC,c.Name

--09. Expensive First Class Rooms

SELECT r.Id,r.Price,h.Name AS Hotel,c.Name AS City
	FROM Rooms AS r
	JOIN Hotels AS h ON h.Id = r.HotelId
	JOIN Cities AS c ON c.Id = h.CityId
	WHERE r.Type = 'First Class'
ORDER BY r.Price DESC,r.Id

--10. Longest and Shortest Trips

SELECT a.Id AS AccountId,a.FirstName + ' ' + ISNULL(MiddleName + ' ','') + a.LastName AS [FullName],
MAX(DATEDIFF(DAY,t.ArrivalDate,t.ReturnDate)) AS LongestTrip,
MIN(DATEDIFF(DAY,t.ArrivalDate,t.ReturnDate)) AS ShortestTrip
	FROM Accounts AS a
	JOIN AccountsTrips AS at ON at.AccountId = a.Id
	JOIN Trips AS t ON t.Id = at.TripId
	WHERE a.MiddleName IS NULL AND t.CancelDate IS NULL
GROUP BY a.Id,a.FirstName,a.MiddleName,LastName
ORDER BY LongestTrip DESC,AccountId

--11. Metropolis

SELECT TOP(5) c.Id,c.Name AS City,c.CountryCode AS Country,COUNT(a.Id) AS Accounts
 FROM Cities AS c
 JOIN Accounts AS a ON a.CityId = c.Id
GROUP BY c.Id,c.Name,c.CountryCode
ORDER BY Accounts DESC

--12. Romantic Getaways

SELECT a.Id,a.Email,c.Name,COUNT(t.Id) AS Trips
	FROM Accounts AS a
	JOIN Cities AS c ON c.Id = a.CityId
	JOIN AccountsTrips AS at ON at.AccountId = a.Id
	JOIN Trips AS t ON t.Id = at.TripId
	JOIN Rooms AS r ON r.Id = t.RoomId
	JOIN Hotels AS h ON h.Id = r.HotelId
	WHERE h.CityId = a.CityId
GROUP BY  a.Id,a.Email,c.Name
ORDER BY Trips DESC,a.Id

--13. Lucrative Destinations

SELECT TOP(10) c.Id,c.Name,SUM(h.BaseRate + r.Price)  AS [Total Revenue],COUNT(t.Id) AS Trips
	FROM Cities AS c
	JOIN Hotels AS h ON h.CityId = c.Id
	JOIN Rooms AS r ON r.HotelId = h.Id
	JOIN Trips AS t ON t.RoomId = r.Id
	WHERE DATEPART(YEAR,t.BookDate) = 2016
GROUP BY c.Id,c.Name
ORDER BY [Total Revenue] DESC,Trips DESC

--14. Trip Revenues

SELECT  at.TripId, h.Name AS HotelName,r.Type AS RoomType,
CASE
WHEN t.CancelDate IS NULL THEN SUM(h.BaseRate + r.Price)
ELSE '0.00'
END AS Revenue
	FROM Hotels AS h
	JOIN Rooms AS r ON r.HotelId = h.Id
	JOIN Trips AS t ON t.RoomId = r.Id
	JOIN AccountsTrips AS at ON at.TripId = t.Id
GROUP BY  at.TripId, h.Name,r.Type,t.CancelDate	
ORDER BY RoomType,at.TripId

--15. Top Travelers


SELECT AccountId,Email,CountryCode,Trips
	FROM(
SELECT a.Id AS AccountId, a.Email ,c.CountryCode,COUNT(t.Id) AS Trips,
DENSE_RANK() OVER (
PARTITION BY c.CountryCode 
ORDER BY COUNT(t.Id) DESC,a.Id) AS Rank
	FROM Accounts AS a
	JOIN AccountsTrips AS at ON at.AccountId = a.Id 
	JOIN Trips AS t ON t.Id = at.TripId
	JOIN Rooms AS r ON r.Id = t.RoomId
	JOIN Hotels AS h ON h.Id = r.HotelId
	JOIN Cities AS c ON c.Id = h.CityId
GROUP BY  a.Id, a.Email ,c.CountryCode) AS RankPerCountry
WHERE Rank = 1
ORDER BY Trips DESC,AccountId

--16. Luggage Fees

SELECT
  at.TripId,
  SUM(at.Luggage) AS Luggage,
  '$' + CONVERT(VARCHAR(10), SUM(at.Luggage) *
                             CASE WHEN SUM(at.Luggage) > 5
                               THEN 5
                             ELSE 0 END) AS Fee
FROM AccountsTrips AS at
GROUP BY at.TripId
HAVING SUM(at.Luggage) > 0
ORDER BY Luggage DESC

--17. GDPR Violation

SELECT t.Id ,
	CONCAT(a.FirstName,' ' + a.MiddleName,' ',a.LastName) AS [Full Name],
	cc.Name AS [From],
	c.Name AS [To],
	CASE
	WHEN t.CancelDate IS NULL THEN CONCAT(DATEDIFF(DAY,t.ArrivalDate,t.ReturnDate) ,' days')
	ELSE 'Canceled'
	END AS Duration
FROM Trips AS t
	JOIN AccountsTrips AS at ON at.TripId = t.Id
	JOIN Accounts AS a ON a.Id = at.AccountId
	JOIN Rooms AS r ON r.Id = t.RoomId
	JOIN Hotels AS h ON h.Id = r.HotelId
	JOIN Cities AS c ON c.Id = h.CityId
	JOIN Cities AS cc ON cc.Id = a.CityId 
ORDER BY [Full Name],t.Id

GO
--18. Available Room

--Room {Room Id}: {Room Type} ({Beds} beds) - ${Total Price}

CREATE FUNCTION udf_GetAvailableRoom(@HotelId INT, @Date DATE, @People INT)
RETURNS VARCHAR(MAX)
AS
BEGIN
	DECLARE @AvailableRoom VARCHAR(MAX) =(
	SELECT TOP(1) CONCAT('Room ',r.Id,': ',r.Type,' (',r.Beds,' beds) - $',(h.BaseRate + r.Price) * @People) 
		FROM Hotels AS h
		JOIN Rooms AS r ON r.HotelId = h.Id
		JOIN Trips AS t ON t.RoomId = r.Id
		WHERE h.Id = @HotelId
		AND @Date NOT BETWEEN t.ArrivalDate AND t.ReturnDate
		AND t.CancelDate IS NULL
		AND r.Beds > @People
	)

	IF @AvailableRoom IS NULL
	BEGIN
		RETURN 'No rooms available'
	END

	RETURN @AvailableRoom
END

GO
SELECT dbo.udf_GetAvailableRoom(112, '2011-12-17', 2)

SELECT dbo.udf_GetAvailableRoom(94, '2015-07-26', 3)

GO
--19. Switch Room

CREATE PROCEDURE usp_SwitchRoom(@TripId INT, @TargetRoomId INT)
AS
BEGIN
DECLARE @HotelTrip INT = (SELECT TOP(1) r.HotelId FROM Trips AS t
JOIN Rooms AS r ON r.Id = t.RoomId
WHERE t.Id = @TripId
)
DECLARE @TargetRoomHotelId INT = (SELECT TOP(1) r.HotelId FROM Rooms AS r
WHERE r.Id = @TargetRoomId)

IF (@HotelTrip != @TargetRoomHotelId)
BEGIN
RAISERROR ('Target room is in another hotel!',16,1)
RETURN
END
DECLARE @TripsAccountBeds INT = (SELECT COUNT(*) FROM AccountsTrips WHERE TripId = @TripId)

IF(@TripsAccountBeds > (SELECT r.Beds FROM Rooms AS r WHERE r.Id = @TargetRoomId))
BEGIN
RAISERROR ('Not enough beds in target room!',16,2)
RETURN
END


UPDATE Trips
SET RoomId = @TargetRoomId
WHERE Id = @TripId
END

GO

EXEC usp_SwitchRoom 10, 11
SELECT RoomId FROM Trips WHERE Id = 10

EXEC usp_SwitchRoom 10, 7

EXEC usp_SwitchRoom 10, 8

GO
--20.Cancel Trip

CREATE TRIGGER tr_CancelTrip
ON Trips
INSTEAD OF DELETE
AS
UPDATE Trips
SET CancelDate = GETDATE()
WHERE Id IN (SELECT Id FROM deleted WHERE CancelDate IS NULL)