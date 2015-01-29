USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateBoardData]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_UpdateBoardData]
	@BoardID		int,
	@RowID			int,
	@ColumnID		int,
	@PackedColor	int
AS
BEGIN

	IF EXISTS(	SELECT	* 
			FROM	BoardData						bd 
			WHERE	bd.BoardID					=	@BoardID 
			AND		bd.RowID					=	@RowID 
			AND		bd.ColumnID					=	@ColumnID)
			BEGIN
				UPDATE	BoardData
				SET		BoardData.PackedColor	=	@PackedColor
				WHERE	BoardData.BoardID		=	@BoardID
				AND		BoardData.RowID			=	@RowID
				AND		BoardData.ColumnID		=	@ColumnID
				RETURN
			END

	INSERT	BoardData
	VALUES	(@BoardID, @RowID, @ColumnID, @PackedColor)

END



GO
