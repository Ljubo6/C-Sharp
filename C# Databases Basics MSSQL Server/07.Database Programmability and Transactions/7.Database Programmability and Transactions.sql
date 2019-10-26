--01. Employees with Salary Above 35000

CREATE PROCEDURE usp_GetEmployeesSalaryAbove35000 AS
SELECT e.FirstName,e.LastName
	FROM Employees AS e
WHERE e.Salary > 35000

EXEC usp_GetEmployeesSalaryAbove35000

GO

--02. Employees with Salary Above Number

CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber (@salary DECIMAL(18,4)) AS
SELECT e.FirstName,e.LastName
	FROM Employees AS E
	WHERE E.Salary >= @salary
GO
EXEC usp_GetEmployeesSalaryAboveNumber 48100
GO
--03. Town Names Starting With

CREATE  PROCEDURE usp_GetTownsStartingWith (@inputText VARCHAR(50)) AS
BEGIN
	SELECT [Name]
		FROM Towns
		WHERE [Name] LIKE @inputText + '%'
END

EXEC usp_GetTownsStartingWith 'b'

GO

--04. Employees from Town

CREATE PROCEDURE usp_GetEmployeesFromTown (@townName VARCHAR(50)) AS
BEGIN
	SELECT FirstName,LastName
		FROM Employees AS E
		JOIN Addresses AS a ON a.AddressID = e.AddressID
		JOIN Towns AS t ON t.TownID = a.TownID
	WHERE t.Name LIKE @townName + '%'
END

EXEC usp_GetEmployeesFromTown 'Sofia'

GO
--05. Salary Level Function

CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4)) 
RETURNS CHAR(7)
BEGIN
	IF (@salary < 30000)
	BEGIN
		RETURN 'Low'
	END
	ELSE IF (@salary BETWEEN 30000 AND 50000)
	BEGIN
		RETURN 'Average'
	END

	RETURN 'High'
END

GO

SELECT Salary,dbo.ufn_GetSalaryLevel(Salary)
	FROM Employees

GO

--06. Employees by Salary Level

CREATE PROCEDURE usp_EmployeesBySalaryLevel @salaryLevel CHAR(7) AS
BEGIN
	SELECT FirstName,LastName 
		FROM Employees
		WHERE dbo.ufn_GetSalaryLevel(Salary) = @salaryLevel
END

EXEC usp_EmployeesBySalaryLevel 'High'

GO

--07. Define Function

CREATE  FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(MAX), @word VARCHAR(MAX)) 
RETURNS BIT
BEGIN
	DECLARE @index INT = 1
	DECLARE @currentChar CHAR(1)
	DECLARE @isComprised INT

	WHILE(@index <= LEN(@word))
	BEGIN
		SET @currentChar = SUBSTRING(@word,@index,1)
		SET @isComprised = CHARINDEX(@currentChar,@setOfLetters)
		IF(@isComprised = 0)
		BEGIN
			RETURN 0
		END
		SET @index += 1
	END
	RETURN 1
END

GO 

SELECT dbo.ufn_IsWordComprised('oistmiahf', 'Sofia')
SELECT dbo.ufn_IsWordComprised('oistmiahf', 'halves')
SELECT dbo.ufn_IsWordComprised('bobr', 'Rob')
SELECT dbo.ufn_IsWordComprised('pppp', 'Guy')

GO

--08. Delete Employees and Departments

CREATE PROCEDURE usp_DeleteEmployeesFromDepartment  (@departmentId INT) AS
BEGIN
	DELETE FROM EmployeesProjects
	WHERE EmployeeID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

	ALTER TABLE Departments
	ALTER COLUMN ManagerID INT

	UPDATE Employees
	SET ManagerID = NULL
	WHERE ManagerID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

	UPDATE Departments
	SET ManagerID = NULL
	WHERE ManagerID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

	DELETE FROM Employees
	WHERE DepartmentID = @departmentId

	DELETE FROM Departments
	WHERE DepartmentID = @departmentId

	SELECT COUNT(*)
		FROM Employees
	WHERE DepartmentID = @departmentId
