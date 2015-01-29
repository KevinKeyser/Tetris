USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteMatch]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_DeleteMatch]
	@MatchID	int
AS
BEGIN
	DELETE	Boards
	WHERE	BoardID	IN
	(
		SELECT	BoardID
		FROM	MatchAccounts
		WHERE	MatchID			=	@MatchID
	)

	DELETE	MatchAccounts
	WHERE	MatchAccounts.MatchID	=	@MatchID

	DELETE	Matches
	WHERE	Matches.MatchID			=	@MatchID
END



GO
