USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetBoardData]    Script Date: 1/28/2015 12:38:15 AM ******/
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
