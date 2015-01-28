USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteFriend]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_DeleteFriend]
	@AccountID	int,
	@FriendID	int
AS
BEGIN
	DELETE	Friends
	WHERE	Friends.AccountID	= @AccountID
	AND		Friends.FriendID	= @FriendID
END

GO
