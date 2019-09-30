--Table Relations


CREATE DATABASE MyDemoDB
USE MyDemoDB


/*--01--*/
/*--01. One-To-One Relationship--*/
CREATE TABLE Persons(
	PersonID INT PRIMARY KEY,
	FirstName VARCHAR(20) NOT NULL,
	Salary DECIMAL(15,2),
	PassportID INT NOT NULL
)

CREATE TABLE Passports(
	PassportID INT PRIMARY KEY,
	PassportNumber CHAR(8) NOT NULL
)

ALTER TABLE Persons
   ADD CONSTRAINT FK_Passports_Persons
   FOREIGN KEY (PassportID) 
   REFERENCES Passports(passportID)


ALTER TABLE Persons
ADD UNIQUE (PassportID)

ALTER TABLE Passports
ADD UNIQUE(PassportID)

INSERT INTO Passports VALUES
(101,'N34FG21B'),
(102,'K65LO4R7'),
(103,'ZE657QP2')

INSERT INTO Persons VALUES
(1, 'Roberto', 43300.00	,102),
(2,	'Tom',56100.00,103),
(3,	'Yana',	60200.00,101)

/*--02--*/
/*--02. One-To-Many Relationship--*/

CREATE TABLE Manufacturers(
	MunufacturerID INT PRIMARY KEY IDENTITY,
	Name VARCHAR(15) NOT NULL,
	EstablishedOn DATE NOT NULL
)

CREATE TABLE Models(
	ModelID INT PRIMARY KEY IDENTITY(101,1),
	Name VARCHAR(15) NOT NULL,
	ManufacturerID INT FOREIGN KEY REFERENCES Manufacturers(MunufacturerID)
)
INSERT INTO Manufacturers(Name,EstablishedOn) VALUES
('BMW',	'07/03/1916'),
('Tesla',	'01/01/2003'),
('Lada',	'01/05/1966')

INSERT INTO Models(Name,ManufacturerID) VALUES
('X1',	1),
('i6',	1),
('Model S',	2),
('Model X',	2),
('Model 3',	2),
('Nova',	3)

/*--03--*/
/*--03. Many-To-Many Relationship--*/
CREATE TABLE Students(
	StudentID INT,
	Name VARCHAR(20)
)
CREATE TABLE Exams(
	ExamID INT,
	Name VARCHAR(20)
)
CREATE TABLE StudentsExams(
	StudentID INT,
	ExamID INT
)
ALTER TABLE StudentsExams
ALTER COLUMN StudentID INT NOT NULL

ALTER TABLE StudentsExams
ALTER COLUMN ExamID INT NOT NULL

ALTER TABLE Students
ALTER COLUMN StudentID INT NOT NULL

ALTER TABLE Students
ADD CONSTRAINT PK_Students PRIMARY KEY (StudentID)

ALTER TABLE Exams
ALTER COLUMN ExamID INT NOT NULL

ALTER TABLE Exams
ADD CONSTRAINT PK_Exams PRIMARY KEY (ExamID)
ALTER TABLE StudentsExams
ADD CONSTRAINT PK_StudentsExams PRIMARY KEY (StudentID,ExamID)

ALTER TABLE StudentsExams 
ADD CONSTRAINT FK_StudentsExams_Students FOREIGN KEY
	(StudentID) REFERENCES Students(StudentID),
	CONSTRAINT FK_StudentsExams_Exams FOREIGN KEY
	(ExamID) REFERENCES Exams(ExamID)

/*--04--*/
/*--04. Self-Referencing--*/
CREATE TABLE Teachers(
	TeacherID INT PRIMARY KEY IDENTITY(101,1),
	[Name] VARCHAR(30),
	ManagerID INT FOREIGN KEY REFERENCES Teachers(TeacherID)
)

INSERT INTO Teachers([Name],ManagerID) VALUES
('John',	NULL),
('Maya',	106	),
('Silvia',	106	),
('Ted',		105	),
('Mark',	101	),
('Greta',	101)

/*--05--*/
/*--05. Online Store Database--*/

