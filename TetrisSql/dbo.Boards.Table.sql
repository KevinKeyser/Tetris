USE [Tetris]
GO
/****** Object:  Table [dbo].[Boards]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Boards](
	[BoardID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[CurrentPieceID] [int] NULL,
	[PieceX] [int] NULL,
	[PieceY] [int] NULL,
	[Rotation] [tinyint] NOT NULL,
	[HeldPieceID] [int] NULL,
	[Combo] [int] NOT NULL,
	[Multiplier] [float] NOT NULL,
	[Level] [int] NOT NULL,
	[Score] [bigint] NOT NULL,
	[PowerUp] [tinyint] NULL,
 CONSTRAINT [PK_Boards] PRIMARY KEY CLUSTERED 
(
	[BoardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Boards]  WITH CHECK ADD  CONSTRAINT [FK_Boards_Pieces] FOREIGN KEY([HeldPieceID])
REFERENCES [dbo].[Pieces] ([PieceID])
GO
ALTER TABLE [dbo].[Boards] CHECK CONSTRAINT [FK_Boards_Pieces]
GO
ALTER TABLE [dbo].[Boards]  WITH CHECK ADD  CONSTRAINT [FK_Boards_Pieces1] FOREIGN KEY([CurrentPieceID])
REFERENCES [dbo].[Pieces] ([PieceID])
GO
ALTER TABLE [dbo].[Boards] CHECK CONSTRAINT [FK_Boards_Pieces1]
GO
ALTER TABLE [dbo].[Boards]  WITH CHECK ADD  CONSTRAINT [FK_Boards_PowerUps] FOREIGN KEY([PowerUp])
REFERENCES [dbo].[PowerUps] ([PowerUpID])
GO
ALTER TABLE [dbo].[Boards] CHECK CONSTRAINT [FK_Boards_PowerUps]
GO
