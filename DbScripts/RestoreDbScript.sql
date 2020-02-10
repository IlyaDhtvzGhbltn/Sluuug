USE [u0823102_slugDtb]
GO
/****** Object:  Table [dbo].[ActivationLinks]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivationLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ActivationParam] [nvarchar](120) NULL,
	[IsExpired] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ActivationLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Albums]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Albums](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[CreateUserID] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[AlbumLabelUrl] [nvarchar](max) NOT NULL,
	[LabelOriginalHeight] [bigint] NOT NULL,
	[LabelOriginalWidth] [bigint] NOT NULL,
	[AlbumLabesPublicID] [nvarchar](max) NULL,
	[User_Id] [int] NULL,
 CONSTRAINT [PK_dbo.Albums] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Avatars]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Avatars](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[UploadTime] [datetime] NOT NULL,
	[IsStandart] [bit] NOT NULL,
	[CountryCode] [int] NULL,
	[LargeAvatar] [nvarchar](max) NOT NULL,
	[MediumAvatar] [nvarchar](max) NULL,
	[SmallAvatar] [nvarchar](max) NULL,
	[AvatarType] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Avatars] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlockedUsersEntries]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlockedUsersEntries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BlockEntryId] [uniqueidentifier] NOT NULL,
	[UserBlocker] [int] NOT NULL,
	[UserBlocked] [int] NOT NULL,
	[BlockDate] [datetime] NOT NULL,
	[HateMessage] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.BlockedUsersEntries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CitiesCode] [int] NOT NULL,
	[CountryCode] [int] NOT NULL,
	[Language] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConversationGroups]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConversationGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConversationGuidId] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ConversationGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Conversations]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conversations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConversationGuidId] [uniqueidentifier] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Conversations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryCode] [int] NOT NULL,
	[Language] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DisableDialogs]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DisableDialogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConversationId] [uniqueidentifier] NOT NULL,
	[UserDisablerId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.DisableDialogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Educations]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Educations](
	[Id] [uniqueidentifier] NOT NULL,
	[EducationType] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Faculty] [nvarchar](max) NULL,
	[Specialty] [nvarchar](max) NULL,
	[CountryCode] [int] NOT NULL,
	[CityCode] [int] NOT NULL,
	[UntilNow] [bit] NOT NULL,
	[Start] [datetime] NOT NULL,
	[End] [datetime] NULL,
	[Comment] [nvarchar](500) NULL,
	[PersonalRating] [int] NOT NULL,
	[User_Id] [int] NULL,
	[UserInfo_Id] [int] NULL,
 CONSTRAINT [PK_dbo.Educations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FakeUsers]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FakeUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CityCode] [int] NOT NULL,
	[CountryCode] [int] NOT NULL,
	[UsersCount] [int] NOT NULL,
 CONSTRAINT [PK_dbo.FakeUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedbacks]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedbacks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [int] NOT NULL,
	[Message] [nvarchar](1000) NOT NULL,
	[BackEmeil] [nvarchar](max) NULL,
	[UserID] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Feedbacks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FotoComments]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FotoComments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserCommenter] [int] NOT NULL,
	[CommentText] [nvarchar](max) NOT NULL,
	[CommentWriteDate] [datetime] NOT NULL,
	[Foto_Id] [int] NULL,
 CONSTRAINT [PK_dbo.FotoComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fotoes]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fotoes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FotoGUID] [uniqueidentifier] NOT NULL,
	[AlbumID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NOT NULL,
	[UploadDate] [datetime] NOT NULL,
	[UploadUserID] [int] NOT NULL,
	[ImagePublicID] [nvarchar](max) NOT NULL,
	[Height] [bigint] NOT NULL,
	[Width] [bigint] NOT NULL,
	[PositiveRating] [int] NOT NULL,
	[NegativeRating] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ImportantEvent_Id] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.Fotoes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImportantEvents]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImportantEvents](
	[Id] [uniqueidentifier] NOT NULL,
	[AlbumGuid] [uniqueidentifier] NOT NULL,
	[EventTitle] [nvarchar](max) NOT NULL,
	[DateEvent] [datetime] NOT NULL,
	[TextEventDescription] [nvarchar](max) NULL,
	[User_Id] [int] NOT NULL,
	[UserInfo_Id] [int] NULL,
 CONSTRAINT [PK_dbo.ImportantEvents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConvarsationGuidId] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[SendingDate] [datetime] NOT NULL,
	[IsReaded] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[UserRecipient] [int] NOT NULL,
	[UserSender] [int] NOT NULL,
	[Message] [nvarchar](max) NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Notifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostComments]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostComments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserCommenter] [int] NOT NULL,
	[CommentText] [nvarchar](max) NOT NULL,
	[CommentWriteDate] [datetime] NOT NULL,
	[Post_Id] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.PostComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [uniqueidentifier] NOT NULL,
	[UserPosted] [int] NOT NULL,
	[PublicDateTime] [datetime] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Posts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResetPasswords]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResetPasswords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserRequestedId] [int] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[ResetParameter] [nvarchar](120) NOT NULL,
	[CreateRequestDate] [datetime] NOT NULL,
	[IsExpired] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ResetPasswords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SecretChatGroups]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecretChatGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PartyGUID] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[Accepted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.SecretChatGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SecretChats]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecretChats](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PartyGUID] [uniqueidentifier] NOT NULL,
	[Create] [datetime] NOT NULL,
	[Destroy] [datetime] NOT NULL,
	[CreatorUserId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.SecretChats] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SecretMessages]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecretMessages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PartyId] [uniqueidentifier] NOT NULL,
	[UserSender] [int] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[SendingDate] [datetime] NOT NULL,
	[IsReaded] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.SecretMessages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](120) NOT NULL,
	[Type] [int] NOT NULL,
	[DateStart] [datetime] NOT NULL,
	[Expired] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Sessions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SessionTypes]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SessionTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.SessionTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserConnections]    Script Date: 10.02.2020 13:26:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserConnections](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConnectionId] [uniqueidentifier] NOT NULL,
	[IpAddress] [nvarchar](max) NOT NULL,
	[CultureCode] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[ConnectionOff] [datetime] NULL,
 CONSTRAINT [PK_dbo.UserConnections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfoes]    Script Date: 10.02.2020 13:26:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfoes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[SurName] [nvarchar](20) NOT NULL,
	[Sex] [int] NOT NULL,
	[NowCountryCode] [int] NOT NULL,
	[NowCityCode] [int] NOT NULL,
	[HelloMessage] [nvarchar](max) NULL,
	[userDatingSex] [int] NOT NULL,
	[DatingPurpose] [int] NOT NULL,
	[userDatingAge] [int] NOT NULL,
	[IdVkUser] [bigint] NOT NULL,
	[IdFBUser] [bigint] NOT NULL,
	[IdOkUser] [bigint] NOT NULL,
	[VipStatusExpiredDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.UserInfoes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10.02.2020 13:26:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](max) NOT NULL,
	[UserStatus] [int] NOT NULL,
	[AvatarGuidId] [uniqueidentifier] NOT NULL,
	[UserType] [int] NOT NULL,
	[RegisterDate] [datetime] NOT NULL,
	[ReferalUserId] [int] NULL,
	[IsFakeBot] [bit] NOT NULL,
	[Settings_Id] [int] NULL,
	[UserFullInfo_Id] [int] NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSettings]    Script Date: 10.02.2020 13:26:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NotificationType] [int] NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[QuickMessage] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UserSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersRelations]    Script Date: 10.02.2020 13:26:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersRelations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OfferSendedDate] [datetime] NOT NULL,
	[UserOferFrienshipSender] [int] NOT NULL,
	[UserConfirmer] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[IsInvitationSeen] [bit] NOT NULL,
	[BlockEntrie] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.UsersRelations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserStatus]    Script Date: 10.02.2020 13:26:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.UserStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VideoConferenceGroups]    Script Date: 10.02.2020 13:26:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoConferenceGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.VideoConferenceGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VideoConferences]    Script Date: 10.02.2020 13:26:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoConferences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GuidId] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Offer] [nvarchar](max) NULL,
	[Answer] [nvarchar](max) NULL,
	[ConferenceCreatorUserId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.VideoConferences] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VkOAuthTokens]    Script Date: 10.02.2020 13:26:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VkOAuthTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](36) NOT NULL,
	[Token] [nvarchar](85) NOT NULL,
	[VkUserId] [bigint] NOT NULL,
	[ReceivedDate] [datetime] NOT NULL,
	[IsExpired] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.VkOAuthTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkPlaces]    Script Date: 10.02.2020 13:26:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkPlaces](
	[Id] [uniqueidentifier] NOT NULL,
	[CompanyTitle] [nvarchar](max) NOT NULL,
	[Position] [nvarchar](max) NOT NULL,
	[CountryCode] [int] NOT NULL,
	[CityCode] [int] NOT NULL,
	[UntilNow] [bit] NOT NULL,
	[Start] [datetime] NOT NULL,
	[End] [datetime] NULL,
	[Comment] [nvarchar](500) NULL,
	[PersonalRating] [int] NOT NULL,
	[User_Id] [int] NULL,
	[UserInfo_Id] [int] NULL,
 CONSTRAINT [PK_dbo.WorkPlaces] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Educations] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[ImportantEvents] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Posts] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[WorkPlaces] ADD  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Albums]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Albums_dbo.Users_User_Id] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Albums] CHECK CONSTRAINT [FK_dbo.Albums_dbo.Users_User_Id]
GO
ALTER TABLE [dbo].[Educations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Educations_dbo.UserInfoes_UserInfo_Id] FOREIGN KEY([UserInfo_Id])
REFERENCES [dbo].[UserInfoes] ([Id])
GO
ALTER TABLE [dbo].[Educations] CHECK CONSTRAINT [FK_dbo.Educations_dbo.UserInfoes_UserInfo_Id]
GO
ALTER TABLE [dbo].[Educations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Educations_dbo.Users_User_Id] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Educations] CHECK CONSTRAINT [FK_dbo.Educations_dbo.Users_User_Id]
GO
ALTER TABLE [dbo].[FotoComments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FotoComments_dbo.Fotoes_Foto_Id] FOREIGN KEY([Foto_Id])
REFERENCES [dbo].[Fotoes] ([Id])
GO
ALTER TABLE [dbo].[FotoComments] CHECK CONSTRAINT [FK_dbo.FotoComments_dbo.Fotoes_Foto_Id]
GO
ALTER TABLE [dbo].[Fotoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Fotoes_dbo.Albums_AlbumID] FOREIGN KEY([AlbumID])
REFERENCES [dbo].[Albums] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Fotoes] CHECK CONSTRAINT [FK_dbo.Fotoes_dbo.Albums_AlbumID]
GO
ALTER TABLE [dbo].[Fotoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Fotoes_dbo.ImportantEvents_ImportantEvent_Id] FOREIGN KEY([ImportantEvent_Id])
REFERENCES [dbo].[ImportantEvents] ([Id])
GO
ALTER TABLE [dbo].[Fotoes] CHECK CONSTRAINT [FK_dbo.Fotoes_dbo.ImportantEvents_ImportantEvent_Id]
GO
ALTER TABLE [dbo].[ImportantEvents]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ImportantEvents_dbo.UserInfoes_UserInfo_Id] FOREIGN KEY([UserInfo_Id])
REFERENCES [dbo].[UserInfoes] ([Id])
GO
ALTER TABLE [dbo].[ImportantEvents] CHECK CONSTRAINT [FK_dbo.ImportantEvents_dbo.UserInfoes_UserInfo_Id]
GO
ALTER TABLE [dbo].[ImportantEvents]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ImportantEvents_dbo.Users_User_Id] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ImportantEvents] CHECK CONSTRAINT [FK_dbo.ImportantEvents_dbo.Users_User_Id]
GO
ALTER TABLE [dbo].[PostComments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PostComments_dbo.Posts_Post_Id] FOREIGN KEY([Post_Id])
REFERENCES [dbo].[Posts] ([Id])
GO
ALTER TABLE [dbo].[PostComments] CHECK CONSTRAINT [FK_dbo.PostComments_dbo.Posts_Post_Id]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.UserInfoes_UserFullInfo_Id] FOREIGN KEY([UserFullInfo_Id])
REFERENCES [dbo].[UserInfoes] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.UserInfoes_UserFullInfo_Id]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.UserSettings_Settings_Id] FOREIGN KEY([Settings_Id])
REFERENCES [dbo].[UserSettings] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.UserSettings_Settings_Id]
GO
ALTER TABLE [dbo].[WorkPlaces]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkPlaces_dbo.UserInfoes_UserInfo_Id] FOREIGN KEY([UserInfo_Id])
REFERENCES [dbo].[UserInfoes] ([Id])
GO
ALTER TABLE [dbo].[WorkPlaces] CHECK CONSTRAINT [FK_dbo.WorkPlaces_dbo.UserInfoes_UserInfo_Id]
GO
ALTER TABLE [dbo].[WorkPlaces]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WorkPlaces_dbo.Users_User_Id] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[WorkPlaces] CHECK CONSTRAINT [FK_dbo.WorkPlaces_dbo.Users_User_Id]
GO
