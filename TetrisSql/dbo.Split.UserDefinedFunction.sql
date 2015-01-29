USE [Tetris]
GO
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Split]
(	
	@String		nvarchar(4000),
	@Delimiter	nchar(1)
)
RETURNS TABLE 
AS
RETURN 
(
	WITH	Split(stpos, endpos)
	AS(
		SELECT 0 AS stpos, CHARINDEX(@Delimiter, @String) AS endpos
		UNION ALL
		SELECT endpos+1, CHARINDEX(@Delimiter, @String, endpos+1)
			FROM	Split
			WHERE	endpos	>	0
	)
	SELECT	'ID'	=	ROW_NUMBER() OVER (ORDER BY (SELECT 1))
	,		'Data'	=	SUBSTRING(@String, stpos, COALESCE(NULLIF(endpos, 0), LEN(@String) + 1) - stpos)
	FROM Split
)

GO
