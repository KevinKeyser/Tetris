USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetPiece]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetPiece]
	@PieceID	int
AS
BEGIN
	SELECT	Pieces.AccountID
	,		Pieces.PieceID
	,		PieceData.RowID
	,		PieceData.ColumnID
	,		PieceData.PackedColor
	FROM	PieceData
	JOIN	Pieces
	ON		Pieces.PieceID			=	PieceData.PieceID
	WHERE	PieceData.PieceID		=	@PieceID
END

GO