END

GO

--09. Find Full Name

CREATE PROCEDURE usp_GetHoldersFullName AS
BEGIN
	SELECT CONCAT(FirstName,' ',LastName) AS [Full Name]
		FROM AccountHolders
END

EXEC usp_GetHoldersFullName 

GO
--10. People with Balance Higher Than

CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan (@inputNumber DECIMAL(15,4)) AS 

BEGIN
	WITH CTE_AccountHolderBalance (AccountHolderID,Balance) AS(
	SELECT AccountHolderId,SUM(Balance) AS TotalBalance
	FROM Accounts
	GROUP BY AccountHolderId)

	SELECT FirstName,LastName
		FROM AccountHolders AS ah
		JOIN CTE_AccountHolderBalance AS cab ON cab.AccountHolderID = ah.Id
		WHERE cab.Balance > @inputNumber
		ORDER BY ah.FirstName ,ah.LastName
END

EXEC usp_GetHoldersWithBalanceHigherThan 0.0

GO
--11. Future Value Function

CREATE FUNCTION ufn_CalculateFutureValue (@sum DECIMAL(15,4),@interestRate FLOAT,@years INT)
RETURNS DECIMAL(15,4)
BEGIN
	RETURN @sum * POWER((1 + @interestRate),@years)
END

SELECT dbo.ufn_CalculateFutureValue(1000, 0.1, 5)
GO

--12. Calculating Interest

CREATE PROCEDURE usp_CalculateFutureValueForAccount (@accountID INT,@interestRate FLOAT) AS
BEGIN
	SELECT a.Id,ah.FirstName,ah.LastName,a.Balance,dbo.ufn_CalculateFutureValue(Balance,@interestRate,5) AS [Balance in 5 years]
		FROM Accounts AS a
		JOIN AccountHolders AS ah ON ah.Id = a.AccountHolderId
		WHERE a.Id = @accountID
END

GO

EXEC  dbo.usp_CalculateFutureValueForAccount 1 , 0.1

GO
--13. *Cash in User Games Odd Rows
CREATE FUNCTION ufn_CashInUsersGames (@gameName VARCHAR(50))
RETURNS TABLE
AS
RETURN
(
SELECT SUM(e.Cash) AS [SumCash]
FROM(
	SELECT g.Id,ug.Cash ,ROW_NUMBER() OVER(ORDER BY ug.Cash DESC) AS [RowNumber] 
		FROM Games AS g
		JOIN UsersGames AS ug ON ug.GameId = g.Id
		WHERE g.Name = @gameName) AS e
	WHERE e.RowNumber % 2 = 1
)
GO
SELECT * FROM dbo.ufn_CashInUsersGames('Lily Stargazer')

--14. Create Table Logs
GO

CREATE TABLE Logs(
	LogId INT PRIMARY KEY IDENTITY,
	AccountId INT FOREIGN KEY REFERENCES Accounts(Id),
	OldSum DECIMAL(15,2),
	NewSum DECIMAL(15,2)
)
GO

CREATE TRIGGER tr_InsertAccountsInfo ON Accounts FOR UPDATE
AS
DECLARE @newSum DECIMAL (15,2) = (SELECT Balance FROM inserted)
DECLARE @oldSum DECIMAL (15,2) = (SELECT Balance FROM deleted)
DECLARE @accountId INT = (SELECT Id FROM inserted)

INSERT INTO Logs(AccountId,NewSum,OldSum) VALUES
(@accountId,@newSum,@oldSum)

UPDATE Accounts
SET Balance += 10
WHERE Id = 1

SELECT *
	FROM Accounts WHERE Id = 1

SELECT *
	FROM Logs

--15. Create Table Emails

