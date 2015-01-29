USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetBoardInfo]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetBoardInfo]
	@MatchID	int
AS
BEGIN
	SELECT	MatchAccounts.AccountID
	,		MatchAccounts.BoardID
	,		MatchAccounts.Ranking
	,		MatchAccounts.TimeFinished
	,		Matches.BoardWidth
	,		Matches.BoardHeight
	,		Boards.CurrentPieceID
	,		Boards.PieceX
	,		Boards.PieceY
	,		Boards.Rotation
	,		Boards.HeldPieceID
	,		Boards.Combo
	,		Boards.Multiplier
	,		Boards.Level
	,		Boards.Score
	,		Boards.PowerUp
	FROM	MatchAccounts
	JOIN	Boards
	ON		MatchAccounts.BoardID		=	Boards.BoardID
	JOIN	Matches
	ON		Matches.MatchID				=	MatchAccounts.MatchID
	WHERE	MatchAccounts.MatchID		=	@MatchID
END

GO
