USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_DeletePiece]    Script Date: 1/29/2015 1:00:00 AM ******/
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