CREATE TABLE NotificationEmails(
Id INT PRIMARY KEY IDENTITY, 
Recipient INT FOREIGN KEY REFERENCES Accounts(Id), 
Subject VARCHAR(50), 
Body VARCHAR (MAX))

GO

CREATE TRIGGER tr_LogMail ON Logs FOR INSERT
AS
DECLARE @accountId INT = (SELECT TOP(1) AccountId FROM inserted)
DECLARE @oldSum DECIMAL(15,2) = (SELECT TOP(1) OldSum FROM inserted)
DECLARE @newSum DECIMAL(15,2) = (SELECT TOP(1) NewSum FROM inserted)

INSERT INTO NotificationEmails(Recipient,Subject,Body) VALUES
(
@accountId,
'Balance change for account: ' + CAST(@accountId AS VARCHAR(20)),
'On ' + CONVERT(VARCHAR(30), GETDATE(),103) + ' your balance was changed from ' + CAST(@oldSum AS VARCHAR(20)) + ' to ' + CAST(@newSum AS VARCHAR(20))
)

SELECT * FROM Accounts WHERE Id = 1
SELECT * FROM Logs
SELECT * FROM NotificationEmails
UPDATE Accounts
SET Balance += 100
WHERE Id = 1

GO

--16. Deposit Money

CREATE PROCEDURE usp_DepositMoney (@accountId INT , @moneyAmount DECIMAL(15,4)) 
AS
BEGIN TRANSACTION

DECLARE @account INT = (SELECT Id FROM Accounts WHERE Id = @accountId)

IF (@account IS NULL)
BEGIN
	ROLLBACK
	RAISERROR('Invalid account id',16,1)
	RETURN
END

IF(@moneyAmount < 0)
BEGIN
	ROLLBACK
	RAISERROR('Negative amount',16,1)
	RETURN
END

UPDATE Accounts
SET Balance += @moneyAmount
WHERE Id = @accountId
COMMIT

EXEC usp_DepositMoney 1,247.78
SELECT * FROM Accounts WHERE Id = 1

GO
--17. Withdraw Money Procedure

CREATE PROCEDURE usp_WithdrawMoney  (@accountId INT , @moneyAmount DECIMAL(15,4)) 
AS
BEGIN TRANSACTION

DECLARE @account INT = (SELECT Id FROM Accounts WHERE Id = @accountId)
DECLARE @accountBalance DECIMAL(15,4) = (SELECT Balance FROM Accounts WHERE Id = @accountId)

IF (@account IS NULL)
BEGIN
	ROLLBACK
	RAISERROR('Invalid account id',16,1)
	RETURN
END

IF(@moneyAmount < 0)
BEGIN
	ROLLBACK
	RAISERROR('Negative amount',16,1)
	RETURN
END
IF(@accountBalance - @moneyAmount < 0)
BEGIN
	ROLLBACK
	RAISERROR('Insufficient funds',16,1)
	RETURN
END

UPDATE Accounts
SET Balance -= @moneyAmount
WHERE Id = @accountId
COMMIT

EXEC usp_WithdrawMoney 1,600
SELECT * FROM Accounts WHERE Id = 1

GO

--18. Money Transfer

CREATE PROCEDURE usp_TransferMoney  (@senderId INT , @receiverId INT ,@amount DECIMAL(15,4)) 
AS
BEGIN TRANSACTION
EXEC usp_WithdrawMoney @senderId,@amount
EXEC usp_DepositMoney @receiverId,@amount
COMMIT

EXEC usp_TransferMoney 1,2,100
SELECT * FROM Accounts WHERE Id = 1 OR Id = 2

GO

--19. Trigger

SELECT *
	FROM Users AS u
	JOIN UsersGames AS ug ON ug.UserId = u.Id
WHERE UG.Id = 38

	SELECT * 
		FROM Items
	WHERE Id = 2

