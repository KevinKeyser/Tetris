USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUserStatuses]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetUserStatuses]
AS
BEGIN
	SELECT	*
	FROM	UserStatus
END

GO
