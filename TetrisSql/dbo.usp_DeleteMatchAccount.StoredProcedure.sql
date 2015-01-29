USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteMatchAccount]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_DeleteMatchAccount]
	@MatchID	int,
	@AccountID	int
AS
BEGIN
	DECLARE @BoardID				int

	UPDATE	Accounts	
	SET		Accounts.UserStatusID		=	2	--ONLINE
	WHERE	Accounts.AccountID			=	@AccountID
	AND		Accounts.CurrentMatchID		=	@MatchID
	AND		Accounts.LastUserStatusID	!=	4	--HIDDEN
	
	UPDATE	Accounts
	SET		Accounts.CurrentMatchID	=	NULL
	WHERE	Accounts.AccountID		=	@AccountID
	AND		Accounts.CurrentMatchID	=	@MatchID

	SELECT	@BoardID				=	MatchAccounts.BoardID
	FROM	MatchAccounts
	WHERE	MatchAccounts.AccountID	=	@AccountID
	AND		MatchAccounts.MatchID	=	@MatchID

	DELETE	BoardData
	WHERE	BoardData.BoardID		=	@BoardID
	
	DELETE	Boards
	WHERE	Boards.BoardID			=	@BoardID
	
	DELETE	MatchAccounts
	WHERE	MatchAccounts.BoardID	=	@BoardID
	AND		MatchAccounts.MatchID	=	@MatchID
	AND		MatchAccounts.AccountID	=	@AccountID


END

GO