SELECT *
	FROM UserGameItems
	WHERE UserGameId = 38 AND ItemId = 14

	INSERT INTO UserGameItems (ItemId,UserGameId) VALUES
	(14,38)

GO

CREATE TRIGGER tr_RestrictItems ON UserGameItems INSTEAD OF INSERT
AS
DECLARE @itemId INT = (SELECT ItemId FROM inserted)
DECLARE @userGameId INT = (SELECT UserGameId FROM inserted)
DECLARE @itemLevel INT = (SELECT MinLevel FROM Items WHERE iD = @itemId)
DECLARE @userGameLevel INT = (SELECT Level FROM UsersGames WHERE Id = @userGameId)
IF (@userGameLevel >= @itemLevel)
BEGIN
	INSERT INTO UserGameItems (ItemId,UserGameId) VALUES
	(@itemId,@userGameId)
END

SELECT *
	FROM Users AS u
	JOIN UsersGames AS ug ON ug.UserId = u.Id
	JOIN Games AS g ON g.Id = ug.GameId
WHERE g.Name = 'Bali' AND u.Username IN ('baleremuda', 'loosenoise', 'inguinalself','buildingdeltoid', 'monoxidecos' )

UPDATE UsersGames
SET Cash += 50000
WHERE GameId = (SELECT Id FROM Games WHERE Name = 'Bali') AND UserId IN (SELECT Id FROM Users WHERE Username IN('baleremuda', 'loosenoise', 'inguinalself','buildingdeltoid', 'monoxidecos'))

GO

DECLARE @itemId INT = 251

WHILE(@itemId <= 299)
BEGIN

	EXECUTE usp_BuyItem 22,@itemId,212
	EXECUTE usp_BuyItem 37,@itemId,212
	EXECUTE usp_BuyItem 52,@itemId,212
	EXECUTE usp_BuyItem 61,@itemId,212

	SET @itemId += 1;
END

SELECT * FROM UsersGames WHERE GameId = 212

DECLARE @counter INT = 501

WHILE(@counter <= 539)
BEGIN

	EXECUTE usp_BuyItem 22,@counter,212
	EXECUTE usp_BuyItem 37,@counter,212
	EXECUTE usp_BuyItem 52,@counter,212
	EXECUTE usp_BuyItem 61,@counter,212

	SET @counter += 1;
END

GO

CREATE PROCEDURE usp_BuyItem @userId INT, @itemId INT,@gameId INT
AS
BEGIN TRANSACTION
DECLARE @user INT = (SELECT Id FROM Users WHERE Id = @userId)
DECLARE @item INT = (SELECT Id FROM Items WHERE Id = @itemId)

IF (@user IS NULL OR @item IS NULL)
BEGIN
	ROLLBACK
	RAISERROR('Invalid user or item id!',16,1)
	RETURN
END

DECLARE @userCash DECIMAL(15,2) = (SELECT Cash FROM UsersGames WHERE UserId = @userId AND GameId = @gameId)
DECLARE @itemPrice DECIMAL (15,2) = (SELECT Price FROM Items WHERE Id = @itemId)

IF (@userCash - @itemPrice < 0)
BEGIN
	ROLLBACK
	RAISERROR('Insufficient funds!',16,2)
	RETURN
END

UPDATE UsersGames
SET Cash -= @itemPrice
WHERE UserId = @userId AND GameId = @gameId

DECLARE @userGameId DECIMAL(15,2) = (SELECT Id FROM UsersGames WHERE UserId = @userId AND GameId = @gameId)

INSERT INTO UserGameItems(ItemId,UserGameId) VALUES (@itemId,@userGameId)
COMMIT


SELECT u.Username,g.Name,ug.Cash,i.Name
	FROM Users AS u
	JOIN UsersGames AS ug ON ug.UserId = u.Id
	JOIN Games AS g ON g.Id = ug.GameId
	JOIN UserGameItems AS ugi ON  ugi.UserGameId = ug.Id
	JOIN Items AS i ON i.Id = ugi.ItemId
	WHERE g.Name = 'Bali'
