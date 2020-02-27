SET IDENTITY_INSERT dbo.[User] ON 
GO
INSERT INTO dbo.[User](Id, Name, Password, State)
VALUES(1, 'Bob', 'TestPassword1',1)

INSERT INTO dbo.[User](Id, Name, Password, State)
VALUES(2, 'Tom1', 'TestPassword2', 0)

INSERT INTO dbo.[User](Id, Name, Password, State)
VALUES(3, 'Mark', 'TestPassword3', 0)

INSERT INTO dbo.[User](Id, Name, Password, State)
VALUES(4, 'Tom2', 'TestPassword4', 0)

GO
SET IDENTITY_INSERT dbo.[User] OFF
GO

SET IDENTITY_INSERT dbo.Message ON 
GO
INSERT INTO dbo.Message(Id, UserId, ContactId, SendTime, DeliveryTime, Content)
VALUES(1, 1, 2, '2019-05-18 16:18', '2019-05-18 16:19', 'Hi Tom!')

INSERT INTO dbo.Message(Id, UserId, ContactId, SendTime, DeliveryTime, Content)
VALUES(2, 2, 1, '2019-05-18 16:19', '2019-05-18 16:20', 'Hi Bob!')

INSERT INTO dbo.Message(Id, UserId, ContactId, SendTime, DeliveryTime, Content)
VALUES(3, 1, 3, '2019-05-18 16:19', '2019-05-18 16:25', 'Hi Mark!')

GO
SET IDENTITY_INSERT dbo.Message OFF 
GO

INSERT INTO dbo.Contact(UserId, ContactId, LastUpdateTime)
VALUES(1, 2, '2019-05-18 16:20')

INSERT INTO dbo.Contact(UserId, ContactId, LastUpdateTime)
VALUES(2, 1, '2019-05-18 16:20')

INSERT INTO dbo.Contact(UserId, ContactId, LastUpdateTime)
VALUES(1, 3, '2019-05-18 16:25')

GO
