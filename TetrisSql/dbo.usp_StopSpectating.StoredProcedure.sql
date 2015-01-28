USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_StopSpectating]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE	[dbo].[usp_StopSpectating]
	@MatchID	int,
	@AccountID	int
AS
BEGIN
	DECLARE	@SpectatorID								int
	SELECT	@SpectatorID							=	MatchSpectatorTimeSheet.SpectatorID
	FROM	MatchSpectatorTimeSheet
	WHERE	MatchSpectatorTimeSheet.TimeEnded		IS	NULL
	AND		MatchSpectatorTimeSheet.SpectatorID
	IN(
		SELECT	MatchSpectators.SpectatorID
		FROM	MatchSpectators
		WHERE	MatchSpectators.AccountID			=	@AccountID
		AND		MatchSpectators.MatchID				=	@MatchID
	)
	IF	(@SpectatorID	!=	NULL)
	BEGIN
		UPDATE	MatchSpectatorTimeSheet
		SET		MatchSpectatorTimeSheet.TimeEnded	=	GETDATE()
		WHERE	MatchSpectatorTimeSheet.SpectatorID	=	@SpectatorID

		UPDATE	Accounts
		SET		Accounts.UserStatusID				=	2	--ONLINE
		WHERE	Accounts.AccountID					=	@AccountID
		AND		Accounts.UserStatusID				!=	4	--HIDDEN
	END
END

GO
