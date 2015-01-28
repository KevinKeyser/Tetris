USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_Login]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Login]
	@Username	varchar(50),
	@Password	varchar(100)
AS
BEGIN
	DECLARE	@UserID			int
	DECLARE @UserStatusID	int

	SELECT	@UserID				=	Accounts.AccountID
	,		@UserStatusID		=	Accounts.UserStatusID
	FROM	Accounts
	WHERE	Accounts.Username	=	@Username
	AND		Accounts.Password	=	@Password
	--TODO
	--Check Status of user
	IF(@@ROWCOUNT > 0)
	BEGIN
		IF(@UserStatusID != 1)
			BEGIN
				RAISERROR ('User is currently online', 14, 1)
			END
			UPDATE	Accounts
			SET		Accounts.LastLoggedIn	=	GETDATE()
			WHERE	Accounts.AccountID		=	@UserID

			UPDATE	Accounts
			SET		Accounts.UserStatusID	=	2
			WHERE	Accounts.AccountID		=	@UserID
			AND		Accounts.LastUserStatusID !=	4		--HIDDEN

			SELECT	AccountID
			,		ExternalAccountID
			FROM	Accounts
			WHERE	AccountID				=	@UserID
	END
	ELSE
	BEGIN
		SELECT 'User Doesnt Exist' AS Error
	END
END
GO
