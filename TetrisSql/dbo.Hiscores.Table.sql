USE [Tetris]
GO
/****** Object:  Table [dbo].[Hiscores]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hiscores](
	[AccountID] [int] NOT NULL,
	[GameModeID] [tinyint] NOT NULL,
	[Score] [bigint] NOT NULL,
 CONSTRAINT [PK_Hiscores] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC,
	[GameModeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Hiscores]  WITH CHECK ADD  CONSTRAINT [FK_Hiscores_Accounts] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[Hiscores] CHECK CONSTRAINT [FK_Hiscores_Accounts]
GO
ALTER TABLE [dbo].[Hiscores]  WITH CHECK ADD  CONSTRAINT [FK_Hiscores_GameModes] FOREIGN KEY([GameModeID])
REFERENCES [dbo].[GameModes] ([GameModeID])
GO
ALTER TABLE [dbo].[Hiscores] CHECK CONSTRAINT [FK_Hiscores_GameModes]
GO
