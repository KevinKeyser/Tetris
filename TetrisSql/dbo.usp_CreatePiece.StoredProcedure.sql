USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_CreatePiece]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_CreatePiece]
	@AccountID	int
AS
BEGIN
	INSERT	Pieces
	VALUES	(@AccountID, 1, NULL)
END


GO
