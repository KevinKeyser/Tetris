USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_ChangeEmail]    Script Date: 1/29/2015 1:00:00 AM ******/
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
