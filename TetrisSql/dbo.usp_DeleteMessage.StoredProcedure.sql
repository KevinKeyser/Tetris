USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteMessage]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_DeleteMessage]
	@MessageID	int
AS
BEGIN
	UPDATE	Messages
	SET		DeletionDate		=	GETDATE()
	WHERE	Messages.MessageID	=	@MessageID
END

GO
