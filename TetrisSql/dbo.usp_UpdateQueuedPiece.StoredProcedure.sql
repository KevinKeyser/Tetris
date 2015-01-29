USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateQueuedPiece]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UpdateQueuedPiece]
	@PieceID		int,
	@BoardID		int,
	@QueuePlace		int
AS
BEGIN
	IF EXISTS	(SELECT	* 
				FROM	QueuedPieces
				WHERE	QueuedPieces.BoardID		=	@BoardID
				AND		QueuedPieces.QueuePlace		=	@QueuePlace)
				BEGIN
					UPDATE	QueuedPieces
					SET		QueuedPieces.PieceID	=	@PieceID
					WHERE	QueuedPieces.BoardID	=	@BoardID
					AND		QueuedPieces.QueuePlace =	@QueuePlace
				RETURN
			END
	INSERT	QueuedPieces
	VALUES	(@PieceID, @BoardID, @QueuePlace)
END

GO
