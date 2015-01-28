USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetFriends]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetFriends]
		@AccountID		int
AS
BEGIN
	SELECT	Accounts.AccountID
	,		Accounts.Username
	,		Accounts.UserStatusID
	FROM	Accounts
	WHERE	Accounts.AccountID
	IN(
			SELECT	Friends.FriendID
			FROM	Friends
			WHERE	Friends.AccountID	=	@AccountID
	)
END

GO
