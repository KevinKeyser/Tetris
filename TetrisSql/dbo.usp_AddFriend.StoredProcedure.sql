USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddFriend]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_AddFriend]
	@AccountID	int,
	@FriendID	int
AS
BEGIN
	if(@AccountID = @FriendID)
	BEGIN
		RAISERROR ('Cannot Friend The Same ID', 15, 1)
		RETURN
	END

	INSERT Friends
	VALUES (@AccountID, @FriendID, GETDATE())
END

GO
