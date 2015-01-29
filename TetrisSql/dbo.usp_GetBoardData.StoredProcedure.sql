USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetBoardData]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE	[dbo].[usp_GetBoardData]
	@BoardID	int
AS
BEGIN
	Select	BoardData.RowID
	,		BoardData.ColumnID
	,		BoardData.PackedColor
	FROM	BoardData
	WHERE	BoardData.BoardID		=	@BoardID
END

GO
