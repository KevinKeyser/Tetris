USE [Tetris]
GO
/****** Object:  Table [dbo].[MatchAccounts]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MatchAccounts](
	[MatchID] [int] NOT NULL,
	[AccountID] [int] NOT NULL,
	[BoardID] [int] NOT NULL,
	[CustomPieceID] [int] NULL,
	[Ranking] [tinyint] NULL,
	[TimeFinished] [datetime] NULL,
 CONSTRAINT [PK_MatchAccounts] PRIMARY KEY CLUSTERED 
(
	[MatchID] ASC,
	[AccountID] ASC,
	[BoardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MatchAccounts]  WITH CHECK ADD  CONSTRAINT [FK_MatchAccounts_Accounts1] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[MatchAccounts] CHECK CONSTRAINT [FK_MatchAccounts_Accounts1]
GO
ALTER TABLE [dbo].[MatchAccounts]  WITH CHECK ADD  CONSTRAINT [FK_MatchAccounts_Boards] FOREIGN KEY([BoardID])
REFERENCES [dbo].[Boards] ([BoardID])
GO
ALTER TABLE [dbo].[MatchAccounts] CHECK CONSTRAINT [FK_MatchAccounts_Boards]
GO
ALTER TABLE [dbo].[MatchAccounts]  WITH CHECK ADD  CONSTRAINT [FK_MatchAccounts_Matches1] FOREIGN KEY([MatchID])
REFERENCES [dbo].[Matches] ([MatchID])
GO
ALTER TABLE [dbo].[MatchAccounts] CHECK CONSTRAINT [FK_MatchAccounts_Matches1]
GO
