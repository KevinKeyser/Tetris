USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_ChangeEmail]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ChangeEmail]
	@AccountID	int,
	@Email		varchar(100)
AS
BEGIN
	UPDATE	Accounts
	SET		Accounts.Email		= @Email
	WHERE	Accounts.AccountID	= @AccountID
END

GO
