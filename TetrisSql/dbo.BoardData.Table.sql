USE [Tetris]
GO
/****** Object:  Table [dbo].[BoardData]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BoardData](
	[BoardID] [int] NOT NULL,
	[RowID] [tinyint] NOT NULL,
	[ColumnID] [tinyint] NOT NULL,
	[PackedColor] [int] NULL,
 CONSTRAINT [PK_BoardData] PRIMARY KEY CLUSTERED 
(
	[BoardID] ASC,
	[RowID] ASC,
	[ColumnID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BoardData]  WITH CHECK ADD  CONSTRAINT [FK_BoardData_Boards] FOREIGN KEY([BoardID])
REFERENCES [dbo].[Boards] ([BoardID])
GO
ALTER TABLE [dbo].[BoardData] CHECK CONSTRAINT [FK_BoardData_Boards]
GO
