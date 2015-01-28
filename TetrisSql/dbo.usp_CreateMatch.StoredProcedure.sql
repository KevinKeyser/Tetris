USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateMatch]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_CreateMatch]
	@AccountID				int,		--Match creator
	@GameModeID				tinyint,
	@RoomName				nvarchar(50),
	@Password				varchar(100),
	@IsChatEnabled			bit,
	@IsSpectatingEnabled	bit,
	@IsPowerUpEnabled		bit,
	@IsCustomPieceEnabled	bit,
	@CustomPiece			int,
	@IsPausingEnabled		bit,
	@StartingLevel			int,
	@MaxPlayers				tinyint,
	@BoardWidth				int,
	@BoardHeight			int
AS

BEGIN
	DECLARE	@ChatGroupID	int
	IF	(@IsChatEnabled = 1)
		BEGIN
			INSERT	ChatGroups
			VALUES	(NEWID(), @AccountID, NULL)

			SET		@ChatGroupID	=	@@IDENTITY

			INSERT	ChatGroupAccess
			VALUES	(@ChatGroupID, @AccountID)

			INSERT ChatGroupAccessTimeSheet
			VALUES	(@ChatGroupID, @AccountID, GETDATE(), NULL)
		END

	INSERT	Matches
	VALUES	(NEWID(), @GameModeID, @AccountID, @RoomName, @Password, @ChatGroupID, @IsSpectatingEnabled,
			@IsPowerUpEnabled, @IsCustomPieceEnabled, @IsPausingEnabled, NULL, @StartingLevel, @MaxPlayers,
			@BoardWidth, @BoardHeight, NULL, NULL)

	DECLARE @MatchID	int
	SET		@MatchID	= @@IDENTITY

	INSERT Boards
	VALUES (NULL, 0, NULL, 0, 0, @StartingLevel, 0, 1)
	
	DECLARE @BoardID	int
	SET		@BoardID	= @@IDENTITY

	INSERT	MatchAccounts
	SELECT	@MatchID
	,		@AccountID
	,		@BoardID
	,		Accounts.CustomPieceID
	,		NULL
	,		NULL
	FROM	Accounts
	WHERE	Accounts.AccountID		=	@AccountID

	UPDATE	Accounts
	SET		Accounts.UserStatusID	=	6	--InLobby
	WHERE	Accounts.UserStatusID	!=	4	--HIDDEN
	AND		Accounts.AccountID		=	@AccountID

	UPDATE	Accounts
	SET		Accounts.CurrentMatchID	=	@MatchID
	WHERE	Accounts.AccountID		=	@AccountID

	
	SELECT @MatchID			AS MatchID
END


--Join example
--SELECT	cga.*
--,		cg.ExternalChatGroupID
--FROM	ChatGroupAccess			cga
--JOIN	ChatGroups				cg
--ON		cga.ChatGroupID		=	cg.ChatGroupID

--SELECT	cga.*
--,		cg.ExternalChatGroupID
--FROM	ChatGroupAccess			cga
--,		ChatGroups				cg
--WHERE	cga.ChatGroupID		=	cg.ChatGroupID


GO
