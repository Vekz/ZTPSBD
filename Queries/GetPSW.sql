DROP PROCEDURE [GetUserPSWD]
Go
CREATE PROCEDURE [dbo].[GetUserPSWD]
	@usrname nchar(20)
AS
	Select password, User_Type FROM "User" Where login = @usrname 