USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetSpectators]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetSpectators]
	@MatchID	int
AS
BEGIN
	SELECT	MatchSpectators.SpectatorID
	,		MatchSpectators.MatchID
	,		MatchSpectators.AccountID
	,		MatchSpectatorTimeSheet.TimeStarted
	,		MatchSpectatorTimeSheet.TimeEnded
	FROM	MatchSpectators
	JOIN	MatchSpectatorTimeSheet
	ON		MatchSpectators.SpectatorID		=	MatchSpectatorTimeSheet.SpectatorID
	WHERE	MatchSpectators.MatchID			=	@MatchID
END

GO
