USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetQueuedPieces]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE	[dbo].[usp_GetQueuedPieces]
	@BoardID	int
AS
BEGIN
	Select	QueuedPieces.PieceID
	,		QueuedPieces.QueuePlace
	FROM	QueuedPieces
	WHERE	QueuedPieces.BoardID		=	@BoardID
	ORDER BY QueuedPieces.BoardID, QueuedPieces.QueuePlace ASC
END

GO
