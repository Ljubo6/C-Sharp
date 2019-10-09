CREATE DATABASE Airport

USE Airport

--01. DDL

CREATE TABLE Planes(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(30) NOT NULL,
	Seats INT NOT NULL,
	Range INT NOT NULL
)

CREATE TABLE Flights(
	Id INT PRiMARY KEY IDENTITY,
	DepartureTime DATETIME,
	ArrivalTime DATETIME,
	Origin NVARCHAR(50) NOT NULL,
	Destination NVARCHAR(50) NOT NULL,
	PlaneId INT NOT NULL FOREIGN KEY REFERENCES Planes(Id)
)

CREATE TABLE Passengers(
	Id INT PRiMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Age INT NOT NULL,
	[Address] NVARCHAR(30) NOT NULL,
	PassportId CHAR(11) NOT NULL
)

CREATE TABLE LuggageTypes(
	Id INT PRiMARY KEY IDENTITY,
	Type NVARCHAR(30) NOT NULL
)

CREATE TABLE Luggages(
	Id INT PRiMARY KEY IDENTITY,
	LuggageTypeId INT NOT NULL FOREIGN KEY REFERENCES LuggageTypes(Id),
	PassengerId INT NOT NULL FOREIGN KEY REFERENCES Passengers(Id)
)

CREATE TABLE Tickets(
	Id INT PRiMARY KEY IDENTITY,
	PassengerId INT NOT NULL FOREIGN KEY REFERENCES Passengers(Id),
	FlightId INT NOT NULL FOREIGN KEY REFERENCES Flights(Id),
	LuggageId INT NOT NULL FOREIGN KEY REFERENCES Luggages(Id),
	Price DECIMAL(15,2) NOT NULL
)

--02. Insert

INSERT INTO Planes([Name],Seats,[Range])VALUES
('Airbus 336',112,5132),
('Airbus 330',432,5325),
('Boeing 369',231,2355),
('Stelt 297',254,2143),
('Boeing 338',165,5111),
('Airbus 558',387,1342),
('Boeing 128',345,5541)

INSERT INTO LuggageTypes([Type])VALUES
('Crossbody Bag'),
('School Backpack'),
('Shoulder Bag')

--03. Update

UPDATE Tickets
SET Price *= 1.13
WHERE (SELECT f.Destination 
			FROM Flights AS f
			JOIN Tickets AS t ON t.FlightId = f.Id
			WHERE f.Destination = 'Carlsbad') = 'Carlsbad'
--04. Delete

DELETE FROM Tickets
WHERE FlightId = (SELECT Id FROM Flights WHERE Destination = 'Ayn Halagim')

DELETE FROM Flights
WHERE Destination = 'Ayn Halagim'

--05. Trips

SELECT F.Origin,F.Destination 
FROM Flights AS f
ORDER BY F.Origin,F.Destination

--06. The "Tr" Planes

SELECT p.Id,p.Name,p.Seats,p.Range
FROM Planes AS p
WHERE P.Name LIKE '%tr%'
ORDER BY p.Id,p.Name,p.Seats,p.Range

--07. Flight Profits

SELECT t.FlightId,SUM(t.Price) AS TotalPrice
FROM Tickets AS t
GROUP BY t.FlightId
ORDER BY TotalPrice DESC,t.FlightId

--08. Passanger and Prices

SELECT TOP(10) p.FirstName,p.LastName,t.Price
FROM Passengers AS p
JOIN Tickets AS t ON t.PassengerId = p.Id
ORDER BY t.Price DESC,p.FirstName,p.LastName

--09. Top Luggages

SELECT lt.Type,COUNT(p.Id) AS Count
FROM LuggageTypes AS lt
JOIN Luggages AS l ON l.LuggageTypeId = lt.Id
JOIN Passengers AS p ON p.Id = l.PassengerId
GROUP BY lt.Type
ORDER BY Count DESC, lt.Type

--10. Passanger Trips

SELECT p.FirstName + ' ' + p.LastName AS [Full Name] ,f.Origin,f.Destination
FROM Passengers AS p
JOIN Tickets AS t ON t.PassengerId = p.Id
JOIN Flights AS f ON f.Id = t.FlightId
ORDER BY [Full Name] ,f.Origin,f.Destination

--11. Non Adventures People

--SELECT pas.FirstName,pas.LastName,pas.Age 
--FROM Passengers AS pas
--WHERE pas.Id NOT IN (
--SELECT *--p.Id
--FROM Passengers AS p
--LEFT JOIN Tickets AS t ON t.PassengerId = p.Id)
--ORDER BY pas.Age DESC,pas.FirstName,pas.LastName


SELECT pas.FirstName,pas.LastName,pas.Age 
FROM Passengers AS pas
LEFT JOIN Tickets AS t ON t.PassengerId = pas.Id
WHERE t.Id IS NULL
ORDER BY pas.Age DESC,pas.FirstName,pas.LastName

--12. Lost Luggages

