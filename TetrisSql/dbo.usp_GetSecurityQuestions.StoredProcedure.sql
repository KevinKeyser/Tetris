USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetSecurityQuestions]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetSecurityQuestions]
AS
BEGIN
	SELECT	SecurityQuestions.Question
	FROM	SecurityQuestions
END

GO
