USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_Logout]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Logout]
	@AccountID	int
AS
BEGIN
	Update	Accounts
	SET		Accounts.LastUserStatusID	=	Accounts.UserStatusID
	WHERE	Accounts.AccountID		=	@AccountID

	Update	Accounts
	SET		Accounts.UserStatusID	=	1
	WHERE	Accounts.AccountID		=	@AccountID
END

GO
