GO
Drop table [UsersRelations]
GO
/****** Object:  Table [dbo].[UsersRelations]    Script Date: 20.02.2020 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersRelations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OfferSendedDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[IsInvitationSeen] [bit] NOT NULL,
	[BlockEntrie] [uniqueidentifier] NOT NULL,
	[UserConfirmer_Id] [int] NULL,
	[UserOferFrienshipSender_Id] [int] NULL,
 CONSTRAINT [PK_dbo.UsersRelations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO