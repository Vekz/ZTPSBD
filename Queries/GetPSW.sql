DROP PROCEDURE [GetUserPSWD]
Go

CREATE PROCEDURE [dbo].[GetUserPSWD]
	@usrname nchar(20)
AS
	Select password FROM "User" Where login = @usrname;