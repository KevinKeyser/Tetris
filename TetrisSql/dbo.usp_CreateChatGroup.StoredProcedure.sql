USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateChatGroup]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_CreateChatGroup]
	@AccountIDs	nvarchar(4000),
	@Delimiter	nchar(1)		=	','
AS
BEGIN
	--DEBUG Purposes
	--DECLARE	@AccountIDs	nvarchar(4000)
	--DECLARE @Delimiter	nchar(1)
	--SET	@AccountIDs	= '1,2,4,5'
	--SET	@Delimiter	= ','; 

	DECLARE	@MaxID				int;

	--Check if ChatGroup for this set of AccountIDs already exists
	DECLARE @ChatGroupID		int;


	SELECT	ID
	,		AccountID	=	CONVERT (int, Data)
	INTO	#SplitData
	FROM	dbo.Split(@AccountIDs, @Delimiter)

	SELECT	@MaxID		=	MAX(ID)
	FROM	#SplitData

	SELECT	DISTINCT
			@ChatGroupID						=	ChatGroupAccessTimeSheet.ChatGroupID
	--,		ChatGroupAccessTimeSheet.AccountID
	FROM	ChatGroupAccessTimeSheet
	JOIN	#SplitData
	ON		ChatGroupAccessTimeSheet.AccountID	=	#SplitData.AccountID
	WHERE	ChatGroupAccessTimeSheet.ChatGroupID	NOT IN
	(
		SELECT	ChatGroupAccessTimeSheet.ChatGroupID
		FROM	ChatGroupAccessTimeSheet
		WHERE	AccountID							NOT IN
		(
			SELECT	AccountID
			FROM	#SplitData
		)
	)
	GROUP BY	ChatGroupAccessTimeSheet.ChatGroupID
	HAVING		COUNT(ChatGroupAccessTimeSheet.AccountID) =	@MaxID
	
	IF(@ChatGroupID	IS NOT NULL)
	BEGIN
		SELECT	@ChatGroupID	AS	ChatGroupID
		RETURN
	END

	--TODO: Create new group. Accounts are not accessible though - may need to convert SplitData to #temp table
	--TEMP: Debug...
	--SELECT	'Group not found...'		AS	ChatGroupID
	INSERT	ChatGroups
	SELECT	NEWID()
	,		#SplitData.AccountID
	,		NULL
	FROM	#SplitData
	WHERE	#SplitData.ID		=	1

	SET		@ChatGroupID		=	@@IDENTITY

	INSERT	ChatGroupAccess
	SELECT	@ChatGroupID
	,		#SplitData.AccountID
	FROM	#SplitData

	INSERT	ChatGroupAccessTimeSheet
	SELECT	@ChatGroupID
	,		#SplitData.AccountID
	,		GETDATE()
	,		NULL
	FROM	#SplitData

	SELECT @ChatGroupID	AS ChatGroupID

	DROP TABLE #SplitData

	--SELECT	*
	--INTO	#tmp1
	--FROM	#tmp
	--WHERE	#tmp.AccountID	
	--IN(
	--	SELECT	#tmp.AccountID
	--	FROM	#tmp
	--)

	--DELETE	#tmp1
	--WHERE	#tmp1.AccountID
	--NOT IN(
	--	SELECT	#tmp.AccountID
	--	FROM	#tmp
	--)
	--SELECT DISTINCT #tmp1.ChatGroupID
	--FROM			#tmp1
	--GROUP BY		#tmp1.ChatGroupID
	--HAVING			COUNT(#tmp1.AccountID) = 3

	--DROP TABLE #tmp
	--DROP TABLE #tmp1

	--select * from #tmp
	--SELECT * FROM ChatGroupAccess
	--SELECT	*	FROM	Accounts
-----------------------------

--SELECT * FROM ChatGroups
	--WITH	tmpChatAccounts (int 
	--SELECT	ChatGroupAccess.ChatGroupID
	--,		AccountID
	--FROM	ChatGroupAccess
	--JOIN	dbo.Split(@AccountIDs, @Delimiter)		tmp
	--ON		ChatGroupAccess.AccountID		=	CONVERT (int, tmp.Data)



	--INSERT	ChatGroups
	--VALUES	(NEWID(), @AccountID, NULL)

	--DECLARE	@ChatGroupID									int
	--SET		@ChatGroupID								=	@@IDENTITY

	--INSERT	ChatGroupAccess
	--VALUES	(@ChatGroupID, @AccountID)

	--INSERT	ChatGroupAccessTimeSheet
	--VALUES	(@ChatGroupID, @AccountID, GETDATE(), NULL)

	--INSERT	ChatGroupAccess
	--VALUES	(@ChatGroupID,	@FriendID)

	--INSERT	ChatGroupAccessTimeSheet
	--VALUES	(@ChatGroupID, @FriendID, GETDATE(), NULL)

	--SELECT	@ChatGroupID								AS	ChatGroupID
END

GO
