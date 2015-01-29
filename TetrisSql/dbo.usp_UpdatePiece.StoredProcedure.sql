USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdatePiece]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_UpdatePiece]
	@PieceID		int,
	@RowID			int,
	@ColumnID		int,
	@PackedColor	int
AS
BEGIN
	IF EXISTS	(SELECT PieceData.PieceID
				FROM	PieceData
				WHERE	PieceData.PieceID			=	@PieceID
				AND		PieceData.RowID				=	@RowID
				AND		PieceData.ColumnID			=	@ColumnID)
				BEGIN
					UPDATE	PieceData
					SET		PieceData.PackedColor	=	@PackedColor
					WHERE	PieceData.PieceID		=	@PieceID
					AND		PieceData.RowID			=	@RowID
					AND		PieceData.ColumnID		=	@ColumnID
					RETURN
				END
	INSERT	PieceData
	VALUES	(@PieceID, @RowID, @ColumnID, @PackedColor)
END


GO
