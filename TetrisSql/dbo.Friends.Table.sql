USE [Tetris]
GO
/****** Object:  Table [dbo].[Friends]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Friends](
	[AccountID] [int] NOT NULL,
	[FriendID] [int] NOT NULL,
	[Notes] [nvarchar](50) NULL,
	[TimeAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_Friends_1] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC,
	[FriendID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Friends]  WITH CHECK ADD  CONSTRAINT [FK_Friends_Accounts2] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[Friends] CHECK CONSTRAINT [FK_Friends_Accounts2]
GO
ALTER TABLE [dbo].[Friends]  WITH CHECK ADD  CONSTRAINT [FK_Friends_Accounts3] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[Friends] CHECK CONSTRAINT [FK_Friends_Accounts3]
GO
