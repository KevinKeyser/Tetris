USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetGameModes]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetGameModes]
AS
BEGIN
	SELECT	*
	FROM	GameModes
END

GO
