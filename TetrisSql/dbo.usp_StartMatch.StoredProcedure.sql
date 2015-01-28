USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_StartMatch]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_StartMatch]
	@MatchID	int
AS
BEGIN
	UPDATE	Matches
	SET		Matches.TimeStarted				=	GETDATE()
	WHERE	Matches.MatchID					=	@MatchID

	UPDATE	Accounts
	SET		Accounts.UserStatusID			=	7	--InGame
	WHERE	Accounts.UserStatusID			!=	4	--HIDDEN
	AND		Accounts.AccountID			
	IN(
			SELECT	MatchAccounts.AccountID
			FROM	MatchAccounts
			WHERE	MatchAccounts.MatchID	=	@MatchID
	)
END

GO
