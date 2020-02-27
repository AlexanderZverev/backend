-- User related procedures.

IF OBJECT_ID('dbo.User_Get_ById', 'P') IS NOT NULL
   DROP PROC dbo.User_Get_ById;
GO
CREATE PROC dbo.User_Get_ById( @UserId int )
AS
BEGIN
	SET NOCOUNT ON

	SELECT Id, Name, Password, State
	FROM dbo.[User] 
	WHERE Id = @UserId
END
GO

IF OBJECT_ID('dbo.User_Get_ByName', 'P') IS NOT NULL
   DROP PROC dbo.User_Get_ByName;
GO
CREATE PROC dbo.User_Get_ByName( @UserName nvarchar(100) )
AS
BEGIN
	SET NOCOUNT ON

	SELECT TOP 1 Id, Name, Password, State
	FROM dbo.[User] 
	WHERE Name = @UserName
END
GO

IF OBJECT_ID('dbo.User_Search_ByName', 'P') IS NOT NULL
   DROP PROC dbo.User_Search_ByName;
GO
CREATE PROC dbo.User_Search_ByName( @UserName nvarchar(100) )
AS
BEGIN
	SET NOCOUNT ON

	SELECT Id, Name, Password, State
	FROM dbo.[User] 
	WHERE Name LIKE '%' + @UserName + '%'
END
GO

IF OBJECT_ID('dbo.User_Add', 'P') IS NOT NULL
   DROP PROC dbo.User_Add;
GO
CREATE PROC dbo.User_Add(@Name nvarchar(100), @Password nvarchar(128), @State bit )
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO dbo.[User] ( Name, Password, State )
	VALUES ( @Name, @Password, @State )

	SELECT SCOPE_IDENTITY() AS Id
	
END
GO

IF OBJECT_ID('dbo.User_Update', 'P') IS NOT NULL
   DROP PROC dbo.User_Update;
GO
CREATE PROC dbo.User_Update( 
	@UserId int, 
	@Name nvarchar(100) = NULL, 
	@Password nvarchar(128) = NULL,
	@State bit = NULL)
AS
BEGIN
	UPDATE dbo.[User] 
	SET Name = ISNULL(@Name, Name),
		Password = ISNULL(@PAssword, Password),
		State = ISNULL(@State, State)
	WHERE Id = @UserId
END
GO

IF OBJECT_ID('dbo.User_UpdateState', 'P') IS NOT NULL
   DROP PROC dbo.User_UpdateState;
GO
CREATE PROC dbo.User_UpdateState( 
	@UserId int, 
	@State bit)
AS
BEGIN
	 EXEC dbo.User_Update @UserId = @UserId, 
						  @Name=NULL, 
						  @Password = NULL, 
						  @State = @State
END
GO

-- Contact related procedures.

IF OBJECT_ID('dbo.Contact_GetUserContacts', 'P') IS NOT NULL
   DROP PROC dbo.Contact_GetUserContacts;
GO
CREATE PROC dbo.Contact_GetUserContacts( @UserId int, @ContactId int = NULL )
AS
BEGIN
	SET NOCOUNT ON

	SELECT UserId, ContactId, LastUpdateTime
	FROM dbo.Contact
	WHERE UserId = @UserId AND ContactId = ISNULL(@ContactId, ContactId)
END
GO

IF OBJECT_ID('dbo.Contact_SearchUserContacts', 'P') IS NOT NULL
   DROP PROC dbo.Contact_SearchUserContacts;
GO
CREATE PROC dbo.Contact_SearchUserContacts( @UserId int, @ContactName nvarchar(100))
AS
BEGIN
	SET NOCOUNT ON

	SELECT c.UserId, c.ContactId, c.LastUpdateTime
	FROM dbo.Contact c JOIN dbo.[User] u ON
		c.ContactId = u.Id
	WHERE c.UserId = @UserId AND u.Name = @ContactName
END
GO

IF OBJECT_ID('dbo.Contact_Add', 'P') IS NOT NULL
   DROP PROC dbo.Contact_Add;
GO
CREATE PROC dbo.Contact_Add(@UserId int, @ContactId int, @LastUpdateTime datetime2 )
AS
BEGIN
	INSERT INTO dbo.Contact ( UserId, ContactId, LastUpdateTime )
	VALUES ( @UserId, @ContactId, @LastUpdateTime )
			
END
GO

IF OBJECT_ID('dbo.Contact_Update', 'P') IS NOT NULL
   DROP PROC dbo.Contact_Update;
GO
CREATE PROC dbo.Contact_Update(@UserId int, @ContactId int, @LastUpdateTime datetime2 )
AS
BEGIN
	UPDATE dbo.Contact
	SET LastUpdateTime = @LastUpdateTime
	WHERE UserId = @UserId AND ContactId = @ContactId

	SELECT UserID, ContactId, LastUpdateTime
	FROM dbo.Contact
	WHERE UserId = @UserId AND ContactId = @ContactId
END
GO

IF OBJECT_ID('dbo.Contact_Delete', 'P') IS NOT NULL
   DROP PROC dbo.Contact_Delete;
GO
CREATE PROC dbo.Contact_Delete(@UserId int, @ContactId int)
AS
BEGIN
	DELETE 
	FROM dbo.Contact
	WHERE UserId = @UserId AND ContactId = @ContactId
END
GO

-- Message related procedures.

IF OBJECT_ID('dbo.Message_Get_ByUserId', 'P') IS NOT NULL
   DROP PROC dbo.Message_Get_ByUserId;
GO
CREATE PROC dbo.Message_Get_ByUserId( @UserId int )
AS
BEGIN
	SET NOCOUNT ON

	SELECT Id, UserId, ContactId, SendTime, DeliveryTime, Content
	FROM dbo.Message
	WHERE UserId = @UserId
END
GO

IF OBJECT_ID('dbo.Message_Search', 'P') IS NOT NULL
   DROP PROC dbo.Message_Search;
GO
CREATE PROC dbo.Message_Search( @UserId int, @ContactId int, @QueryString nvarchar(max) )
AS
BEGIN
	SET NOCOUNT ON

	SELECT Id, UserId, ContactId, SendTime, DeliveryTime, Content
	FROM dbo.Message
	WHERE UserId = @UserId AND ContactId = @ContactId AND Content LIKE '%'+@QueryString+'%'
END
GO

IF OBJECT_ID('dbo.Message_Add', 'P') IS NOT NULL
   DROP PROC dbo.Message_Add;
GO
CREATE PROC dbo.Message_Add(
	@UserId int, 
	@ContactId int, 
	@SendTime datetime2, 
	@DeliveryTime datetime2, 
	@Content nvarchar(max) 
)
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO dbo.Message ( UserId, ContactId, SendTime, DeliveryTime, Content )
	VALUES(@UserId, @ContactId, @SendTime, @DeliveryTime, @Content)

	SELECT SCOPE_IDENTITY() AS Id
END
GO