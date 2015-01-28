USE [Tetris]
GO
/****** Object:  Table [dbo].[ChatGroups]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatGroups](
	[ChatGroupID] [int] IDENTITY(1,1) NOT NULL,
	[ExternalChatGroupID] [uniqueidentifier] NOT NULL,
	[OwnerID] [int] NOT NULL,
	[DeletionDate] [datetime] NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[ChatGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChatGroups]  WITH CHECK ADD  CONSTRAINT [FK_ChatGroups_Accounts] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[Accounts] ([AccountID])
GO
ALTER TABLE [dbo].[ChatGroups] CHECK CONSTRAINT [FK_ChatGroups_Accounts]
GO
