USE [Tetris]
GO
/****** Object:  Table [dbo].[Matches]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Matches](
	[MatchID] [int] IDENTITY(1,1) NOT NULL,
	[ExternalMatchID] [uniqueidentifier] NOT NULL,
	[GameModeID] [tinyint] NOT NULL,
	[OwnerID] [int] NOT NULL,
	[RoomName] [nvarchar](50) NULL,
	[Password] [varchar](100) NULL,
	[ChatGroupID] [int] NULL,
	[IsSpectatingEnabled] [bit] NOT NULL,
	[IsPowerUpsEnabled] [bit] NOT NULL,
	[IsCutomPieceEnabled] [bit] NOT NULL,
	[IsPausingEnabled] [bit] NOT NULL,
	[PlayerIDPaused] [int] NULL,
	[StartingLevel] [int] NOT NULL,
	[MaxPlayers] [tinyint] NOT NULL,
	[BoardWidth] [tinyint] NOT NULL,
	[BoardHeight] [tinyint] NOT NULL,
	[TimeStarted] [datetime] NULL,
	[TimeEnded] [datetime] NULL,
 CONSTRAINT [PK_Matches] PRIMARY KEY CLUSTERED 
(
	[MatchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Matches_Accounts] FOREIGN KEY([PlayerIDPaused])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Matches_Accounts]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Matches_Accounts1] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Matches_Accounts1]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Matches_ChatGroups] FOREIGN KEY([ChatGroupID])
REFERENCES [dbo].[ChatGroups] ([ChatGroupID])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Matches_ChatGroups]
GO
ALTER TABLE [dbo].[Matches]  WITH CHECK ADD  CONSTRAINT [FK_Matches_GameMode1] FOREIGN KEY([GameModeID])
REFERENCES [dbo].[GameModes] ([GameModeID])
GO
ALTER TABLE [dbo].[Matches] CHECK CONSTRAINT [FK_Matches_GameMode1]
GO
