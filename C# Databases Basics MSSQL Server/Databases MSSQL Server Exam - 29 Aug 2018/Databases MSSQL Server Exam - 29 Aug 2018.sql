CREATE DATABASE Supermarket

USE Supermarket

--01. DDL

CREATE Table Categories(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL
)

CREATE TABLE Items(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL,
	Price DECIMAL(15,2),
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id)
)

CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Phone CHAR(12) NOT NULL,
	Salary DECIMAL(15,2) NOT NULL
)

CREATE TABLE Orders(
	Id INT PRIMARY KEY IDENTITY,
	DateTime DATETIME NOT NULL,
	EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(Id)
)

CREATE TABLE OrderItems(
	OrderId INT NOT NULL FOREIGN KEY REFERENCES Orders(Id),
	ItemId INT NOT NULL FOREIGN KEY REFERENCES Items(Id),
	Quantity INT NOT NULL CHECK(Quantity >= 1)

	PRIMARY KEY(OrderId,ItemId)
)

CREATE TABLE Shifts(
	Id INT NOT NULL IDENTITY,
	EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(Id),
	CheckIn DATETIME NOT NULL,
	CheckOut DATETIME NOT NULL

	PRIMARY KEY(Id,EmployeeId)
)

ALTER TABLE Shifts
ADD CONSTRAINT CH_CheckInOut CHECK(CheckIn < CheckOut)

--02. Insert

INSERT INTO Employees (FirstName,LastName,Phone,Salary) VALUES
('Stoyan','Petrov','888-785-8573',500.25),
('Stamat','Nikolov','789-613-1122',999995.25),
('Evgeni','Petkov','645-369-9517',1234.51),
('Krasimir','Vidolov','321-471-9982',50.25)



INSERT INTO Items([Name],Price,CategoryId) VALUES
('Tesla battery',154.25,8),
('Chess',30.25,8),
('Juice',5.32,1),
('Glasses',10,8),
('Bottle of water',1,1)

--03. Update

UPDATE Items
SET Price *= 1.27
WHERE CategoryId IN (1,2,3)

--04. Delete

DELETE OrderItems
WHERE OrderId = 48

--05. Richest People

SELECT Id,FirstName
FROM Employees
WHERE Salary > 6500
ORDER BY FirstName,Id

--06. Cool Phone Numbers

SELECT CONCAT(FirstName,' ',LastName) AS [Full Name],Phone AS [Phone Number]
FROM Employees
WHERE Phone LIKE '3%'
ORDER BY [Full Name],[Phone Number]

--07. Employee Statistics

SELECT e.FirstName,e.LastName,COUNT(o.Id) AS [Count]
FROM Employees AS e
JOIN Orders AS o ON o.EmployeeId = e.Id
GROUP BY e.FirstName,e.LastName
ORDER BY [Count] DESC,e.FirstName

--08. Hard Workers Club

SELECT e.FirstName,e.LastName,AVG(DATEDIFF(HOUR,sh.CheckIn,sh.CheckOut)) AS [Works Ours]
FROM Employees AS e
JOIN Shifts AS sh ON sh.EmployeeId = e.Id
GROUP BY e.Id,e.FirstName,e.LastName
HAVING AVG(DATEDIFF(HOUR,sh.CheckIn,sh.CheckOut)) > 7
ORDER BY [Works Ours] DESC,e.Id

--09. The Most Expensive Order

SELECT TOP(1) o.Id AS OrderId,SUM(oi.Quantity * i.Price) AS TotalPrice
FROM Orders AS o
JOIN OrderItems AS oi ON oi.OrderId = o.Id
JOIN Items AS i ON i.Id = oi.ItemId
GROUP BY o.Id
ORDER BY TotalPrice DESC

--10. Rich Item, Poor Item

SELECT TOP(10) o.Id AS OrderId,MAX(i.Price) AS ExpensivePrice,MIN(i.Price) AS CheapPrice
FROM Orders as o
JOIN OrderItems AS oi ON oi.OrderId = o.Id
JOIN Items AS i ON i.Id = oi.ItemId
GROUP BY o.Id
ORDER BY ExpensivePrice DESC,o.Id

