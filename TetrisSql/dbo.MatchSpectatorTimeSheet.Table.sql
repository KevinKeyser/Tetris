USE [Tetris]
GO
/****** Object:  Table [dbo].[MatchSpectatorTimeSheet]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MatchSpectatorTimeSheet](
	[SpectatorID] [int] NOT NULL,
	[TimeStarted] [datetime] NOT NULL,
	[TimeEnded] [datetime] NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MatchSpectatorTimeSheet]  WITH CHECK ADD  CONSTRAINT [FK_MatchSpectatorTimeSheet_MatchSpectators] FOREIGN KEY([SpectatorID])
REFERENCES [dbo].[MatchSpectators] ([SpectatorID])
GO
ALTER TABLE [dbo].[MatchSpectatorTimeSheet] CHECK CONSTRAINT [FK_MatchSpectatorTimeSheet_MatchSpectators]
GO
