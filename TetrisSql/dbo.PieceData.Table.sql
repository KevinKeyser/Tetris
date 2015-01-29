USE [Tetris]
GO
/****** Object:  Table [dbo].[PieceData]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PieceData](
	[PieceID] [int] NOT NULL,
	[RowID] [int] NOT NULL,
	[ColumnID] [int] NOT NULL,
	[PackedColor] [int] NULL,
 CONSTRAINT [PK_PieceData] PRIMARY KEY CLUSTERED 
(
	[PieceID] ASC,
	[RowID] ASC,
	[ColumnID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PieceData]  WITH CHECK ADD  CONSTRAINT [FK_PieceData_Pieces] FOREIGN KEY([PieceID])
REFERENCES [dbo].[Pieces] ([PieceID])
GO
ALTER TABLE [dbo].[PieceData] CHECK CONSTRAINT [FK_PieceData_Pieces]
GO
