USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetPowerUps]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetPowerUps]
AS
BEGIN
	SELECT	*
	FROM	PowerUps
END

GO
