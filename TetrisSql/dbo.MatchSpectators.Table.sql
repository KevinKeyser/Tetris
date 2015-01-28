USE [Tetris]
GO
/****** Object:  Table [dbo].[MatchSpectators]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MatchSpectators](
	[SpectatorID] [int] IDENTITY(1,1) NOT NULL,
	[MatchID] [int] NOT NULL,
	[AccountID] [int] NOT NULL,
 CONSTRAINT [PK_MatchSpectators] PRIMARY KEY CLUSTERED 
(
	[SpectatorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MatchSpectators]  WITH CHECK ADD  CONSTRAINT [FK_MatchSpectators_Accounts] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[MatchSpectators] CHECK CONSTRAINT [FK_MatchSpectators_Accounts]
GO
ALTER TABLE [dbo].[MatchSpectators]  WITH CHECK ADD  CONSTRAINT [FK_MatchSpectators_Matches] FOREIGN KEY([MatchID])
REFERENCES [dbo].[Matches] ([MatchID])
GO
ALTER TABLE [dbo].[MatchSpectators] CHECK CONSTRAINT [FK_MatchSpectators_Matches]
GO
