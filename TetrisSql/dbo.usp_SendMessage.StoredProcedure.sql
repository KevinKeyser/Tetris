USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_SendMessage]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_SendMessage]
	@AccountID		int,
	@ChatGroupID	int,
	@Message		nvarchar(MAX)
AS
BEGIN
	IF EXISTS	(SELECT *
				FROM	Messages
				JOIN	ChatGroupAccessTimeSheet
				ON		Messages.ChatGroupID				=	ChatGroupAccessTimeSheet.ChatGroupID
				AND		Messages.AccountID					=	ChatGroupAccessTimeSheet.AccountID
				WHERE	ChatGroupAccessTimeSheet.RemovedTime!= NULL)
				BEGIN		
					INSERT	Messages
					VALUES	(@AccountID, @ChatGroupID, @Message, GETDATE(), NULL)
				END
END

GO
