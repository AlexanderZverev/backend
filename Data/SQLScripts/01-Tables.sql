DROP TABLE IF EXISTS dbo.Message;  
GO

DROP TABLE IF EXISTS dbo.Contact;  
GO 

DROP TABLE IF EXISTS dbo.[User];  
GO

CREATE TABLE dbo.[User](
	Id int IDENTITY(1,1) NOT NULL,
	Name nvarchar(100) NOT NULL,
	Password nvarchar(128) NOT NULL,
	State bit NOT NULL,
	CONSTRAINT PK_USER PRIMARY KEY CLUSTERED 
	(
	Id ASC
	)
)
GO

DROP TABLE IF EXISTS dbo.Contact;  
GO 
CREATE TABLE dbo.Contact(
	UserId int NOT NULL,
	ContactId int NOT NULL,
	LastUpdateTime datetime2(7) NOT NULL,
	CONSTRAINT FK_Contact_usrid FOREIGN KEY (UserId) REFERENCES dbo.[User](Id),
	CONSTRAINT FK_Contact_contactid FOREIGN KEY (ContactId) REFERENCES dbo.[User](Id),
	CONSTRAINT PK_Contact PRIMARY KEY (UserId, ContactId),
	CHECK (UserId != ContactId)
) 
GO

DROP TABLE IF EXISTS dbo.Message;  
GO 
CREATE TABLE dbo.Message(
	Id int IDENTITY(1,1) NOT NULL,
	UserId int NOT NULL,
	ContactId int NOT NULL,
	SendTime datetime2(7) NOT NULL,
	DeliveryTime datetime2(7) NOT NULL,
	Content nvarchar(max) NOT NULL,
	CONSTRAINT PK_Message PRIMARY KEY CLUSTERED(Id ASC),
	CONSTRAINT FK_Message_usrid FOREIGN KEY (UserId) REFERENCES dbo.[User](Id),
	CONSTRAINT FK_Message_contactid FOREIGN KEY (ContactId) REFERENCES dbo.[User](Id),
) 
GO