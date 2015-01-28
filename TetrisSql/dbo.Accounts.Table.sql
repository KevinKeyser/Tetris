USE [Tetris]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 1/28/2015 12:38:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[ExternalAccountID] [uniqueidentifier] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[CustomPieceID] [int] NULL,
	[CurrentMatchID] [int] NULL,
	[IsVerified] [bit] NOT NULL,
	[SecurityQ1] [tinyint] NULL,
	[SecurityA1] [varchar](50) NULL,
	[SecurityQ2] [tinyint] NULL,
	[SecurityA2] [varchar](50) NULL,
	[SecurityQ3] [tinyint] NULL,
	[SecurityA3] [varchar](50) NULL,
	[Birthday] [date] NOT NULL,
	[UserStatusID] [tinyint] NOT NULL,
	[LastUserStatusID] [tinyint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[LastLoggedIn] [datetime] NOT NULL,
	[TimeCreated] [datetime] NOT NULL,
	[DeletionDate] [datetime] NULL,
 CONSTRAINT [PK_Accounts_1] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Matches] FOREIGN KEY([CurrentMatchID])
REFERENCES [dbo].[Matches] ([MatchID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Matches]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_SecurityQuestions3] FOREIGN KEY([SecurityQ3])
REFERENCES [dbo].[SecurityQuestions] ([QuestionID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_SecurityQuestions3]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_SecurityQuestions4] FOREIGN KEY([SecurityQ2])
REFERENCES [dbo].[SecurityQuestions] ([QuestionID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_SecurityQuestions4]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_SecurityQuestions5] FOREIGN KEY([SecurityQ1])
REFERENCES [dbo].[SecurityQuestions] ([QuestionID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_SecurityQuestions5]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_UserStatus] FOREIGN KEY([UserStatusID])
REFERENCES [dbo].[UserStatus] ([UserStatusID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_UserStatus]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_UserStatus1] FOREIGN KEY([LastUserStatusID])
REFERENCES [dbo].[UserStatus] ([UserStatusID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_UserStatus1]
GO