--11. Cashiers

SELECT DISTINCT e.Id,e.FirstName,e.LastName
FROM Employees AS e
JOIN Orders AS o ON o.EmployeeId = e.Id
ORDER BY e.Id

--12. Lazy Employees

SELECT DISTINCT e.Id,e.FirstName + ' ' + e.LastName AS [Full Name]
FROM Employees AS e
JOIN Shifts AS sh ON sh.EmployeeId = e.Id
WHERE DATEDIFF(HOUR,sh.CheckIn,sh.CheckOut) < 4
ORDER BY e.Id

--13. Sellers

SELECT TOP(10) e.FirstName + ' ' + e.LastName As [Full Name],SUM(oi.Quantity * i.Price) AS [Total Price],SUM(oi.Quantity) AS Items
FROM Employees AS e
JOIN Orders AS o ON o.EmployeeId = e.Id
JOIN OrderItems AS oi ON oi.OrderId = o.Id
JOIN Items AS i ON i.Id = oi.ItemId
WHERE o.DateTime < '2018-06-15'
GROUP BY e.FirstName,e.LastName
ORDER BY [Total Price] DESC,Items DESC

--14. Tough Days

SELECT e.FirstName + ' ' + LastName AS [Full Name],DATENAME(WEEKDAY, sh.CheckOut) AS [Day of week]
FROM Employees AS e
JOIN Shifts AS sh ON sh.EmployeeId = e.Id
LEFT JOIN Orders AS o ON o.EmployeeId = e.Id
WHERE o.Id IS NULL AND DATEDIFF(HOUR,sh.CheckIn,sh.CheckOut) > 12
ORDER BY e.Id

--15. Top Order per Employee

SELECT k.[Full Name],DATEDIFF(HOUR,sh.CheckIn,sh.CheckOut) AS WorkHours,k.TotalSum
FROM(
SELECT o.Id AS OrderId,e.Id AS EmployeeId,o.DateTime,e.FirstName + ' ' + e.LastName AS [Full Name],SUM(oi.Quantity * i.Price) AS TotalSum,
ROW_NUMBER() OVER(PARTITION BY e.Id ORDER BY SUM(oi.Quantity * i.Price) DESC) AS RowNumber
FROM Employees AS e
JOIN Orders AS o ON o.EmployeeId = e.Id
JOIN OrderItems AS oi ON oi.OrderId = o.Id
JOIN Items AS i ON i.Id = oi.ItemId
GROUP BY o.Id,e.FirstName,e.LastName,e.Id,o.DateTime) AS k
JOIN Shifts AS sh ON sh.EmployeeId = k.EmployeeId
WHERE k.RowNumber = 1 AND k.DateTime BETWEEN sh.CheckIn AND sh.CheckOut
ORDER BY k.[Full Name],WorkHours DESC,k.TotalSum DESC

--16. Average Profit per Day

SELECT DATEPART(DAY,O.DateTime) AS [Day],CAST(AVG(oi.Quantity * i.Price) AS DECIMAL(15,2)) AS [Total profit]
FROM Orders AS o
JOIN OrderItems AS oi ON oi.OrderId = o.Id
JOIN Items AS i ON i.Id = oi.ItemId
GROUP BY DATEPART(DAY,O.DateTime)
ORDER BY DATEPART(DAY,O.DateTime)

--17. Top Products

SELECT i.Name AS Item, c.Name AS Category,SUM(oi.Quantity) AS COUNT,SUM(oi.Quantity * i.Price) AS Totalprice
FROM Orders AS o
  JOIN OrderItems AS oi ON oi.OrderId = o.Id
  RIGHT JOIN Items AS i ON i.Id = oi.ItemId
  JOIN Categories AS c ON c.Id = i.CategoryId
GROUP BY i.Name,c.Name
ORDER BY Totalprice DESC,COUNT DESC

GO
--18. Promotion Days

