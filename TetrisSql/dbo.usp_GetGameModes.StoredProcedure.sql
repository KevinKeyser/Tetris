USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetGameModes]    Script Date: 1/28/2015 12:38:15 AM ******/
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