ORDER BY u.Username,i.Name

GO
--20. *Massive Shopping



DECLARE @userGameId INT = (SELECT Id FROM UsersGames WHERE UserId = 9 AND GameId = 87)

DECLARE @stamatCash DECIMAL(15,2) = (SELECT Cash FROM UsersGames WHERE UserId = 9 AND GameId = 87)
DECLARE @itemsPrice DECIMAL(15,2) = (SELECT SUM(Price) AS TotalPrice FROM Items WHERE MinLevel BETWEEN 11 AND 12 )

IF(@stamatCash >= @itemsPrice)
BEGIN
	BEGIN TRANSACTION
	UPDATE UsersGames
	SET Cash -= @itemsPrice
	WHERE Id = @userGameId
	INSERT INTO UserGameItems(ItemId,UserGameId)
	SELECT Id,@userGameId from Items WHERE MinLevel BETWEEN 11 AND 12
	COMMIT
END

SET @stamatCash  = (SELECT Cash FROM UsersGames WHERE UserId = 9 AND GameId = 87)
SET @itemsPrice = (SELECT SUM(Price) AS TotalPrice FROM Items WHERE MinLevel BETWEEN 11 AND 12 )

IF(@stamatCash >= @itemsPrice)
BEGIN
	BEGIN TRANSACTION
	UPDATE UsersGames
	SET Cash -= @itemsPrice
	WHERE Id = @userGameId
	INSERT INTO UserGameItems(ItemId,UserGameId)
	SELECT Id,@userGameId from Items WHERE MinLevel BETWEEN 11 AND 12
	COMMIT
END
SELECT i.Name
	FROM Users AS u
	JOIN UsersGames AS ug ON ug.UserId = u.Id
	JOIN Games AS g ON g.Id = ug.GameId
	JOIN UserGameItems AS ugi ON  ugi.UserGameId = ug.Id
	JOIN Items AS i ON i.Id = ugi.ItemId
	WHERE u.Username = 'Stamat' AND g.Name = 'Safflower'
ORDER BY i.Name

GO
--21. Employees with Three Projects

CREATE PROC usp_AssignProject(@emloyeeId INT, @projectId INt)
AS
BEGIN TRANSACTION
DECLARE @employee INT = (SELECT EmployeeId FROM Employees WHERE EmployeeID = @emloyeeId)
DECLARE @project INT = (SELECT ProjectId FROM Projects WHERE ProjectID = @projectId)

IF(@emloyeeId IS NULL OR @project IS NULL)
BEGIN
	ROLLBACK
	RAISERROR('Invalid employees id or project id!',16,1)
	RETURN
END

DECLARE @employeeProjects INT = (SELECT COUNT(*) FROM EmployeesProjects WHERE EmployeeID = @emloyeeId)

IF(@employeeProjects >= 3)
BEGIN
	ROLLBACK
	RAISERROR('The employee has too many projects!',16,2)
	RETURN
END

INSERT INTO EmployeesProjects(EmployeeID,ProjectID) VALUES (@emloyeeId,@projectId)

COMMIT


--22. Delete Employees

CREATE TABLE Deleted_Employees
(
	EmployeeId INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	MiddleName VARCHAR(50),
	JobTitle VARCHAR(50) NOT NULL,
	DepartmentID INT NOT NULL,
	Salary DECIMAL(15,2) NOT NULL
)
GO
CREATE TRIGGER tr_DeleteEmployees ON Employees FOR DELETE 
AS
INSERT INTO Deleted_Employees(FirstName,LastName,Middlename,JobTitle,DepartmentId,Salary)
	SELECT  FirstName, LastName, MiddleName, JobTitle, DepartmentID, Salary FROM deleted

