USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_SetUserStatus]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE	[dbo].[usp_SetUserStatus]
	@AccountID		int,
	@UserStatusID	int
AS
BEGIN
	UPDATE	Accounts
	SET		Accounts.UserStatusID	=	@UserStatusID
	WHERE	Accounts.AccountID		=	@AccountID
END

GO
