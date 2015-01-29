USE [Tetris]
GO
/****** Object:  Table [dbo].[Pieces]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pieces](
	[PieceID] [int] IDENTITY(1,1) NOT NULL,
	[AccountID] [int] NOT NULL,
	[IsCustom] [bit] NOT NULL,
	[DeletionDate] [datetime] NULL,
 CONSTRAINT [PK_Pieces] PRIMARY KEY CLUSTERED 
(
	[PieceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Pieces]  WITH CHECK ADD  CONSTRAINT [FK_Pieces_Accounts1] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[Pieces] CHECK CONSTRAINT [FK_Pieces_Accounts1]
GO
