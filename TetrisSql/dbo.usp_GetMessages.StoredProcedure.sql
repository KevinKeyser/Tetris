USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetMessages]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetMessages]
	@AccountID											int,
	@ChatGroupID										int,
	@LastTimeRecieved									datetime
AS
BEGIN
	SELECT	Messages.*
	FROM	Messages
	JOIN	ChatGroupAccessTimeSheet
	ON		Messages.AccountID						=	ChatGroupAccessTimeSheet.AccountID
	AND		Messages.ChatGroupID					=	ChatGroupAccessTimeSheet.ChatGroupID
	WHERE	ChatGroupAccessTimeSheet.AccountID		=	@AccountID
	AND		ChatGroupAccessTimeSheet.ChatGroupID	=	@ChatGroupID
	AND		Messages.TimeSent						>	@LastTimeRecieved
	AND		Messages.TimeSent						>=	ChatGroupAccessTimeSheet.TimeAdded
	AND		
	(
		ChatGroupAccessTimeSheet.RemovedTime		=	NULL
	OR	Messages.TimeSent							<=	ChatGroupAccessTimeSheet.RemovedTime
	)
END
GO
