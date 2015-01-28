USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_SpectateMatch]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE	[dbo].[usp_SpectateMatch]
	@MatchID	int,
	@AccountID	int
AS
BEGIN
	DECLARE	@SpectatorID	int
	IF NOT EXISTS(SELECT MatchSpectators.SpectatorID
				FROM	MatchSpectators
				WHERE	MatchSpectators.AccountID		=	@AccountID
				AND		MatchSpectators.MatchID			=	@MatchID)
				BEGIN
					INSERT	MatchSpectators
					VALUES	(@MatchID, @AccountID)
					SET		@SpectatorID				=	@@IDENTITY
				END
	ELSE
				BEGIN
					SELECT	@SpectatorID				=	MatchSpectators.SpectatorID
					FROM	MatchSpectators
					WHERE	MatchSpectators.AccountID	=	@AccountID
					AND		MatchSpectators.MatchID		=	@MatchID
				END
	INSERT	MatchSpectatorTimeSheet
	VALUES	(@SpectatorID,	GETDATE(), NULL)

	UPDATE	Accounts
	SET		Accounts.UserStatusID	=	5			--Spectating
	WHERE	Accounts.AccountID		=	@AccountID
	AND		Accounts.UserStatusID	!=	4			--HIDDEN
END

GO
