USE [Tetris]
GO
/****** Object:  Table [dbo].[ChatGroupAccess]    Script Date: 1/29/2015 1:00:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatGroupAccess](
	[ChatGroupID] [int] NOT NULL,
	[AccountID] [int] NOT NULL,
 CONSTRAINT [PK_GroupAccess] PRIMARY KEY CLUSTERED 
(
	[ChatGroupID] ASC,
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChatGroupAccess]  WITH CHECK ADD  CONSTRAINT [FK_GroupAccess_Accounts] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[ChatGroupAccess] CHECK CONSTRAINT [FK_GroupAccess_Accounts]
GO
ALTER TABLE [dbo].[ChatGroupAccess]  WITH CHECK ADD  CONSTRAINT [FK_GroupAccess_Groups] FOREIGN KEY([ChatGroupID])
REFERENCES [dbo].[ChatGroups] ([ChatGroupID])
GO
ALTER TABLE [dbo].[ChatGroupAccess] CHECK CONSTRAINT [FK_GroupAccess_Groups]
GO
