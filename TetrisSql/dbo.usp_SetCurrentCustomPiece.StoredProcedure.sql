USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_SetCurrentCustomPiece]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_SetCurrentCustomPiece]
	@AccountID		int,
	@CustomPieceID	int
AS
BEGIN
	UPDATE	Accounts
	SET		Accounts.CustomPieceID			=		@CustomPieceID
	WHERE	Accounts.AccountID				=		@AccountID
	IF	EXISTS(SELECT	Accounts.AccountID
		FROM	Accounts
		WHERE	Accounts.AccountID			=		@AccountID
		AND		Accounts.CurrentMatchID		IS NOT	NULL)
		BEGIN
			UPDATE	MatchAccounts
			SET		MatchAccounts.CustomPieceID		=		@CustomPieceID
			WHERE	MatchAccounts.AccountID			=		@AccountID
		END
END


GO
