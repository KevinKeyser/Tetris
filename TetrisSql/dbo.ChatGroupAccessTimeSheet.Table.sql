USE [Tetris]
GO
/****** Object:  Table [dbo].[ChatGroupAccessTimeSheet]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatGroupAccessTimeSheet](
	[ChatGroupID] [int] NOT NULL,
	[AccountID] [int] NOT NULL,
	[TimeAdded] [datetime] NOT NULL,
	[RemovedTime] [datetime] NULL,
 CONSTRAINT [PK_ChatGroupAccessTimeSheet] PRIMARY KEY CLUSTERED 
(
	[ChatGroupID] ASC,
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChatGroupAccessTimeSheet]  WITH CHECK ADD  CONSTRAINT [FK_ChatGroupAccessTimeSheet_ChatGroupAccess] FOREIGN KEY([ChatGroupID], [AccountID])
REFERENCES [dbo].[ChatGroupAccess] ([ChatGroupID], [AccountID])
GO
ALTER TABLE [dbo].[ChatGroupAccessTimeSheet] CHECK CONSTRAINT [FK_ChatGroupAccessTimeSheet_ChatGroupAccess]
GO