SELECT p.PassportId,p.Address
FROM Passengers AS p
LEFT JOIN Luggages AS l ON l.PassengerId = p.Id
WHERE l.Id IS NULL
ORDER BY p.PassportId,p.Address

--13. Count of Trips

SELECT p.FirstName,p.LastName,ISNULL(COUNT(pl.Id),0) AS [Total Trips]
FROM Passengers AS p
LEFT JOIN Tickets AS t ON t.PassengerId = p.Id
LEFT JOIN Flights AS f ON f.Id = t.FlightId
LEFT JOIN Planes AS pl ON pl.Id = f.PlaneId
GROUP BY p.FirstName,P.LastName
ORDER BY [Total Trips] DESC, p.FirstName,p.LastName

--14. Full Info

SELECT p.FirstName + ' ' + p.LastName AS [Full Name],
	pl.Name AS [Plane Name],f.Origin + ' - ' + f.Destination AS Trip,
	lt.Type AS [Luggage Type]
FROM Passengers AS p
JOIN Tickets AS t ON t.PassengerId = p.Id
JOIN Flights AS f ON f.Id = t.FlightId
JOIN Planes AS pl ON pl.Id = f.PlaneId
JOIN Luggages AS l ON l.Id = T.LuggageId
JOIN LuggageTypes AS lt ON lt.Id = l.LuggageTypeId
ORDER BY [Full Name],Name,Origin,Destination,Type

--15. Most Expesnive Trips

SELECT h.FirstName,h.LastName,h.Destination,h.Price
FROM(
SELECT p.FirstName,P.LastName,f.Destination,t.Price,ROW_NUMBER() OVER(PARTITION BY p.FirstName ORDER BY t.Price DESC) AS RowPrice
FROM Passengers AS p
LEFT JOIN Tickets AS t ON t.PassengerId = p.Id
LEFT JOIN Flights AS f ON f.Id = t.FlightId
WHERE t.Id IS NOT NULL
) AS h
WHERE h.RowPrice = 1
ORDER BY H.Price DESC,h.FirstName,h.LastName,h.Destination

--16. Destinations Info

SELECT f.Destination,ISNULL(COUNT(t.Id),0) AS FilesCount
FROM Flights AS f
LEFT JOIN Tickets AS t ON t.FlightId = f.Id
GROUP BY f.Destination
ORDER BY FilesCount DESC,f.Destination

--17. PSP

SELECT pl.Name,pl.Seats AS Seats,ISNULL(COUNT(t.Id),0) AS [Passengers Count]
FROM Planes AS pl
LEFT JOIN Flights AS f ON f.PlaneId = pl.Id
LEFT JOIN Tickets AS t ON t.FlightId = f.Id
GROUP BY pl.Name,pl.Seats
ORDER BY [Passengers Count] DESC,pl.Name,Seats

GO
--18. Vacation

CREATE FUNCTION udf_CalculateTickets(@origin NVARCHAR(50), @destination NVARCHAR(50), @peopleCount INT) 
RETURNS NVARCHAR(MAX)
AS
BEGIN
	IF(@peopleCount <= 0)
	BEGIN
		RETURN('Invalid people count!')
	END
	DECLARE @passengerId INT = (SELECT f.Id FROM Flights AS f WHERE f.Origin = @origin AND f.Destination = @destination)

	IF(@passengerId IS NULL)
	BEGIN
		RETURN('Invalid flight!')
	END
	DECLARE @price DECIMAL(15,2) = (SELECT t.Price FROM Flights AS f 
										JOIN Tickets AS t ON t.FlightId = f.Id
										WHERE f.Origin = @origin AND f.Destination = @destination)

	DECLARE @totalPrice DECIMAL(15,2) = @peopleCount * @price
	RETURN('Total price ' + CAST(@totalPrice AS NVARCHAR(20)))
END

GO

SELECT dbo.udf_CalculateTickets('Kolyshley','Rancabolang', 33)

SELECT dbo.udf_CalculateTickets('Kolyshley','Rancabolang', -1)

SELECT dbo.udf_CalculateTickets('Invalid','Rancabolang', 33)

GO
--19. Wrong Data

CREATE PROCEDURE usp_CancelFlights
As
BEGIN
	UPDATE Flights
	SET DepartureTime = NULL , ArrivalTime = NULL
	WHERE DepartureTime < ArrivalTime
END

--20. Deleted Planes

CREATE TABLE DeletedPlanes(
	Id INT ,
	Name NVARCHAR(30),
	Seats INT ,
	Range INT
)
GO

CREATE TRIGGER tr_DeletedPlanes
ON Planes
AFTER DELETE
AS
	INSERT INTO DeletedPlanes (Id,Name,Seats, Range)
	SELECT Id,Name,Seats, Range FROM deleted




DELETE Tickets
WHERE FlightId IN (SELECT Id FROM Flights WHERE PlaneId = 8)

DELETE FROM Flights
WHERE PlaneId = 8

DELETE FROM Planes
WHERE Id = 8