CREATE TABLE Cities(
	CityID  INT NOT NULL,
	[Name] varchar(50) NOT NULL
)
CREATE TABLE Customers(
	CustomerID INT NOT NULL,
	[Name] varchar(50) NOT NULL,
	Birthday DATE NOT NULL,
	CityID INT NOT NULL
)
CREATE TABLE Orders(
	OrderID INT NOT NULL,
	CustomerID INT NOT NULL
)
CREATE TABLE ItemTypes(
	ItemTypeID INT NOT NULL,
	[Name] varchar(50) NOT NULL
)
CREATE TABLE Items(
	ItemID INT NOT NULL,
	[Name] varchar(50) NOT NULL,
	ItemTypeID INT NOT NULL
)
CREATE TABLE OrderItems(
	OrderID INT NOT NULL,
	ItemID INT NOT NULL
)

ALTER TABLE Cities
ADD CONSTRAINT PK_Cities PRIMARY KEY(CityID),
	CONSTRAINT CHk_CityName CHECK (LEN([Name]) > 2)

ALTER TABLE Customers
ADD CONSTRAINT PK_Customers PRIMARY KEY(CustomerID),
	CONSTRAINT CHK_CustomerName CHECK (LEN([Name]) > 2),
	CONSTRAINT FK_Customeras_Cities FOREIGN KEY (CityID) REFERENCES Cities(CityID)
ALTER TABLE Orders
ADD CONSTRAINT PK_Orders PRIMARY KEY (OrderID),
	CONSTRAINT FK_Orders_Customers FOREIGN KEY(CustomerID) REFERENCES Customers(CustomerID)


ALTER TABLE ItemTypes
ADD CONSTRAINT PK_ItemTypes PRIMARY KEY (ItemTypeID),
	CONSTRAINT CHK_ItemTypeyName CHECK (LEN([Name]) > 2)

ALTER TABLE Items
ADD CONSTRAINT PK_Items PRIMARY KEY  (ItemID),
	CONSTRAINT CHK_ItemName CHECK (LEN([Name]) > 2),
	CONSTRAINT FK_Items_ItemTypes FOREIGN KEY(ItemTypeID) REFERENCES ItemTypes(ItemTypeID)

ALTER TABLE OrderItems
ADD CONSTRAINT FK_OrderItems_Orders FOREIGN KEY(OrderID)			REFERENCES Orders(OrderID),
	CONSTRAINT FK_OrderItems_Items FOREIGN KEY (ItemID) REFERENCES Items(ItemID),
	CONSTRAINT PK_OrderItems PRIMARY KEY(OrderID,ItemID)

/*--06--*/
/*--06. University Database--*/
CREATE TABLE Majors(
	MajorID INT CONSTRAINT PK_Majors PRIMARY KEY,
	[Name] VARCHAR(30) NOT NULL CONSTRAINT CHK_MajorName CHECK(LEN([Name]) > 2)
)
CREATE TABLE Students(
	StudentID INT CONSTRAINT PK_Students PRIMARY KEY,
	StudentNumber CHAR(10) NOT NULL,
	StudentName VARCHAR(30) NOT NULL CONSTRAINT CHK_StudentName CHECK(LEN(StudentName) > 2),
	MajorID INT CONSTRAINT FK_Students_Majors FOREIGN KEY REFERENCES Majors(MajorID)
)
CREATE TABLE Subjects(
	SubjectID INT CONSTRAINT PK_Subjects PRIMARY KEY,
	SubjectName VARCHAR(40) NOT NULL
)
CREATE TABLE Agenda(
	StudentID INT CONSTRAINT FK_Agenda_Students FOREIGN KEY REFERENCES Students(StudentID),
	SubjectID INT CONSTRAINT FK_Agenda_Subjects FOREIGN KEY REFERENCES Subjects(SubjectID)

	CONSTRAINT PK_Agenda PRIMARY KEY (StudentID,SubjectID)
)
CREATE TABLE Payments(
	PaymentID INT CONSTRAINT PK_Payments PRIMARY KEY,
	PaymentDate DATETIME NOT NULL,
	PaymentAmount DECIMAL(15,2),
	StudentID INT CONSTRAINT FK_Payments_Students FOREIGN KEY REFERENCES Students(StudentID)
)
/*--09--*/
/*--09. *Peaks in Rila--*/
USE Geography
SELECT m.MountainRange,p.PeakName,p.Elevation
FROM Mountains AS m
JOIN Peaks AS p
ON p.MountainID = m.Id
WHERE MountainId = 17
ORDER BY Elevation DESC,PeakName
