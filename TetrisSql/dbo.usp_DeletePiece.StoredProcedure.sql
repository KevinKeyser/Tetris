USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_DeletePiece]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_DeletePiece]
	@PieceID	int
AS
BEGIN
	UPDATE	Pieces
	SET		Pieces.DeletionDate =	GETDATE()
	WHERE	Pieces.PieceID		=	@PieceID
END

GO
