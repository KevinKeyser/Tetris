USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddGroupAccess]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_AddGroupAccess]
	@ExternalChatGroupID			uniqueidentifier,
	@AccountID						int
AS
BEGIN
	INSERT	ChatGroupAccess
	SELECT	ChatGroupID
	,		@AccountID
	FROM	ChatGroups
	WHERE	ExternalChatGroupID		=	@ExternalChatGroupID

	INSERT	ChatGroupAccessTimeSheet
	SELECT	ChatGroupID
	,		@AccountID
	,		GETDATE()
	,		NULL
	FROM	ChatGroups
	WHERE	ExternalChatGroupID		=	@ExternalChatGroupID

END

--DECLARE	@AccountID	int
--DECLARE	@ExternalChatGroupID	uniqueidentifier
--DECLARE	@Password				varchar(100)

--SET	@AccountID = 0
--SET	@ExternalChatGroupID = NEWID()
--SET @Password = NULL

--INSERT	ChatGroupAccess
--SELECT	ChatGroupID
--,		@AccountID
--,		GETDATE()
--FROM	ChatGroups
--WHERE	ExternalChatGroupID		=	@ExternalChatGroupID
--AND		Password				=	@Password


--SELECT	'Hello' AS SomeRandomColumn
--,		ChatGroupID
--FROM	ChatGroups

GO
