CREATE TABLE Region
(
	RegionID int IDENTITY(1,1) PRIMARY KEY,
	RegionName varchar(30) NOT NULL
);

CREATE TABLE Customer
(
	cID int PRIMARY KEY,
	fName varchar(30) NOT NULL,
	lName varchar(30) NOT NULL,
	cAddress varchar(60) NOT NULL,
	Phone varchar(10),
	Mobile varchar(10),
	Fax varchar(10),
	Email varchar(30),
	RegionID int FOREIGN KEY REFERENCES Region(RegionID)
);

CREATE TABLE ProjStatus
(
	psID int IDENTITY(1,1) PRIMARY KEY,
	psName varchar(30) NOT NULL
);

CREATE TABLE Project
(
	pID int IDENTITY(1,1) PRIMARY KEY,
	pName varchar(30) NOT NULL,
	Comments varchar(255),
	DateOpened date NOT NULL,
	ExpirationDate date,
	InstallationDate date, --Used also for Back-To-Customer Date
	Cost float NOT NULL,
	ContractorName varchar(30),
	ContractorPhone varchar(10),
	ArchitectName varchar(30),
	ArchitectPhone varchar(10),
	SupervisorName varchar(30),
	SupervisorPhone varchar(10),
	HatchesImageURL varchar(200),
	cID int FOREIGN KEY REFERENCES Customer(cID),
	psID int FOREIGN KEY REFERENCES ProjStatus(psID)
);

CREATE TABLE Employee
(
	eID int PRIMARY KEY,
	eName varchar(30) NOT NULL,
	eRole varchar(30) NOT NULL,
	eUsername varchar(30) NOT NULL,
	Phone varchar(10),
	Mobile varchar(10)
);

CREATE TABLE ServiceCall
(
	scID int IDENTITY(1,1) PRIMARY KEY,
	ProblemDesc varchar(200) NOT NULL,
	Urgent bit NOT NULL,
	DateOpened date NOT NULL,
	DateClosed date,
	cID int FOREIGN KEY REFERENCES Customer(cID),
	pID int FOREIGN KEY REFERENCES Project(pID),
);

CREATE TABLE Supplier
(
	SupplierID int IDENTITY(1,1) PRIMARY KEY,
	sName varchar(30) NOT NULL,
	--City varchar(30) NOT NULL,
	sAddress varchar(60) NOT NULL,
	Phone varchar(10) NOT NULL,
	Mobile varchar(10),
	Fax varchar(10),
	Email varchar(30),
	IsActive bit NOT NULL
);

CREATE TABLE RawMaterials
(
	rmID int IDENTITY(1,1) PRIMARY KEY,
	rName varchar(30) NOT NULL
);

CREATE TABLE SupplierRawMaterials
(
	SupplierID int FOREIGN KEY REFERENCES Supplier(SupplierID),
	rmID int FOREIGN KEY REFERENCES RawMaterials(rmID),
	CONSTRAINT PK_SupplierRawMaterials PRIMARY KEY (SupplierID,rmID)
);

CREATE TABLE OrderStatus
(
	osID int IDENTITY(1,1) PRIMARY KEY,
	osName varchar(20) NOT NULL
);

CREATE TABLE Orders
(
	oID int IDENTITY(1,1) PRIMARY KEY,
	DateOpened date NOT NULL,
	EstimatedDateOfArrival date,
	DateOfArrival date,
	Quantity float,
	osID int FOREIGN KEY REFERENCES OrderStatus(osID),
	pID int FOREIGN KEY REFERENCES Project(pID),
	SupplierID int FOREIGN KEY REFERENCES Supplier(SupplierID),
	rmID int FOREIGN KEY REFERENCES RawMaterials(rmID)
);

CREATE TABLE HatchType
(
	htID int IDENTITY(1,1) PRIMARY KEY,
	htName varchar(20) NOT NULL
);

CREATE TABLE HatchStatus
(
	hsID int IDENTITY(1,1) PRIMARY KEY,
	hsName varchar(20) NOT NULL
);

CREATE TABLE FailureType
(
	ftID int IDENTITY(1,1) PRIMARY KEY,
	ftName varchar(30) NOT NULL
);

--CREATE TABLE Failure
--(
--	fID int IDENTITY(1,1) PRIMARY KEY,
--	--ftID int FOREIGN KEY REFERENCES FailureType(ftID),
--	FailureDesc varchar(100)
--);

CREATE TABLE Hatch
(
	hID int IDENTITY(1,1) PRIMARY KEY,
	hsID int FOREIGN KEY REFERENCES HatchStatus(hsID),
	htID int FOREIGN KEY REFERENCES HatchType(htID),
	pID int FOREIGN KEY REFERENCES Project(pID),
	--fID int FOREIGN KEY REFERENCES Failure(fID),
	ftID int FOREIGN KEY REFERENCES FailureType(ftID),
	eID int FOREIGN KEY REFERENCES Employee(eID),
	StatusLastModified date,
	Comments varchar(200),
	IsActive bit NOT NULL
);

CREATE TABLE QAQuestion
(
	qID int IDENTITY(1,1) PRIMARY KEY,
	qQuestion varchar(50) NOT NULL
);

CREATE TABLE QA
(
	hID int FOREIGN KEY REFERENCES Hatch(hID),
	qID int FOREIGN KEY REFERENCES QAQuestion(qID),
	Checked bit NOT NULL,
	UnDamaged bit NOT NULL,
	CONSTRAINT PK_QA PRIMARY KEY (hID,qID)
);

CREATE TABLE Picture
(
	picID int IDENTITY(1,1) PRIMARY KEY,
	picDesc varchar(100),
	DateTaken date,
	ImageURL varchar(200) NOT NULL,
	hID int FOREIGN KEY REFERENCES Hatch(hID)
);

CREATE TABLE Pin
(
	pinID int IDENTITY(1,1) PRIMARY KEY,
	CoordinateX float NOT NULL,
	CoordinateY float NOT NULL,
	PinComment varchar(100),
	AudioURL varchar(200),
	VideoURL varchar(200),
	picID int FOREIGN KEY REFERENCES Picture(picID)
);

CREATE TABLE Notifications
(
	nID int IDENTITY(1,1) PRIMARY KEY,
	[Notification] varchar(255) NOT NULL,
	nDate date NOT NULL,
	eID1 int NOT NULL,
	eID2 int
);

CREATE TABLE SpecialNotifications
(
	nID int IDENTITY(1,1) PRIMARY KEY,
	[Notification] varchar(255),
	nDate date NOT NULL,
	nType varchar(50) NOT NULL,
	EmailSubject varchar(100),
	EmailMessage varchar(500),
	EmailAddress varchar(50),
	eID1 int NOT NULL,
	eID2 int
);

CREATE TABLE Files
(
	fID int IDENTITY(1,1) PRIMARY KEY,
	[Description] varchar(100),
	URL varchar(255) NOT NULL,
	pID int FOREIGN KEY REFERENCES Project(pID)
);