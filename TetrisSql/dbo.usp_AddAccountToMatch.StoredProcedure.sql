USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddAccountToMatch]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_AddAccountToMatch]
	@MatchID		int,
	@AccountID		int
AS
BEGIN
	IF NOT EXISTS(
				SELECT	MatchAccounts.MatchID
				,		MatchAccounts.AccountID
				FROM	MatchAccounts
				WHERE	MatchAccounts.MatchID	=	@MatchID
				AND		MatchAccounts.AccountID =	@AccountID
				)
				BEGIN
					INSERT	Boards
					SELECT	NULL					AS	CurrentPieceID
					,		0						AS	Rotation
					,		NULL					AS	HeldPieceID	
					,		0						AS	Combo
					,		1						AS	Multiplier
					,		Matches.StartingLevel	AS	StartingLevel
					,		0						AS	Score
					,		NULL					AS	PowerUP
					FROM	Matches
					WHERE	MatchID					=	@MatchID

					DECLARE	@BoardID					int
					SET		@BoardID				=	@@IDENTITY

					INSERT	MatchAccounts
					SELECT	@MatchID
					,		@AccountID
					,		@BoardID
					,		Accounts.CustomPieceID
					,		NULL
					,		NULL
					FROM	Accounts
					WHERE	Accounts.AccountID		=	@AccountID

					DECLARE	@ChatGroupID				int

					SELECT	@ChatGroupID			=	Matches.ChatGroupID
					FROM	Matches
					WHERE	Matches.MatchID			=	@MatchID

					UPDATE	Accounts
					SET		Accounts.UserStatusID	=	6
					WHERE	Accounts.AccountID		=	@AccountID
					AND		Accounts.UserStatusID	!=	4			--HIDDEN

					UPDATE	Accounts
					SET		Accounts.CurrentMatchID	=	@MatchID
					WHERE	Accounts.AccountID		=	@AccountID
					
					--UPDATE	Accounts
					--SET		Accounts.UserStatusID		=	UserStatus.UserStatusID
					--FROM	UserStatus
					--WHERE	UserStatus.UserStatus			=	'InLobby'
					--AND		Accounts.AccountID			=	@AccountID

					IF	(@ChatGroupID			!=	NULL)	
						BEGIN
							INSERT	ChatGroupAccess
							VALUES	(@ChatGroupID, @AccountID)
						END
					INSERT	ChatGroupAccessTimeSheet
					VALUES	(@ChatGroupID, @AccountID, GETDATE(), NULL)

					SELECT	@BoardID AS 'BoardID'
				END
END

GO
