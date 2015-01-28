USE [Tetris]
GO
/****** Object:  Table [dbo].[SecurityQuestions]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SecurityQuestions](
	[QuestionID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Question] [varchar](100) NOT NULL,
 CONSTRAINT [PK_SecurityQuestions] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
