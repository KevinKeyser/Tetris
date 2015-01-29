USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteMessage]    Script Date: 1/29/2015 1:00:00 AM ******/
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
