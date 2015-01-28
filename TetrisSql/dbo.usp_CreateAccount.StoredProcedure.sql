USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateAccount]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_CreateAccount]
	@Username	varchar(50),
	@Password	varchar(100),
	@FirstName	varchar(50),
	@LastName	varchar(50),
	@Email		varchar(100),
	@Birthday	date
AS

BEGIN
	IF EXISTS(	SELECT	Accounts.AccountID
				FROM	Accounts
				WHERE	Accounts.Username = @Username)
				BEGIN
					RAISERROR ('Account already exists', 16, 1)
					RETURN
				END

	INSERT	Accounts
	VALUES	(NEWID(), @Username, @Password, @FirstName, @LastName, @Email, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, @Birthday, 1, 2, 0, GETDATE(), GETDATE(), NULL)

	SELECT	AccountID
	,		ExternalAccountID
	FROM	Accounts
	WHERE	AccountID			=	@@IDENTITY

END


GO
