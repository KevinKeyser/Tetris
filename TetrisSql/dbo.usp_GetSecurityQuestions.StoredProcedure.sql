USE [Tetris]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetSecurityQuestions]    Script Date: 1/29/2015 1:00:00 AM ******/
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
