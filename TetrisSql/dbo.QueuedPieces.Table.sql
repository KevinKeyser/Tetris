USE [Tetris]
GO
/****** Object:  Table [dbo].[QueuedPieces]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QueuedPieces](
	[PieceID] [int] NOT NULL,
	[BoardID] [int] NOT NULL,
	[QueuePlace] [tinyint] NOT NULL,
 CONSTRAINT [PK_QueuedPieces] PRIMARY KEY CLUSTERED 
(
	[PieceID] ASC,
	[BoardID] ASC,
	[QueuePlace] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[QueuedPieces]  WITH CHECK ADD  CONSTRAINT [FK_QueuedPieces_Boards] FOREIGN KEY([BoardID])
REFERENCES [dbo].[Boards] ([BoardID])
GO
ALTER TABLE [dbo].[QueuedPieces] CHECK CONSTRAINT [FK_QueuedPieces_Boards]
GO
ALTER TABLE [dbo].[QueuedPieces]  WITH CHECK ADD  CONSTRAINT [FK_QueuedPieces_Pieces] FOREIGN KEY([PieceID])
REFERENCES [dbo].[Pieces] ([PieceID])
GO
ALTER TABLE [dbo].[QueuedPieces] CHECK CONSTRAINT [FK_QueuedPieces_Pieces]
GO
