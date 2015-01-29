USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateMatchRanking]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_UpdateMatchRanking]
	@MatchID	int,
	@AccountID	int
AS
BEGIN
	--Determine ranking
	DECLARE	@Ranking							int
	DECLARE	@Score								bigint
	DECLARE	@GameModeID							int
	SELECT	@Ranking						=	COUNT(MatchAccounts.AccountID) 
	,		@Score							=	Boards.Score
	,		@GameModeID						=	Matches.GameModeID
	FROM	MatchAccounts
	JOIN	Boards
	ON		MatchAccounts.BoardID			=	Boards.BoardID
	JOIN	Matches
	ON		MatchAccounts.MatchID			=	Matches.MatchID
	WHERE	MatchAccounts.MatchID			=	@MatchID
	AND		MatchAccounts.Ranking			IS	NULL
	GROUP BY	MatchAccounts.AccountID
	,			Boards.Score
	,			Matches.GameModeID

	IF(@@ROWCOUNT = 1)
		BEGIN
			UPDATE	MatchAccounts
			SET		MatchAccounts.Ranking			=	0
			,		MatchAccounts.TimeFinished		=	GETDATE()
			WHERE	MatchAccounts.MatchID			=	@MatchID
			AND		MatchAccounts.AccountID			=	@AccountID

			IF EXISTS	(SELECT	Hiscores.Score
						FROM	Hiscores
						WHERE	Hiscores.AccountID			=	@AccountID
						AND		Hiscores.GameModeID			=	@GameModeID)
						BEGIN
							UPDATE	Hiscores
							SET		Score					=	@Score
							WHERE	Hiscores.AccountID		=	@AccountID
							AND		Hiscores.GameModeID		=	@GameModeID
							AND		Hiscores.Score			<	@Score
						END
			ELSE
						BEGIN
							INSERT	Hiscores
							VALUES	(@AccountID, @GameModeID, @Score)
						END
		END
	ELSE
		BEGIN

		--Set the ranking for this AccountID
		UPDATE	MatchAccounts
		SET		MatchAccounts.Ranking				=	@Ranking
		,		MatchAccounts.TimeFinished			=	GETDATE()
		WHERE	MatchAccounts.MatchID				=	@MatchID
		AND		MatchAccounts.AccountID				=	@AccountID


		--Is there a winner?
		IF(@Ranking = 2)
		BEGIN
			UPDATE	MatchAccounts
			SET		MatchAccounts.Ranking			=	1
			,		MatchAccounts.TimeFinished		=	GETDATE()
			WHERE	MatchAccounts.MatchID			=	@MatchID
			AND		MatchAccounts.Ranking			IS	NULL

			UPDATE	Matches
			SET		Matches.TimeEnded				=	GETDATE()
			WHERE	Matches.MatchID					=	@MatchID

			UPDATE	Accounts
			SET		Accounts.UserStatusID			=	6 --InLobby
			WHERE	Accounts.AccountID
			IN(
					SELECT	MatchAccounts.AccountID
					FROM	MatchAccounts
					WHERE	MatchAccounts.MatchID	=	@MatchID
			)
		END
	--Return player's ranking
	SELECT	@Ranking	AS	PlayerRanking
	END
	RETURN
END
GO
