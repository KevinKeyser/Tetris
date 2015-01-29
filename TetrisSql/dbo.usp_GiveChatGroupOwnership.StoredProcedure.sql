USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GiveChatGroupOwnership]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GiveChatGroupOwnership]
		@ChatGroupID		int,
		@AccountID			int
AS
BEGIN
	UPDATE	ChatGroups
	SET		ChatGroups.OwnerID		=	@AccountID
	WHERE	ChatGroups.ChatGroupID	=	@ChatGroupID
END

GO
