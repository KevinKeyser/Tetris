USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_ChangePassword]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ChangePassword]
	@AccountID	int,
	@Password	varchar(100)
AS
BEGIN
	UPDATE	Accounts
	SET		Accounts.[Password]	= @Password
	WHERE	Accounts.AccountID	= @AccountID
END

GO
