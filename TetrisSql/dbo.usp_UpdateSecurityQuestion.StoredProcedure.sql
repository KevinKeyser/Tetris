USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateSecurityQuestion]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_UpdateSecurityQuestion]
	@AccountID		int,
	@QuestionNumber	int,
	@QuestionID		tinyint,
	@Answer			varchar(50)
AS
BEGIN
	IF(@QuestionNumber = 1)
	BEGIN
		UPDATE	Accounts
		SET		SecurityQ1				= @QuestionID
		,		SecurityA1				= @Answer
		WHERE	Accounts.AccountID		= @AccountID
	END
	ELSE IF(@QuestionNumber = 2)
	BEGIN
		UPDATE	Accounts
		SET		SecurityQ2				= @QuestionID
		,		SecurityA2				= @Answer
		WHERE	Accounts.AccountID		= @AccountID
	END
	ELSE IF(@QuestionNumber = 3)
	BEGIN
		UPDATE	Accounts
		SET		SecurityQ3				= @QuestionID
		,		SecurityA3				= @Answer
		WHERE	Accounts.AccountID		= @AccountID
	END
END


GO
