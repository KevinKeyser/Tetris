USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateBoardInfo]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UpdateBoardInfo]
		@BoardID		int,
		@CurrentPieceID	int,
		@PieceX			int,
		@PieceY			int,
		@Rotation		tinyint,
		@HeldPieceID	int,
		@Combo			int,
		@Multiplier		float,
		@Level			int,
		@Score			int,
		@PowerUp		int
AS
BEGIN
	UPDATE	Boards
	SET		Boards.CurrentPieceID	=	@CurrentPieceID
	,		Boards.PieceX			=	@PieceX
	,		Boards.PieceY			=	@PieceY
	,		Boards.Rotation			=	@Rotation
	,		Boards.HeldPieceID		=	@HeldPieceID
	,		Boards.Combo			=	@Combo
	,		Boards.Multiplier		=	@Multiplier
	,		Boards.Level			=	@Level
	,		Boards.Score			=	@Score
	,		Boards.PowerUp			=	@PowerUp
	WHERE	Boards.BoardID			=	@BoardID
END

GO
