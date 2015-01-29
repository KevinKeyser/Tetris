USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteFriend]    Script Date: 1/29/2015 1:00:00 AM ******/
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
