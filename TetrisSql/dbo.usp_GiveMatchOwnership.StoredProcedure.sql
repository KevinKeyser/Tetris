USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GiveMatchOwnership]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GiveMatchOwnership]
		@MatchID			int,
		@AccountID			int
AS
BEGIN
	UPDATE	Matches
	SET		Matches.OwnerID			=	@AccountID
	WHERE	Matches.MatchID			=	@MatchID


	DECLARE	@ChatGroupID				int
	SELECT	@ChatGroupID			=	Matches.ChatGroupID
	FROM	Matches
	WHERE	Matches.MatchID			=	@MatchID


	UPDATE	ChatGroups
	SET		ChatGroups.OwnerID		=	@AccountID
	WHERE	ChatGroups.ChatGroupID	=	@ChatGroupID
END

GO