CREATE FUNCTION udf_GetPromotedProducts(@CurrentDate DATETIME, @StartDate DATETIME, @EndDate DATETIME, @Discount INT, @FirstItemId INT, @SecondItemId INT, @ThirdItemId INT)
RETURNS VARCHAR(MAX)
AS
BEGIN
	DECLARE @FirstItemPrice DECIMAL(15,2) = (SELECT Price FROM Items WHERE Id = @FirstItemId )
	DECLARE @SecondItemPrice DECIMAL(15,2) = (SELECT Price FROM Items WHERE Id = @SecondItemId )
	DECLARE @ThirdItemPrice DECIMAL(15,2) = (SELECT Price FROM Items WHERE Id = @ThirdItemId )
	IF(@FirstItemPrice IS NULL OR @SecondItemPrice IS NULL OR @ThirdItemPrice IS NULL)
	BEGIN
		RETURN 'One of the items does not exists!'
	END

	IF(@CurrentDate <= @StartDate OR @CurrentDate >= @EndDate)
	BEGIN
		RETURN 'The current date is not within the promotion dates!'
	END

	DECLARE @NewFirstItemPrice DECIMAL(15,2) = @FirstItemPrice - ((@Discount * @FirstItemPrice) / 100)
	DECLARE @NewSecondItemPrice DECIMAL(15,2) = @SecondItemPrice - ((@Discount * @SecondItemPrice) / 100)
	DECLARE @NewThirdItemPrice DECIMAL(15,2) = @ThirdItemPrice - ((@Discount * @ThirdItemPrice) / 100)

	DECLARE @FirstItemName VARCHAR(50) = (SELECT [Name] FROM Items WHERE Id = @FirstItemId)
	DECLARE @SecondItemName VARCHAR(50) = (SELECT [Name] FROM Items WHERE Id = @SecondItemId)
	DECLARE @ThirdItemName VARCHAR(50) = (SELECT [Name] FROM Items WHERE Id = @ThirdItemId)

	RETURN @FirstItemName + ' price: ' + CAST(ROUND(@NewFirstItemPrice,2) AS VARCHAR) + ' <-> ' + @SecondItemName + ' price: ' + CAST(ROUND(@NewSecondItemPrice,2) AS VARCHAR) + ' <-> ' + @ThirdItemName + ' price: ' + CAST(ROUND(@NewThirdItemPrice,2) AS VARCHAR)

END

GO

SELECT dbo.udf_GetPromotedProducts('2018-08-02', '2018-08-01', '2018-08-03',13, 3,4,5)

SELECT dbo.udf_GetPromotedProducts('2018-08-01', '2018-08-02', '2018-08-03',13,3 ,4,5)

GO

--19. Cancel Order

CREATE PROCEDURE usp_CancelOrder(@OrderId INT, @CancelDate DATETIME)
AS
BEGIN
	DECLARE @Order INT = (SELECT Id FROM Orders WHERE Id = @OrderId)
	IF (@Order IS NULL)
	BEGIN
		RAISERROR('The order does not exist!',16,1)
	END

	DECLARE @IssueDate DATETIME = (SELECT DateTime FROM Orders WHERE Id = @OrderId)
	DECLARE @Datediff INT = (SELECT DATEDIFF(DAY,@IssueDate,@CancelDate))

	IF(@Datediff > 3)
	BEGIN
		RAISERROR('You cannot cancel the order!',16,2)
	END
	DELETE FROM OrderItems
	WHERE OrderId = @OrderId
	
	DELETE FROM Orders
	WHERE Id = @OrderId
END

GO

EXEC usp_CancelOrder 1, '2018-06-02'
SELECT COUNT(*) FROM Orders
SELECT COUNT(*) FROM OrderItems

EXEC usp_CancelOrder 1, '2018-06-15'

EXEC usp_CancelOrder 124231, '2018-06-15'

--20. Deleted Orders

CREATE TABLE DeletedOrders(
	OrderId INT,
	ItemId INT,
	ItemQuantity INT
)
GO
CREATE TRIGGER tr_DeleteOrders
ON OrderItems AFTER DELETE
AS
BEGIN
	INSERT INTO DeletedOrders(OrderId,ItemId,ItemQuantity)
	SELECT d.OrderId,d.ItemId,d.Quantity FROM deleted AS d
END

GO
DELETE FROM OrderItems
WHERE OrderId = 5

DELETE FROM Orders
WHERE Id = 5 
