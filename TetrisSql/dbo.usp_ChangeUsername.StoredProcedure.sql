USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_ChangeUsername]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_ChangeUsername]
	@AccountID	int,
	@Username	varchar(50)
AS
BEGIN
	UPDATE	Accounts
	SET		Accounts.Username	= @Username
	WHERE	Accounts.AccountID	= @AccountID
END

GO
