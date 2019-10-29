USE [u0823102_slugDtb]
GO
/****** Object:  Table [dbo].[ActivationLinks]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[Albums]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[Avatars]    Script Date: 29.10.2019 14:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Avatars](
	[Id] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[BlockedUsersEntries]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[Cities]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[ConversationGroups]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[Conversations]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[Countries]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[DisableDialogs]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[Educations]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[Feedbacks]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[FotoComments]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[Fotoes]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[ImportantEvents]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[Messages]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[Notifications]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[PostComments]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[Posts]    Script Date: 29.10.2019 14:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [uniqueidentifier] NOT NULL,
	[PublicDateTime] [datetime] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[UserPosted_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Posts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResetPasswords]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[SecretChatGroups]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[SecretChats]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[SecretMessages]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[Sessions]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[SessionTypes]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[UserConnections]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[UserInfoes]    Script Date: 29.10.2019 14:36:48 ******/
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
 CONSTRAINT [PK_dbo.UserInfoes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 29.10.2019 14:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](max) NOT NULL,
	[UserStatus] [int] NOT NULL,
	[AvatarId] [int] NULL,
	[UserType] [int] NOT NULL,
	[Settings_Id] [int] NULL,
	[UserFullInfo_Id] [int] NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSettings]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[UsersRelations]    Script Date: 29.10.2019 14:36:48 ******/
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
/****** Object:  Table [dbo].[UserStatus]    Script Date: 29.10.2019 14:36:49 ******/
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
/****** Object:  Table [dbo].[VideoConferenceGroups]    Script Date: 29.10.2019 14:36:49 ******/
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
/****** Object:  Table [dbo].[VideoConferences]    Script Date: 29.10.2019 14:36:49 ******/
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
/****** Object:  Table [dbo].[VkOAuthTokens]    Script Date: 29.10.2019 14:36:49 ******/
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
/****** Object:  Table [dbo].[WorkPlaces]    Script Date: 29.10.2019 14:36:49 ******/
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
SET IDENTITY_INSERT [dbo].[ActivationLinks] ON 

INSERT [dbo].[ActivationLinks] ([Id], [UserId], [ActivationParam], [IsExpired]) VALUES (1, 8, N'7cb34c46c57538ba03c20267c4ddb47d7d5651c0946c336695c3d7e27bedc8566dc0634c7dcbd7663c6b78bc54ebcc4b3ddccdd73476d46d06d47e67', 0)
INSERT [dbo].[ActivationLinks] ([Id], [UserId], [ActivationParam], [IsExpired]) VALUES (2, 9, N'0f2bc952faee24e765a8ffa868a8cfa8e8a15fb87225ffc508be5428af227e9e7042728f9814f582ef825f0188ffa6680247918ebbb250b5007ffb99', 1)
INSERT [dbo].[ActivationLinks] ([Id], [UserId], [ActivationParam], [IsExpired]) VALUES (3, 10, N'e799cce5ee96fec1b5517e70c7be673e0e6b59b60938ba559be16cb3779ee9e9972c59e1e11e157b1b98967367b3c6e1a96479866e17be58e1e6cc2a', 1)
SET IDENTITY_INSERT [dbo].[ActivationLinks] OFF
SET IDENTITY_INSERT [dbo].[Avatars] ON 

INSERT [dbo].[Avatars] ([Id], [UploadTime], [IsStandart], [CountryCode], [LargeAvatar], [MediumAvatar], [SmallAvatar], [AvatarType]) VALUES (1, CAST(N'2019-10-29T13:54:22.947' AS DateTime), 1, 375, N'https://res.cloudinary.com/dlk1sqmj4/image/upload/v1569835906/system/standart_avatars/no-avatar-bel.jpg', N'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_100,w_100,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-bel.jpg', N'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_50,w_50,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-bel.jpg', 0)
INSERT [dbo].[Avatars] ([Id], [UploadTime], [IsStandart], [CountryCode], [LargeAvatar], [MediumAvatar], [SmallAvatar], [AvatarType]) VALUES (2, CAST(N'2019-10-29T13:54:23.170' AS DateTime), 1, 7, N'https://res.cloudinary.com/dlk1sqmj4/image/upload/v1569835906/system/standart_avatars/no-avatar-ru.jpg', N'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_100,w_100,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-ru.jpg', N'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_50,w_50,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-ru.jpg', 0)
INSERT [dbo].[Avatars] ([Id], [UploadTime], [IsStandart], [CountryCode], [LargeAvatar], [MediumAvatar], [SmallAvatar], [AvatarType]) VALUES (3, CAST(N'2019-10-29T13:54:23.170' AS DateTime), 1, 380, N'https://res.cloudinary.com/dlk1sqmj4/image/upload/v1569835906/system/standart_avatars/no-avatar-ua.jpg', N'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_100,w_100,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-ua.jpg', N'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_50,w_50,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-ua.jpg', 0)
SET IDENTITY_INSERT [dbo].[Avatars] OFF
SET IDENTITY_INSERT [dbo].[Cities] ON 

INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1, 163, 375, 2, N'БАРАНОВИЧИ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2, 2232, 375, 2, N'Белыничи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (3, 1643, 375, 2, N'Береза')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (4, 1715, 375, 2, N'Березино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (5, 1511, 375, 2, N'Берестовица')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (6, 2131, 375, 2, N'Бешенковичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (7, 225, 375, 2, N'БОБРУЙСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (8, 177, 375, 2, N'БОРИСОВ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (9, 2344, 375, 2, N'Брагин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (10, 2153, 375, 2, N'Браслав')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (11, 162, 375, 2, N'БРЕСТ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (12, 2336, 375, 2, N'Буда-Кошелево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (13, 2231, 375, 2, N'Быхов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (14, 2151, 375, 2, N'Верхнедвинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (15, 2330, 375, 2, N'Ветка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (16, 1771, 375, 2, N'Вилейка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (17, 212, 375, 2, N'ВИТЕБСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (18, 1512, 375, 2, N'Волковыск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (19, 1772, 375, 2, N'Воложин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (20, 1594, 375, 2, N'Вороново')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (21, 1646, 375, 2, N'Ганцевичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (22, 2156, 375, 2, N'Глубокое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (23, 2230, 375, 2, N'Глуск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (24, 2322, 375, 2, N'ГОМЕЛЬ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (25, 2233, 375, 2, N'Горки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (26, 2139, 375, 2, N'Городок')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (27, 1522, 375, 2, N'ГРОДНО')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (28, 1716, 375, 2, N'Дзержинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (29, 2333, 375, 2, N'Добруш')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (30, 2157, 375, 2, N'Докшицы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (31, 2248, 375, 2, N'Дрибин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (32, 1644, 375, 2, N'Дрогичин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (33, 2137, 375, 2, N'Дубровно')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (34, 1563, 375, 2, N'Дятлово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (35, 2354, 375, 2, N'Ельск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (36, 1641, 375, 2, N'Жабинка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (37, 2353, 375, 2, N'Житковичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (38, 2334, 375, 2, N'ЖЛОБИН')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (39, 1775, 375, 2, N'Жодино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (40, 1564, 375, 2, N'Зельва')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (41, 1652, 375, 2, N'Иваново')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (42, 1645, 375, 2, N'Иванцевичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (43, 1595, 375, 2, N'Ивье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (44, 2345, 375, 2, N'Калинковичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (45, 1631, 375, 2, N'Каменец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (46, 2237, 375, 2, N'Кировск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (47, 1793, 375, 2, N'Клецк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (48, 2244, 375, 2, N'Климовичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (49, 2236, 375, 2, N'Кличев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (50, 1642, 375, 2, N'КОБРИН')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (51, 1719, 375, 2, N'Копыль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (52, 1596, 375, 2, N'Кореличи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (53, 2337, 375, 2, N'Корма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (54, 2245, 375, 2, N'Костюковичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (55, 2238, 375, 2, N'Краснополье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (56, 2241, 375, 2, N'Кричев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (57, 2234, 375, 2, N'Круглое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (58, 1796, 375, 2, N'Крупки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (59, 2356, 375, 2, N'Лельчицы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (60, 2132, 375, 2, N'Лепель')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (61, 1561, 375, 2, N'ЛИДА')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (62, 2138, 375, 2, N'Лиозно')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (63, 1774, 375, 2, N'Логойск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (64, 2347, 375, 2, N'Лоев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (65, 1647, 375, 2, N'Лунинец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (66, 1794, 375, 2, N'Любань')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (67, 1633, 375, 2, N'Ляховичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (68, 1651, 375, 2, N'Малорита')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (69, 1713, 375, 2, N'Марьина Горка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (70, 17, 375, 2, N'МИНСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (71, 2152, 375, 2, N'Миоры')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (72, 222, 375, 2, N'МОГИЛЁВ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (73, 2351, 375, 2, N'МОЗЫРЬ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (74, 1773, 375, 2, N'МОЛОДЕЧНО')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (75, 1515, 375, 2, N'Мосты')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (76, 2240, 375, 2, N'Мстиславль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (77, 21797, 375, 2, N'Мядель')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (78, 2355, 375, 2, N'Наровля')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (79, 1797, 375, 2, N'Нарочь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (80, 1770, 375, 2, N'Несвиж')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (81, 1597, 375, 2, N'Новогрудок')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (82, 214, 375, 2, N'НОВОПОЛОЦК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (83, 2357, 375, 2, N'Октябрьский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (84, 216, 375, 2, N'ОРША')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (85, 2235, 375, 2, N'Осиповичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (86, 1591, 375, 2, N'Островец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (87, 1593, 375, 2, N'Ошмяны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (88, 2350, 375, 2, N'Петриков')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (89, 165, 375, 2, N'ПИНСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (90, 1779, 375, 2, N'Плещеницы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (91, 2104, 375, 2, N'ПОЛОЦК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (92, 2155, 375, 2, N'Поставы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (93, 1632, 375, 2, N'Пружаны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (94, 2340, 375, 2, N'Речица')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (95, 2339, 375, 2, N'Рогачев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (96, 2159, 375, 2, N'Россоны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (97, 2342, 375, 2, N'Светлогорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (98, 1513, 375, 2, N'Свислоч')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (99, 2135, 375, 2, N'Сенно')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (100, 2246, 375, 2, N'Славгород')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (101, 1562, 375, 2, N'СЛОНИМ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (102, 1795, 375, 2, N'СЛУЦК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (103, 1776, 375, 2, N'Смолевичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (104, 1592, 375, 2, N'Сморгонь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (105, 1710, 375, 2, N'Солигорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (106, 1792, 375, 2, N'Старые Дороги')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (107, 1717, 375, 2, N'Столбцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (108, 1655, 375, 2, N'Столин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (109, 2136, 375, 2, N'Толочин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (110, 1718, 375, 2, N'Узда')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (111, 2158, 375, 2, N'Ушачи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (112, 2346, 375, 2, N'Хойники')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (113, 2247, 375, 2, N'Хотимск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (114, 2242, 375, 2, N'Чаусы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (115, 2133, 375, 2, N'Чашники')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (116, 1714, 375, 2, N'Червень')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (117, 2243, 375, 2, N'Чериков')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (118, 2332, 375, 2, N'Чечерск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (119, 2154, 375, 2, N'Шарковщина')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (120, 2239, 375, 2, N'Шклов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (121, 2130, 375, 2, N'Шумилино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (122, 1514, 375, 2, N'Щучин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (123, 39022, 7, 2, N'АБАКАН')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (124, 39041, 7, 2, N'Белый Яр')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (125, 39044, 7, 2, N'Бея')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (126, 39034, 7, 2, N'Боград')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (127, 39036, 7, 2, N'Копьево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (128, 39042, 7, 2, N'Саяногорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (129, 39033, 7, 2, N'Сорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (130, 39046, 7, 2, N'Таштып')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (131, 39032, 7, 2, N'Усть-Абакан')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (132, 39031, 7, 2, N'Черногорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (133, 39035, 7, 2, N'Шира')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (134, 8182, 7, 2, N'АРХАНГЕЛЬСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (135, 81831, 7, 2, N'Березник')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (136, 81836, 7, 2, N'Вельск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (137, 81854, 7, 2, N'Верхняя Тойма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (138, 81843, 7, 2, N'Ильинско-Подомское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (139, 81841, 7, 2, N'Каргополь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (140, 81856, 7, 2, N'Карпогоры')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (141, 81858, 7, 2, N'Коноша')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (142, 81850, 7, 2, N'Коряжма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (143, 81837, 7, 2, N'Котлас')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (144, 81840, 7, 2, N'Красноборск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (145, 81833, 7, 2, N'Лешуконское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (146, 81848, 7, 2, N'Мезень')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (147, 81834, 7, 2, N'Мирный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (148, 81853, 7, 2, N'Нарьян-Мар')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (149, 81852, 7, 2, N'Новодвинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (150, 81838, 7, 2, N'Няндома')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (151, 81855, 7, 2, N'Октябрьский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (152, 81839, 7, 2, N'Онега')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (153, 81832, 7, 2, N'Плесецк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (154, 81842, 7, 2, N'Северодвинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (155, 81835, 7, 2, N'о.Соловки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (156, 81830, 7, 2, N'Холмогоры')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (157, 81851, 7, 2, N'Шенкурск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (158, 81859, 7, 2, N'Яренск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (159, 8512, 7, 2, N'АСТРАХАНЬ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (160, 85111, 7, 2, N'Ахтубинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (161, 85112, 7, 2, N'Володарский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (162, 85113, 7, 2, N'Енотаевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (163, 85114, 7, 2, N'Икряное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (164, 85110, 7, 2, N'Знаменск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (165, 85115, 7, 2, N'Камызяк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (166, 85116, 7, 2, N'Красный Яр')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (167, 85117, 7, 2, N'Лиман')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (168, 85171, 7, 2, N'Нариманов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (169, 85172, 7, 2, N'Началово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (170, 85118, 7, 2, N'Харабли')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (171, 85119, 7, 2, N'Черный Яр')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (172, 3852, 7, 2, N'БАРНАУЛ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (173, 38553, 7, 2, N'Алейск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (174, 3854, 7, 2, N'Бийск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (175, 38595, 7, 2, N'Заринск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (176, 38514, 7, 2, N'Камень на Оби')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (177, 38532, 7, 2, N'Новоалтайск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (178, 38511, 7, 2, N'Павловск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (179, 722, 7, 2, N'БЕЛГОРОД')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (180, 7234, 7, 2, N'Алексеевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (181, 7246, 7, 2, N'Борисовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (182, 7236, 7, 2, N'Валуйки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (183, 7237, 7, 2, N'Вейделевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (184, 7235, 7, 2, N'Волоконовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (185, 7241, 7, 2, N'Губкин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (186, 7243, 7, 2, N'Ивня')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (187, 7231, 7, 2, N'Короча')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (188, 7247, 7, 2, N'Красногвардейское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (189, 7262, 7, 2, N'Красное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (190, 7233, 7, 2, N'Новый Оскол')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (191, 7242, 7, 2, N'Прохоровка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (192, 7245, 7, 2, N'Ракитное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (193, 7238, 7, 2, N'Ровеньки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (194, 725, 7, 2, N'Старый Оскол')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (195, 7244, 7, 2, N'Строитель')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (196, 7232, 7, 2, N'Чернянка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (197, 7248, 7, 2, N'Щебекино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (198, 4162, 7, 2, N'БЛАГОВЕЩЕНСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (199, 832, 7, 2, N'БРЯНСК')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (200, 8341, 7, 2, N'Выгоничи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (201, 8344, 7, 2, N'Жирятино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (202, 8334, 7, 2, N'Жуковка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (203, 8358, 7, 2, N'Злынка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (204, 8335, 7, 2, N'Карачев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (205, 8338, 7, 2, N'Клетня')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (206, 8347, 7, 2, N'Климово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (207, 8336, 7, 2, N'Клинцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (208, 8355, 7, 2, N'Комаричи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (209, 8346, 7, 2, N'Красная Гора')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (210, 8354, 7, 2, N'Локоть')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (211, 8339, 7, 2, N'Мглин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (212, 8343, 7, 2, N'Новозыбков')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (213, 8349, 7, 2, N'Погар')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (214, 8345, 7, 2, N'Почеп')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (215, 8331, 7, 2, N'Рогнедино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (216, 8356, 7, 2, N'Севск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (217, 8348, 7, 2, N'Стародуб')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (218, 8353, 7, 2, N'Суземка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (219, 8351, 7, 2, N'Унеча')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (220, 4232, 7, 2, N'ВЛАДИВОСТОК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (221, 42362, 7, 2, N'Анучино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (222, 42361, 7, 2, N'Арсеньев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (223, 423370, 7, 2, N'Артем')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (224, 42335, 7, 2, N'Большой Камень')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (225, 42373, 7, 2, N'Дальнегорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (226, 42356, 7, 2, N'Дальнереченск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (227, 42375, 7, 2, N'Кавалерово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (228, 42349, 7, 2, N'Камень-Рыболов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (229, 42354, 7, 2, N'Кировский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (230, 42377, 7, 2, N'Лазо')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (231, 42355, 7, 2, N'Лесозаводск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (232, 42357, 7, 2, N'Лучегорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (233, 42346, 7, 2, N'Михайловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (234, 42366, 7, 2, N'Находка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (235, 42359, 7, 2, N'Новопокровка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (236, 42376, 7, 2, N'Ольга')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (237, 423630, 7, 2, N'Партизанск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (238, 42345, 7, 2, N'Пограничный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (239, 42344, 7, 2, N'Покровка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (240, 42352, 7, 2, N'Спасск-Дальний')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (241, 42331, 7, 2, N'Славянка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (242, 42374, 7, 2, N'Терней')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (243, 42341, 7, 2, N'Уссурийск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (244, 42339, 7, 2, N'Фокино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (245, 42347, 7, 2, N'Хороль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (246, 42351, 7, 2, N'Черниговка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (247, 42372, 7, 2, N'Чугуевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (248, 42371, 7, 2, N'Яковлевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (249, 8672, 7, 2, N'ВЛАДИКАВКАЗ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (250, 86731, 7, 2, N'Алагир')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (251, 86732, 7, 2, N'Ардон')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (252, 86739, 7, 2, N'Архонская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (253, 86737, 7, 2, N'Беслан')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (254, 86733, 7, 2, N'Дигора')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (255, 86736, 7, 2, N'Моздок')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (256, 86738, 7, 2, N'Октябрьское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (257, 86734, 7, 2, N'Чикола')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (258, 86735, 7, 2, N'Эльхотово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (259, 922, 7, 2, N'ВЛАДИМИР')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (260, 9244, 7, 2, N'Александров')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (261, 9233, 7, 2, N'Вязники')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (262, 9238, 7, 2, N'Гороховец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (263, 9241, 7, 2, N'Гусь Хрустальный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (264, 9248, 7, 2, N'Камешково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (265, 9237, 7, 2, N'Киржач')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (266, 9232, 7, 2, N'Ковров')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (267, 9245, 7, 2, N'Кольчугино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (268, 9236, 7, 2, N'Красная Горбатка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (269, 9247, 7, 2, N'Меленки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (270, 9234, 7, 2, N'Муром')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (271, 9243, 7, 2, N'Петушки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (272, 9254, 7, 2, N'Радужный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (273, 9242, 7, 2, N'Собинка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (274, 9235, 7, 2, N'Судогда')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (275, 9231, 7, 2, N'Суздаль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (276, 9246, 7, 2, N'Юрьев-Польский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (277, 8442, 7, 2, N'ВОЛГОГРАД')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (278, 84446, 7, 2, N'Алексеевская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (279, 84495, 7, 2, N'Быково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (280, 8443, 7, 2, N'Волжский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (281, 84468, 7, 2, N'Городище')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (282, 84461, 7, 2, N'Даниловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (283, 84458, 7, 2, N'Дубовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (284, 84452, 7, 2, N'Елань')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (285, 844542, 7, 2, N'Жирновск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (286, 84467, 7, 2, N'Иловля')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (287, 84472, 7, 2, N'Калач-на-Дону')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (288, 84457, 7, 2, N'Камышин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (289, 84445, 7, 2, N'Киквидзе')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (290, 84466, 7, 2, N'Клетский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (291, 84476, 7, 2, N'Котельниково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (292, 84455, 7, 2, N'Котово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (293, 84462, 7, 2, N'Кумылженская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (294, 84478, 7, 2, N'Ленинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (295, 84463, 7, 2, N'Михайловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (296, 84443, 7, 2, N'Нехаевский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (297, 84494, 7, 2, N'Николаевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (298, 84447, 7, 2, N'Новоаннинский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (299, 84444, 7, 2, N'Новониколаевский')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (300, 84475, 7, 2, N'Октябрьский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (301, 84456, 7, 2, N'Ольховка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (302, 84492, 7, 2, N'Палласовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (303, 84453, 7, 2, N'Рудня')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (304, 84477, 7, 2, N'Светлый Яр')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (305, 84464, 7, 2, N'Серафимович')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (306, 84479, 7, 2, N'Средняя Ахтуба')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (307, 84493, 7, 2, N'Старая Полтавка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (308, 84473, 7, 2, N'Суровикино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (309, 84442, 7, 2, N'Урюпинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (310, 84465, 7, 2, N'Фролово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (311, 84474, 7, 2, N'Чернышковский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (312, 8172, 7, 2, N'ВОЛОГДА')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (313, 81743, 7, 2, N'Бабаево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (314, 81756, 7, 2, N'Белозерск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (315, 81738, 7, 2, N'Великий Устюг')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (316, 81759, 7, 2, N'Верховажье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (317, 81744, 7, 2, N'Вожега')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (318, 81746, 7, 2, N'Вытегра')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (319, 81755, 7, 2, N'Грязовец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (320, 81742, 7, 2, N'Кадуй')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (321, 81757, 7, 2, N'Кириллов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (322, 81740, 7, 2, N'Кичменгский Городок')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (323, 81758, 7, 2, N'Липин Бор')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (324, 81754, 7, 2, N'Никольск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (325, 81747, 7, 2, N'Нюксеница')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (326, 81745, 7, 2, N'Село им.Бабушкина')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (327, 81733, 7, 2, N'Сокол')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (328, 81752, 7, 2, N'Сямжа')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (329, 81748, 7, 2, N'Тарногский Городок')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (330, 81739, 7, 2, N'Тотьма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (331, 81753, 7, 2, N'Устье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (332, 81737, 7, 2, N'Устюжна')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (333, 81732, 7, 2, N'Харовск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (334, 81741, 7, 2, N'Чагода')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (335, 81751, 7, 2, N'Шексна')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (336, 81749, 7, 2, N'Шуйское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (337, 732, 7, 2, N'ВОРОНЕЖ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (338, 7346, 7, 2, N'Анна')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (339, 7350, 7, 2, N'Бобров')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (340, 7366, 7, 2, N'Богучар')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (341, 7354, 7, 2, N'Борисоглебск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (342, 7361, 7, 2, N'Бутурлиновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (343, 7355, 7, 2, N'Верхний Мамон')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (344, 7343, 7, 2, N'Верхняя Хава')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (345, 7356, 7, 2, N'Воробьевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (346, 7363, 7, 2, N'Калач')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (347, 7357, 7, 2, N'Каменка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (348, 7342, 7, 2, N'Каширское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (349, 7391, 7, 2, N'Лиски')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (350, 7370, 7, 2, N'Нижнедевицк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (351, 7341, 7, 2, N'Новая Усмань')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (352, 7364, 7, 2, N'Нововоронежский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (353, 7353, 7, 2, N'Новохоперск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (354, 7375, 7, 2, N'Острогожск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (355, 7362, 7, 2, N'Павловск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (356, 7344, 7, 2, N'Панино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (357, 7365, 7, 2, N'Петропавловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (358, 7394, 7, 2, N'Подгоренский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (359, 7376, 7, 2, N'Поворино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (360, 7340, 7, 2, N'Рамонь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (361, 7396, 7, 2, N'Россошь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (362, 7372, 7, 2, N'Семилуки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (363, 7352, 7, 2, N'Таловая')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (364, 7347, 7, 2, N'Терновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (365, 7345, 7, 2, N'Эртиль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (366, 38822, 7, 2, N'ГОРНО-АЛТАЙСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (367, 3432, 7, 2, N'ЕКАТЕРИНБУРГ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (368, 34346, 7, 2, N'Алапаевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (369, 34363, 7, 2, N'Артемовский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (370, 34395, 7, 2, N'Арти')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (371, 34365, 7, 2, N'Асбест')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (372, 34391, 7, 2, N'Ачит')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (373, 34369, 7, 2, N'Березовский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (374, 34376, 7, 2, N'Богданович')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (375, 34368, 7, 2, N'Верхняя Пышма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (376, 34345, 7, 2, N'Верхняя Салда')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (377, 34319, 7, 2, N'Верхотурье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (378, 34377, 7, 2, N'Заречный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (379, 34355, 7, 2, N'Ирбит')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (380, 34378, 7, 2, N'Каменск-Уральский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (381, 34313, 7, 2, N'Карпинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (382, 34341, 7, 2, N'Качканар')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (383, 34357, 7, 2, N'Кировград')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (384, 34314, 7, 2, N'Краснотурьинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (385, 34343, 7, 2, N'Красноуральск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (386, 34394, 7, 2, N'Красноуфимск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (387, 34344, 7, 2, N'Кушва')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (388, 34356, 7, 2, N'Невьянск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (389, 34396, 7, 2, N'Нижние Серги')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (390, 3435, 7, 2, N'Нижний Тагил')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (391, 34342, 7, 2, N'Нижняя Тура')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (392, 34318, 7, 2, N'Новая Ляля')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (393, 34370, 7, 2, N'Новоуральск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (394, 34392, 7, 2, N'Первоуральск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (395, 34350, 7, 2, N'Полевской')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (396, 34372, 7, 2, N'Пышма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (397, 34397, 7, 2, N'Ревда')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (398, 34310, 7, 2, N'Североуральск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (399, 34315, 7, 2, N'Серов')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (400, 34373, 7, 2, N'Сухой Лог')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (401, 34371, 7, 2, N'Талица')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (402, 34347, 7, 2, N'Таборы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (403, 34360, 7, 2, N'Тавда')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (404, 34367, 7, 2, N'Тугулым')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (405, 34349, 7, 2, N'Туринск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (406, 34361, 7, 2, N'Туринская Слобода')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (407, 34358, 7, 2, N'Шаля')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (408, 932, 7, 2, N'ИВАНОВО')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (409, 9349, 7, 2, N'Верхний Ландех')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (410, 9354, 7, 2, N'Вичуга')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (411, 9355, 7, 2, N'Гаврилов Посад')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (412, 9353, 7, 2, N'Ильинское-Хованское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (413, 9331, 7, 2, N'Кинешма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (414, 9352, 7, 2, N'Комсомольск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (415, 9357, 7, 2, N'Лежнево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (416, 9334, 7, 2, N'Палех')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (417, 9346, 7, 2, N'Пестяки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (418, 9339, 7, 2, N'Приволжск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (419, 9345, 7, 2, N'Пучеж')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (420, 9336, 7, 2, N'Родники')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (421, 9356, 7, 2, N'Савино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (422, 9343, 7, 2, N'Тейково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (423, 9341, 7, 2, N'Фурманов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (424, 9351, 7, 2, N'Шуя')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (425, 9347, 7, 2, N'Южа')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (426, 9337, 7, 2, N'Юрьевец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (427, 3412, 7, 2, N'ИЖЕВСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (428, 34150, 7, 2, N'Алнаши')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (429, 34166, 7, 2, N'Балезино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (430, 34155, 7, 2, N'Вавож')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (431, 34145, 7, 2, N'Воткинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (432, 34141, 7, 2, N'Глазов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (433, 34163, 7, 2, N'Грахово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (434, 34151, 7, 2, N'Дебесы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (435, 34123, 7, 2, N'Завьялово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (436, 34134, 7, 2, N'Игра')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (437, 34153, 7, 2, N'Камбарка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (438, 34132, 7, 2, N'Каракулино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (439, 34158, 7, 2, N'Кез')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (440, 34154, 7, 2, N'Кизнер')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (441, 34133, 7, 2, N'Киясово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (442, 34164, 7, 2, N'Красногорское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (443, 34138, 7, 2, N'Малая Пурга')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (444, 34139, 7, 2, N'Можга')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (445, 34147, 7, 2, N'Сарапул')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (446, 34159, 7, 2, N'Селты')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (447, 34152, 7, 2, N'Сюмси')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (448, 34130, 7, 2, N'Ува')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (449, 34136, 7, 2, N'Шаркан')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (450, 34161, 7, 2, N'Юкаменское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (451, 34162, 7, 2, N'Якшур-Бодья')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (452, 34157, 7, 2, N'Яр')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (453, 3952, 7, 2, N'ИРКУТСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (454, 39566, 7, 2, N'Железногорск-Илимский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (455, 39568, 7, 2, N'Киренск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (456, 39549, 7, 2, N'Новонукутский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (457, 39535, 7, 2, N'Усть-Илимск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (458, 39546, 7, 2, N'Черемхово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (459, 39510, 7, 2, N'Шелехов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (460, 8362, 7, 2, N'ЙОШКАР-ОЛА')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (461, 83631, 7, 2, N'Волжск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (462, 83645, 7, 2, N'Звенигово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (463, 83632, 7, 2, N'Козьмодемьянск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (464, 83637, 7, 2, N'Куженер')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (465, 83643, 7, 2, N'Килемары')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (466, 83635, 7, 2, N'Морки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (467, 83634, 7, 2, N'Мари-Турек')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (468, 83636, 7, 2, N'Новый Торьял')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (469, 83641, 7, 2, N'Оршанка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (470, 83639, 7, 2, N'Параньга')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (471, 83633, 7, 2, N'Сернур')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (472, 83638, 7, 2, N'Советский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (473, 83644, 7, 2, N'Юрино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (474, 8432, 7, 2, N'КАЗАНЬ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (475, 84344, 7, 2, N'Аксубаево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (476, 84341, 7, 2, N'Алексеевское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (477, 84376, 7, 2, N'Апастово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (478, 84366, 7, 2, N'Арск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (479, 84369, 7, 2, N'Атня')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (480, 84346, 7, 2, N'Базарные Матаки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (481, 84368, 7, 2, N'Балтаси')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (482, 84362, 7, 2, N'Богатые Сабы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (483, 84347, 7, 2, N'Булгар')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (484, 84374, 7, 2, N'Буинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (485, 84379, 7, 2, N'Верхний Услон')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (486, 84365, 7, 2, N'Высокая Гора')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (487, 84371, 7, 2, N'Зеленодольск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (488, 84377, 7, 2, N'Камское Устье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (489, 84364, 7, 2, N'Кукмор')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (490, 84378, 7, 2, N'Лаишево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (491, 84348, 7, 2, N'Новошешминск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (492, 84345, 7, 2, N'Нурлат')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (493, 84367, 7, 2, N'Пестрецы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (494, 84361, 7, 2, N'Рыбная Слобода')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (495, 84375, 7, 2, N'Старое Дрожжаное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (496, 84373, 7, 2, N'Тетюши')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (497, 84360, 7, 2, N'Тюлячи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (498, 84316, 7, 2, N'Черемшан')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (499, 84342, 7, 2, N'Чистополь')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (500, 112, 7, 2, N'КАЛИНИНГРАД')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (501, 1156, 7, 2, N'Багратионовск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (502, 1145, 7, 2, N'Балтийск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (503, 1159, 7, 2, N'Гвардейск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (504, 1151, 7, 2, N'Гурьевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (505, 1143, 7, 2, N'Гусев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (506, 1150, 7, 2, N'Зеленоградск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (507, 1164, 7, 2, N'Краснознаменск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (508, 1162, 7, 2, N'Неман')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (509, 1144, 7, 2, N'Нестеров')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (510, 1142, 7, 2, N'Озерск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (511, 1153, 7, 2, N'Пионерский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (512, 1158, 7, 2, N'Полесск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (513, 1157, 7, 2, N'Правдинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (514, 11533, 7, 2, N'Светлогорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (515, 1152, 7, 2, N'Светлый')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (516, 1163, 7, 2, N'Славск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (517, 1161, 7, 2, N'Советск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (518, 1141, 7, 2, N'Черняховск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (519, 4842, 7, 2, N'КАЛУГА')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (520, 48448, 7, 2, N'Бабынино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (521, 48458, 7, 2, N'Балабаново')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (522, 48438, 7, 2, N'Боровск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (523, 48432, 7, 2, N'Жуково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (524, 48456, 7, 2, N'Киров')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (525, 48434, 7, 2, N'Кондрово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (526, 48444, 7, 2, N'Людиново')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (527, 48431, 7, 2, N'Малоярославец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (528, 48433, 7, 2, N'Медынь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (529, 48446, 7, 2, N'Мещовск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (530, 48439, 7, 2, N'Обнинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (531, 48441, 7, 2, N'Перемышль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (532, 48455, 7, 2, N'Спас-Деменск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (533, 48451, 7, 2, N'Сухиничи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (534, 48435, 7, 2, N'Таруса')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (535, 48443, 7, 2, N'Ульяново')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (536, 48436, 7, 2, N'Юхнов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (537, 3842, 7, 2, N'КЕМЕРОВО')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (538, 38453, 7, 2, N'Анжеро-Судженск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (539, 38452, 7, 2, N'Белово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (540, 38445, 7, 2, N'Березовский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (541, 38444, 7, 2, N'Верх-Чебула')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (542, 38463, 7, 2, N'Гурьевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (543, 38459, 7, 2, N'Ижморский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (544, 38464, 7, 2, N'Кисилевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (545, 38456, 7, 2, N'Ленинск-Кузнецкий')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (546, 38443, 7, 2, N'Мариинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (547, 38475, 7, 2, N'Междуреченск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (548, 38474, 7, 2, N'Мыски')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (549, 3843, 7, 2, N'Новокузнецк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (550, 38471, 7, 2, N'Осинники')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (551, 38466, 7, 2, N'Прокопьевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (552, 38442, 7, 2, N'Промышленная')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (553, 38473, 7, 2, N'Таштагол')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (554, 38447, 7, 2, N'Тисуль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (555, 38454, 7, 2, N'Топки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (556, 38451, 7, 2, N'Юрга')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (557, 38455, 7, 2, N'Яшкино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (558, 38441, 7, 2, N'Яя')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (559, 8332, 7, 2, N'КИРОВ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (560, 83330, 7, 2, N'Арбаж')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (561, 83364, 7, 2, N'Белая Холуница')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (562, 83333, 7, 2, N'Богородское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (563, 83335, 7, 2, N'Верхошижемье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (564, 83334, 7, 2, N'Вятские Поляны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (565, 83336, 7, 2, N'Даровской')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (566, 83337, 7, 2, N'Зуевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (567, 83341, 7, 2, N'Кикнур')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (568, 83338, 7, 2, N'Кильмезь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (569, 83361, 7, 2, N'Кирово-Чепецк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (570, 83339, 7, 2, N'Кирс')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (571, 83342, 7, 2, N'Котельнич')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (572, 83343, 7, 2, N'Кумены')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (573, 83344, 7, 2, N'Лебяжье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (574, 83345, 7, 2, N'Ленинское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (575, 83346, 7, 2, N'Луза')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (576, 83347, 7, 2, N'Малмыж')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (577, 83348, 7, 2, N'Мураши')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (578, 83349, 7, 2, N'Нагорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (579, 83350, 7, 2, N'Нема')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (580, 83368, 7, 2, N'Нолинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (581, 83352, 7, 2, N'Омутнинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (582, 83353, 7, 2, N'Опарино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (583, 83354, 7, 2, N'Оричи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (584, 83365, 7, 2, N'Орлов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (585, 83355, 7, 2, N'Пижанка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (586, 83351, 7, 2, N'Подосиновец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (587, 83357, 7, 2, N'Санчурск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (588, 83358, 7, 2, N'Свеча')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (589, 83362, 7, 2, N'Слободской')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (590, 83375, 7, 2, N'Советск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (591, 83369, 7, 2, N'Суна')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (592, 83340, 7, 2, N'Тужа')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (593, 83359, 7, 2, N'Уни')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (594, 83363, 7, 2, N'Уржум')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (595, 83332, 7, 2, N'Фаленки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (596, 83366, 7, 2, N'Юрья')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (597, 83367, 7, 2, N'Яранск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (598, 942, 7, 2, N'КОСТРОМА')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (599, 9430, 7, 2, N'Антропово')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (600, 9451, 7, 2, N'Боговарово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (601, 9435, 7, 2, N'Буй')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (602, 9453, 7, 2, N'Волгореченск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (603, 9450, 7, 2, N'Вохма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (604, 9437, 7, 2, N'Галич')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (605, 9447, 7, 2, N'Георгиевское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (606, 9442, 7, 2, N'Кадый')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (607, 9443, 7, 2, N'Кологрив')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (608, 9432, 7, 2, N'Красное-на-Волге')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (609, 944577, 7, 2, N'Макарьев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (610, 9446, 7, 2, N'Мантурово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (611, 9431, 7, 2, N'Нерехта')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (612, 9444, 7, 2, N'Нея')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (613, 9438, 7, 2, N'Островское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (614, 9439, 7, 2, N'Павино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (615, 9440, 7, 2, N'Парфеньево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (616, 9448, 7, 2, N'Поназырево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (617, 9452, 7, 2, N'Пыщуг')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (618, 9436, 7, 2, N'Солигалич')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (619, 9433, 7, 2, N'Судиславль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (620, 9434, 7, 2, N'Сусанино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (621, 9441, 7, 2, N'Чухлома')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (622, 9449, 7, 2, N'Шарья')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (623, 8612, 7, 2, N'КРАСНОДАР')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (624, 86150, 7, 2, N'Абинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (625, 86133, 7, 2, N'Анапа')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (626, 86152, 7, 2, N'Апшеронск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (627, 86137, 7, 2, N'Армавир')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (628, 86155, 7, 2, N'Белореченск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (629, 86141, 7, 2, N'Геленджик')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (630, 86159, 7, 2, N'Горячий Ключ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (631, 86162, 7, 2, N'Динская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (632, 86164, 7, 2, N'Каневская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (633, 86160, 7, 2, N'Гулькевичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (634, 86142, 7, 2, N'Кореновск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (635, 86138, 7, 2, N'Кропоткин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (636, 86147, 7, 2, N'Курганинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (637, 86169, 7, 2, N'Лабинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (638, 86192, 7, 2, N'Мостовской')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (639, 86143, 7, 2, N'Приморско-Ахтарск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (640, 86146, 7, 2, N'Славянск-на-Кубани')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (641, 86156, 7, 2, N'Станица Брюховецкая')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (642, 86193, 7, 2, N'Станица Кавказская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (643, 86163, 7, 2, N'Станица Калининская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (644, 86165, 7, 2, N'Станица Красноармейская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (645, 86161, 7, 2, N'Станица Крыловская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (646, 86168, 7, 2, N'Станица Кущевская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (647, 86149, 7, 2, N'Станица Новопокровская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (648, 86144, 7, 2, N'Станица Отрадная')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (649, 86191, 7, 2, N'Станица Павловская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (650, 86151, 7, 2, N'Станица Старощербиновская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (651, 86158, 7, 2, N'Станица Тбилисская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (652, 86130, 7, 2, N'Тимашевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (653, 86196, 7, 2, N'Тихорецк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (654, 86167, 7, 2, N'Туапсе')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (655, 86140, 7, 2, N'Успенское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (656, 86135, 7, 2, N'Усть-Лабинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (657, 3912, 7, 2, N'КРАСНОЯРСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (658, 3916322, 7, 2, N'Абан')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (659, 39151, 7, 2, N'Ачинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (660, 39148, 7, 2, N'Балахта')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (661, 39175, 7, 2, N'Березовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (662, 39118, 7, 2, N'Большая Мурта')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (663, 39159, 7, 2, N'Большой Улуй')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (664, 39168, 7, 2, N'Бородино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (665, 39167, 7, 2, N'Дзержинское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (666, 39144, 7, 2, N'Дивногорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (667, 39133, 7, 2, N'Емельяново')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (668, 39115, 7, 2, N'Енисейск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (669, 39138, 7, 2, N'Ермаковское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (670, 39197, 7, 2, N'Железногорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (671, 39169, 7, 2, N'Зеленогорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (672, 39174, 7, 2, N'Ирбейское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (673, 39116, 7, 2, N'Казачинское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (674, 39161, 7, 2, N'Канск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (675, 39137, 7, 2, N'Каратузское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (676, 39195, 7, 2, N'Кедровый')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (677, 39154, 7, 2, N'Козулька')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (678, 39145, 7, 2, N'Лесосибирск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (679, 39132, 7, 2, N'Минусинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (680, 39171, 7, 2, N'Нижний Ингаш')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (681, 39147, 7, 2, N'Новоселово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (682, 39140, 7, 2, N'Партизанское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (683, 39117, 7, 2, N'Пировское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (684, 39119, 7, 2, N'Сухобузимское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (685, 39164, 7, 2, N'Тасеево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (686, 39158, 7, 2, N'Тюхтет')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (687, 39156, 7, 2, N'Ужур')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (688, 39146, 7, 2, N'Уяр')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (689, 39149, 7, 2, N'Шалинское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (690, 39153, 7, 2, N'Шарыпово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (691, 39139, 7, 2, N'Шушенское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (692, 35222, 7, 2, N'КУРГАН')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (693, 35242, 7, 2, N'Альменево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (694, 35232, 7, 2, N'Белозерское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (695, 35233, 7, 2, N'Варгаши')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (696, 35239, 7, 2, N'Глядянское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (697, 35252, 7, 2, N'Далматово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (698, 35240, 7, 2, N'Звериноголовское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (699, 35256, 7, 2, N'Каргаполье')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (700, 35251, 7, 2, N'Катайск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (701, 35231, 7, 2, N'Кетово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (702, 35249, 7, 2, N'Куртамыш')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (703, 35237, 7, 2, N'Лебяжье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (704, 35236, 7, 2, N'Макушино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (705, 35247, 7, 2, N'Мишкино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (706, 35234, 7, 2, N'Мокроусово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (707, 35235, 7, 2, N'Петухово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (708, 35238, 7, 2, N'Половинное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (709, 35243, 7, 2, N'Сафакулево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (710, 35241, 7, 2, N'Целинное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (711, 35230, 7, 2, N'Частоозерье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (712, 35253, 7, 2, N'Шадринск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (713, 35257, 7, 2, N'Шатрово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (714, 35245, 7, 2, N'Шумиха')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (715, 35244, 7, 2, N'Щучье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (716, 35248, 7, 2, N'Юргамыш')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (717, 7122, 7, 2, N'КУРСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (718, 7149, 7, 2, N'Белая')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (719, 7136, 7, 2, N'Большое Солдатское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (720, 7132, 7, 2, N'Глушково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (721, 7133, 7, 2, N'Горшечное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (722, 7150, 7, 2, N'Дмитриев-Льговский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (723, 7148, 7, 2, N'Железногорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (724, 7151, 7, 2, N'Золотухино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (725, 7157, 7, 2, N'Касторное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (726, 7156, 7, 2, N'Конышевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (727, 7147, 7, 2, N'Коренево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (728, 7131, 7, 2, N'Курчатов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (729, 7158, 7, 2, N'Кшенский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (730, 71400, 7, 2, N'Льгов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (731, 7155, 7, 2, N'Мантурово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (732, 7146, 7, 2, N'Медвенка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (733, 7141, 7, 2, N'Обоянь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (734, 7135, 7, 2, N'Поныри')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (735, 7134, 7, 2, N'Пристень')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (736, 7142, 7, 2, N'Прямицыно')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (737, 7152, 7, 2, N'Рыльск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (738, 7143, 7, 2, N'Суджа')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (739, 7154, 7, 2, N'Солнцево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (740, 7153, 7, 2, N'Тим')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (741, 7144, 7, 2, N'Фатеж')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (742, 7137, 7, 2, N'Хомутовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (743, 7145, 7, 2, N'Щигры')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (744, 7159, 7, 2, N'Черемисиново')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (745, 742, 7, 2, N'ЛИПЕЦК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (746, 7473, 7, 2, N'Волово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (747, 7461, 7, 2, N'Грязи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (748, 7465, 7, 2, N'Данков')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (749, 7462, 7, 2, N'Добринка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (750, 7463, 7, 2, N'Доброе')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (751, 7468, 7, 2, N'Долгоруково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (752, 7467, 7, 2, N'Елец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (753, 7471, 7, 2, N'Задонск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (754, 7478, 7, 2, N'Измалково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (755, 7469, 7, 2, N'Красное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (756, 7466, 7, 2, N'Лебедянь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (757, 7464, 7, 2, N'Лев Толстой')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (758, 7476, 7, 2, N'Становое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (759, 7474, 7, 2, N'Тербуны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (760, 7472, 7, 2, N'Усмань')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (761, 7477, 7, 2, N'Хлевное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (762, 7475, 7, 2, N'Чаплыгин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (763, 87722, 7, 2, N'МАЙКОП')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (764, 41322, 7, 2, N'МАГАДАН')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (765, 41346, 7, 2, N'Омсукчан')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (766, 41342, 7, 2, N'Палатка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (767, 41347, 7, 2, N'Сеймчан')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (768, 41345, 7, 2, N'Сусуман')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (769, 41344, 7, 2, N'Усть-Омчуг')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (770, 8722, 7, 2, N'МАХАЧКАЛА')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (771, 495, 7, 2, N'МОСКВА')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (772, 49636, 7, 2, N'Волоколамск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (773, 49644, 7, 2, N'Воскресенск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (774, 49633, 7, 2, N'Голицыно')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (775, 49622, 7, 2, N'Дмитров')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (776, 49679, 7, 2, N'Домодедово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (777, 49621, 7, 2, N'Дубна')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (778, 49666, 7, 2, N'Зарайск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (779, 49632, 7, 2, N'Звенигород')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (780, 49640, 7, 2, N'Егорьевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (781, 49648, 7, 2, N'Жуковский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (782, 49631, 7, 2, N'Истра')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (783, 49624, 7, 2, N'Клин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (784, 49661, 7, 2, N'Коломна')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (785, 49628, 7, 2, N'Лотошино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (786, 49663, 7, 2, N'Луховицы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (787, 49638, 7, 2, N'Можайск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (788, 49634, 7, 2, N'Наро-Фоминск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (789, 49651, 7, 2, N'Ногинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (790, 49670, 7, 2, N'Озеры')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (791, 4964, 7, 2, N'Орехово-Зуево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (792, 49643, 7, 2, N'Павловский Посад')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (793, 49675, 7, 2, N'Подольск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (794, 49653, 7, 2, N'Пушкино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (795, 49646, 7, 2, N'Раменское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (796, 49627, 7, 2, N'Руза')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (797, 49667, 7, 2, N'Серебряные Пруды')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (798, 49673, 7, 2, N'Серпухов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (799, 49654, 7, 2, N'Сергиев Посад')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (800, 49626, 7, 2, N'Солнечногорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (801, 49664, 7, 2, N'Ступино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (802, 49620, 7, 2, N'Талдом')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (803, 4967, 7, 2, N'Троицк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (804, 49645, 7, 2, N'Шатура')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (805, 49637, 7, 2, N'Шаховская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (806, 49656, 7, 2, N'Щелково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (807, 49672, 7, 2, N'Чехов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (808, 9657, 7, 2, N'Электросталь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (809, 8152, 7, 2, N'МУРМАНСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (810, 81555, 7, 2, N'Апатиты')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (811, 81554, 7, 2, N'Заполярный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (812, 81556, 7, 2, N'Заозерск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (813, 81533, 7, 2, N'Кандалакша')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (814, 81531, 7, 2, N'Кировск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (815, 81535, 7, 2, N'Ковдор')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (816, 81553, 7, 2, N'Кола')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (817, 81538, 7, 2, N'Ловозеро')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (818, 81536, 7, 2, N'Мончегорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (819, 815522, 7, 2, N'Оленегорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (820, 81537, 7, 2, N'Североморск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (821, 81530, 7, 2, N'Снежногорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (822, 815512, 7, 2, N'Полярный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (823, 81532, 7, 2, N'Полярные Зори')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (824, 81559, 7, 2, N'Умба')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (825, 8552, 7, 2, N'НАБ. ЧЕЛНЫ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (826, 85551, 7, 2, N'Агрыз')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (827, 85511, 7, 2, N'Азнакаево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (828, 85552, 7, 2, N'Актаныш')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (829, 8553, 7, 2, N'Альметьевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (830, 85519, 7, 2, N'Бавлы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (831, 85514, 7, 2, N'Бугульма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (832, 85557, 7, 2, N'Елабуга')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (833, 85515, 7, 2, N'Лениногорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (834, 85558, 7, 2, N'Заинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (835, 85563, 7, 2, N'Мамадыш')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (836, 85549, 7, 2, N'Менделеевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (837, 85555, 7, 2, N'Мензелинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (838, 85556, 7, 2, N'Муслюмово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (839, 8555, 7, 2, N'Нижнекамск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (840, 85559, 7, 2, N'Сарманово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (841, 85510, 7, 2, N'Уруссу')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (842, 8312, 7, 2, N'НИЖ. НОВГОРОД')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (843, 83179, 7, 2, N'Ардатов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (844, 83147, 7, 2, N'Арзамас')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (845, 83144, 7, 2, N'Балахна')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (846, 83170, 7, 2, N'Богородск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (847, 83138, 7, 2, N'Большое Болдино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (848, 83167, 7, 2, N'Большое Мурашкино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (849, 83159, 7, 2, N'Бор')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (850, 83172, 7, 2, N'Бутурлино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (851, 83141, 7, 2, N'Вад')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (852, 83173, 7, 2, N'Вача')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (853, 83150, 7, 2, N'Ветлуга')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (854, 83178, 7, 2, N'Вознесенское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (855, 83163, 7, 2, N'Воскресенское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (856, 83177, 7, 2, N'Выкса')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (857, 83195, 7, 2, N'Гагино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (858, 83161, 7, 2, N'Городец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (859, 83168, 7, 2, N'Дальнее Константиново')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (860, 8313, 7, 2, N'Дзержинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (861, 83143, 7, 2, N'Дивеево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (862, 83157, 7, 2, N'Ковернино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (863, 83145, 7, 2, N'Кстово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (864, 83176, 7, 2, N'Кулебаки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (865, 83196, 7, 2, N'Лукоянов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (866, 83149, 7, 2, N'Лысково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (867, 83175, 7, 2, N'Навашино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (868, 83171, 7, 2, N'Павлово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (869, 83146, 7, 2, N'Первомайск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (870, 83148, 7, 2, N'Перевоз')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (871, 83192, 7, 2, N'Пильна')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (872, 83197, 7, 2, N'Починки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (873, 83130, 7, 2, N'Саров')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (874, 83162, 7, 2, N'Семенов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (875, 83191, 7, 2, N'Сергач')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (876, 83193, 7, 2, N'Сеченово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (877, 83174, 7, 2, N'Сосновское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (878, 83165, 7, 2, N'Спасское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (879, 83153, 7, 2, N'Тонкино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (880, 83194, 7, 2, N'Уразовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (881, 83154, 7, 2, N'Урень')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (882, 83160, 7, 2, N'Чкаловск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (883, 83155, 7, 2, N'Шаранга')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (884, 83142, 7, 2, N'Шатки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (885, 83152, 7, 2, N'Шахунья')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (886, 81622, 7, 2, N'ВЕЛИКИЙ НОВГОРОД')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (887, 81661, 7, 2, N'Батецкий')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (888, 81664, 7, 2, N'Боровичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (889, 81666, 7, 2, N'Валдай')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (890, 81662, 7, 2, N'Волот')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (891, 81651, 7, 2, N'Демянск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (892, 81659, 7, 2, N'Крестцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (893, 81668, 7, 2, N'Любытино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (894, 81660, 7, 2, N'Малая Вишера')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (895, 81663, 7, 2, N'Марево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (896, 81653, 7, 2, N'Мошенское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (897, 81657, 7, 2, N'Окуловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (898, 81650, 7, 2, N'Парфино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (899, 81669, 7, 2, N'Пестово')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (900, 81658, 7, 2, N'Поддорье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (901, 81655, 7, 2, N'Сольцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (902, 81652, 7, 2, N'Старая Русса')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (903, 81656, 7, 2, N'Шимск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (904, 81667, 7, 2, N'Хвойная')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (905, 81654, 7, 2, N'Холм')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (906, 81665, 7, 2, N'Чудово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (907, 86117, 7, 2, N'НОВОРОССИЙСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (908, 3832, 7, 2, N'НОВОСИБИРСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (909, 38353, 7, 2, N'Баган')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (910, 383612, 7, 2, N'Барабинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (911, 38341, 7, 2, N'Бердск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (912, 38349, 7, 2, N'Болотное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (913, 38369, 7, 2, N'Венгерово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (914, 38363, 7, 2, N'Здвинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (915, 38343, 7, 2, N'Искитим')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (916, 38355, 7, 2, N'Карасук')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (917, 38365, 7, 2, N'Каргат')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (918, 38352, 7, 2, N'Колывань')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (919, 38351, 7, 2, N'Коченево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (920, 38356, 7, 2, N'Кочки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (921, 38357, 7, 2, N'Краснозерское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (922, 38362, 7, 2, N'Куйбышев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (923, 38358, 7, 2, N'Купино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (924, 38371, 7, 2, N'Кыштовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (925, 38347, 7, 2, N'Маслянино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (926, 38348, 7, 2, N'Мошково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (927, 38373, 7, 2, N'Обь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (928, 38359, 7, 2, N'Ордынское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (929, 38360, 7, 2, N'Северное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (930, 38346, 7, 2, N'Сузун')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (931, 38364, 7, 2, N'Татарск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (932, 38340, 7, 2, N'Тогучин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (933, 38366, 7, 2, N'Убинское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (934, 38372, 7, 2, N'Усть-Тарка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (935, 38367, 7, 2, N'Чаны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (936, 38345, 7, 2, N'Черепаново')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (937, 38368, 7, 2, N'Чистоозерное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (938, 38350, 7, 2, N'Чулым')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (939, 3812, 7, 2, N'ОМСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (940, 38169, 7, 2, N'Большеречье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (941, 38179, 7, 2, N'Знаменское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (942, 38173, 7, 2, N'Исилькуль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (943, 38155, 7, 2, N'Калачинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (944, 38160, 7, 2, N'Колосовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (945, 38170, 7, 2, N'Кормиловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (946, 38167, 7, 2, N'Крутинка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (947, 38175, 7, 2, N'Любинский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (948, 38168, 7, 2, N'Марьяновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (949, 38174, 7, 2, N'Москаленки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (950, 38158, 7, 2, N'Муромцево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (951, 38161, 7, 2, N'Называевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (952, 38152, 7, 2, N'Нововаршавка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (953, 38159, 7, 2, N'Одесское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (954, 38166, 7, 2, N'Оконешниково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (955, 38172, 7, 2, N'Павлоградка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (956, 38163, 7, 2, N'Полтавка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (957, 38156, 7, 2, N'Русская Поляна')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (958, 38178, 7, 2, N'Саргатское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (959, 38164, 7, 2, N'Седельниково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (960, 38151, 7, 2, N'Таврическое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (961, 38171, 7, 2, N'Тара')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (962, 38154, 7, 2, N'Тевриз')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (963, 38176, 7, 2, N'Тюкалинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (964, 38150, 7, 2, N'Усть-Ишим')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (965, 38153, 7, 2, N'Черлак')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (966, 38177, 7, 2, N'Щербакуль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (967, 18622, 7, 2, N'ОРЕЛ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (968, 8640, 7, 2, N'Болхов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (969, 8676, 7, 2, N'Верховье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (970, 8675, 7, 2, N'Глазуновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (971, 8649, 7, 2, N'Дмитровск-Орловск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (972, 8648, 7, 2, N'Залегощь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (973, 8662, 7, 2, N'Знаменка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (974, 8674, 7, 2, N'Колпны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (975, 8643, 7, 2, N'Кромы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (976, 8677, 7, 2, N'Ливны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (977, 8679, 7, 2, N'Малоархангельск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (978, 8646, 7, 2, N'Мценск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (979, 8647, 7, 2, N'Нарышкино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (980, 8664, 7, 2, N'Покровское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (981, 8666, 7, 2, N'Тросно')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (982, 3532, 7, 2, N'ОРЕНБУРГ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (983, 35365, 7, 2, N'Адамовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (984, 35335, 7, 2, N'Акбулак')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (985, 35359, 7, 2, N'Александровка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (986, 35351, 7, 2, N'Асекеево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (987, 35352, 7, 2, N'Бугуруслан')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (988, 35342, 7, 2, N'Бузулук')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (989, 35362, 7, 2, N'Гай')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (990, 35344, 7, 2, N'Грачевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (991, 35337, 7, 2, N'Илек')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (992, 35361, 7, 2, N'Кувандык')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (993, 35341, 7, 2, N'Курманаевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (994, 35372, 7, 2, N'Орск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (995, 35348, 7, 2, N'Первомайский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (996, 35333, 7, 2, N'Саракташ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (997, 35354, 7, 2, N'Северное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (998, 35336, 7, 2, N'Соль-Илецк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (999, 35349, 7, 2, N'Тоцкое')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1000, 35332, 7, 2, N'Тюльган')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1001, 35358, 7, 2, N'Шарлык')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1002, 35368, 7, 2, N'Ясный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1003, 3182, 7, 2, N'ПАВЛОДАР')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1004, 8412, 7, 2, N'ПЕНЗА')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1005, 84143, 7, 2, N'Башмаково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1006, 84151, 7, 2, N'Беднодемьяновск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1007, 84153, 7, 2, N'Белинский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1008, 84140, 7, 2, N'Бессоновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1009, 84158, 7, 2, N'Городище')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1010, 84144, 7, 2, N'Исса')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1011, 84155, 7, 2, N'Земетчино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1012, 84156, 7, 2, N'Каменка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1013, 84146, 7, 2, N'Колышлей')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1014, 84157, 7, 2, N'Кузнецк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1015, 84148, 7, 2, N'Лопатино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1016, 84161, 7, 2, N'Лунино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1017, 84162, 7, 2, N'Малая Сердоба')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1018, 84150, 7, 2, N'Мокшан')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1019, 84163, 7, 2, N'Наровчат')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1020, 84165, 7, 2, N'Никольск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1021, 84152, 7, 2, N'Пачелма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1022, 84145, 7, 2, N'Русский Камешкир')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1023, 84167, 7, 2, N'Сердобск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1024, 84168, 7, 2, N'Сосновоборск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1025, 84169, 7, 2, N'Тамала')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1026, 84159, 7, 2, N'Шемышейка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1027, 3422, 7, 2, N'ПЕРМЬ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1028, 34247, 7, 2, N'Александровск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1029, 34292, 7, 2, N'Барда')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1030, 34242, 7, 2, N'Березники')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1031, 34251, 7, 2, N'Березовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1032, 34257, 7, 2, N'Большая Соснова')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1033, 34254, 7, 2, N'Верещагино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1034, 34269, 7, 2, N'Горнозаводск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1035, 34250, 7, 2, N'Гремячинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1036, 34248, 7, 2, N'Губаха')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1037, 34265, 7, 2, N'Добрянка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1038, 34296, 7, 2, N'Елово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1039, 342633, 7, 2, N'Звездный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1040, 34276, 7, 2, N'Ильинский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1041, 34297, 7, 2, N'Карагай')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1042, 34255, 7, 2, N'Кизел')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1043, 34273, 7, 2, N'Краснокамск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1044, 34260, 7, 2, N'Кудымкар')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1045, 34262, 7, 2, N'Куеда')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1046, 34271, 7, 2, N'Кунгур')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1047, 34249, 7, 2, N'Лысьва')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1048, 34272, 7, 2, N'Нытва')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1049, 34266, 7, 2, N'Октябрьский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1050, 34258, 7, 2, N'Орда')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1051, 34291, 7, 2, N'Оса')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1052, 34279, 7, 2, N'Оханск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1053, 34278, 7, 2, N'Очер')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1054, 34277, 7, 2, N'Сива')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1055, 34253, 7, 2, N'Соликамск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1056, 34275, 7, 2, N'Суксун')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1057, 34259, 7, 2, N'Уинское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1058, 34244, 7, 2, N'Усолье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1059, 34241, 7, 2, N'Чайковский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1060, 34268, 7, 2, N'Частые')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1061, 34261, 7, 2, N'Чернушка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1062, 34240, 7, 2, N'Чердынь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1063, 34256, 7, 2, N'Чусовой')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1064, 8142, 7, 2, N'ПЕТРОЗАВОДСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1065, 81437, 7, 2, N'Беломорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1066, 81454, 7, 2, N'Калевала')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1067, 81458, 7, 2, N'Кемь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1068, 81451, 7, 2, N'Кондопога')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1069, 81459, 7, 2, N'Костомукша')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1070, 81450, 7, 2, N'Лахденпохья')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1071, 81439, 7, 2, N'Лоухи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1072, 81434, 7, 2, N'Медвежьегорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1073, 81455, 7, 2, N'Муезерский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1074, 81436, 7, 2, N'Олонец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1075, 81433, 7, 2, N'Питкяранта')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1076, 81456, 7, 2, N'Пряжа')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1077, 81452, 7, 2, N'Пудож')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1078, 81431, 7, 2, N'Сегежа')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1079, 81430, 7, 2, N'Сортавала')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1080, 81457, 7, 2, N'Суоярви')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1081, 81122, 7, 2, N'ПСКОВ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1082, 81141, 7, 2, N'Бежаницы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1083, 81153, 7, 2, N'Великие Луки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1084, 81131, 7, 2, N'Гдов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1085, 81136, 7, 2, N'Дедовичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1086, 81135, 7, 2, N'Дно')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1087, 81137, 7, 2, N'Красногородское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1088, 81149, 7, 2, N'Кунья')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1089, 81139, 7, 2, N'Локня')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1090, 81151, 7, 2, N'Невель')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1091, 81143, 7, 2, N'Новоржев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1092, 81144, 7, 2, N'Новосокольники')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1093, 81138, 7, 2, N'Опочка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1094, 81152, 7, 2, N'Остров')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1095, 81145, 7, 2, N'Палкино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1096, 81148, 7, 2, N'Печоры')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1097, 81133, 7, 2, N'Плюсса')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1098, 81134, 7, 2, N'Порхов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1099, 81142, 7, 2, N'Пустошка')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1100, 81146, 7, 2, N'Пушкинские Горы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1101, 81147, 7, 2, N'Пыталово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1102, 81140, 7, 2, N'Себеж')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1103, 81132, 7, 2, N'Струги Красные')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1104, 81150, 7, 2, N'Усвяты')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1105, 8632, 7, 2, N'РОСТОВ-НА-ДОНУ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1106, 86342, 7, 2, N'Азов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1107, 86350, 7, 2, N'Аксай')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1108, 86357, 7, 2, N'Багаевский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1109, 86354, 7, 2, N'Батайск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1110, 86313, 7, 2, N'Белая Калитва')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1111, 86312, 7, 2, N'Боковская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1112, 86395, 7, 2, N'Большая Мартыновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1113, 86358, 7, 2, N'Веселый')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1114, 86311, 7, 2, N'Вешенская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1115, 86392, 7, 2, N'Волгодонск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1116, 86368, 7, 2, N'Донецк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1117, 86377, 7, 2, N'Дубовское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1118, 86370, 7, 2, N'Егорлыкская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1119, 86378, 7, 2, N'Заветное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1120, 86359, 7, 2, N'Зерноград')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1121, 86376, 7, 2, N'Зимовники')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1122, 86345, 7, 2, N'Кагальницкая')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1123, 86365, 7, 2, N'Каменск-Шахтинский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1124, 86393, 7, 2, N'Константиновск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1125, 86348, 7, 2, N'Куйбышево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1126, 86341, 7, 2, N'Матвеев Курган')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1127, 86315, 7, 2, N'Миллерово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1128, 86319, 7, 2, N'Милютинская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1129, 86314, 7, 2, N'Морозовск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1130, 86352, 7, 2, N'Новочеркасск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1131, 86369, 7, 2, N'Новошахтинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1132, 86375, 7, 2, N'Орловский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1133, 86396, 7, 2, N'Обливская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1134, 86347, 7, 2, N'Покровское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1135, 86379, 7, 2, N'Ремонтное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1136, 86340, 7, 2, N'Родионово-Несветайская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1137, 86394, 7, 2, N'Романовская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1138, 86372, 7, 2, N'Сальск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1139, 86356, 7, 2, N'Семикаракорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1140, 86344, 7, 2, N'Таганрог')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1141, 86316, 7, 2, N'Тарасовский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1142, 86351, 7, 2, N'Усть-Донецкий')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1143, 86391, 7, 2, N'Цимлянск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1144, 86349, 7, 2, N'Чалтырь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1145, 86317, 7, 2, N'Чертково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1146, 86362, 7, 2, N'Шахты')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1147, 912, 7, 2, N'РЯЗАНЬ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1148, 9144, 7, 2, N'Ермишь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1149, 9139, 7, 2, N'Кадом')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1150, 9131, 7, 2, N'Касимов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1151, 9143, 7, 2, N'Кораблино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1152, 9157, 7, 2, N'Милославское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1153, 9130, 7, 2, N'Михайлов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1154, 9141, 7, 2, N'Новомичуринск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1155, 9145, 7, 2, N'Пителино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1156, 9146, 7, 2, N'Путятино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1157, 9137, 7, 2, N'Рыбное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1158, 9132, 7, 2, N'Ряжск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1159, 9152, 7, 2, N'Сапожок')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1160, 9133, 7, 2, N'Сасово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1161, 9156, 7, 2, N'Скопин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1162, 9134, 7, 2, N'Солотча')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1163, 9142, 7, 2, N'Спас-Клепики')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1164, 9135, 7, 2, N'Спасск-Рязанский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1165, 9151, 7, 2, N'Старожилово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1166, 9154, 7, 2, N'Ухолово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1167, 9138, 7, 2, N'Чучково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1168, 9147, 7, 2, N'Шацк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1169, 9136, 7, 2, N'Шилово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1170, 8462, 7, 2, N'САМАРА')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1171, 84671, 7, 2, N'Алексеевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1172, 84676, 7, 2, N'Безенчук')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1173, 84673, 7, 2, N'Большая Глушица')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1174, 84672, 7, 2, N'Большая Черниговка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1175, 84662, 7, 2, N'Жигулевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1176, 84663, 7, 2, N'Кинель')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1177, 84653, 7, 2, N'Клявлино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1178, 84650, 7, 2, N'Кошки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1179, 84657, 7, 2, N'Красный Яр')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1180, 84670, 7, 2, N'Нефтегорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1181, 84635, 7, 2, N'Новокуйбышевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1182, 84661, 7, 2, N'Отрадный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1183, 84656, 7, 2, N'Похвистнево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1184, 84655, 7, 2, N'Сергиевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1185, 8482, 7, 2, N'Тольятти')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1186, 84639, 7, 2, N'Чапаевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1187, 84651, 7, 2, N'Челно-Вершины')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1188, 84652, 7, 2, N'Шентала')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1189, 84648, 7, 2, N'Шигоны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1190, 812, 7, 2, N'САНКТ-ПЕТЕРБУРГ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1191, 81278, 7, 2, N'Выборг')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1192, 81271, 7, 2, N'Гатчина')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1193, 81275, 7, 2, N'Кингисепп')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1194, 81268, 7, 2, N'Кириши')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1195, 81272, 7, 2, N'Луга')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1196, 81266, 7, 2, N'Пикалево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1197, 81279, 7, 2, N'Приозерск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1198, 812274, 7, 2, N'Сланцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1199, 83422, 7, 2, N'САРАНСК')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1200, 83431, 7, 2, N'Ардатов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1201, 83454, 7, 2, N'Атюрьево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1202, 83434, 7, 2, N'Атяшево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1203, 83436, 7, 2, N'Большие Березники')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1204, 83442, 7, 2, N'Большое Игнатово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1205, 83447, 7, 2, N'Дубенки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1206, 83444, 7, 2, N'Ельники')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1207, 83458, 7, 2, N'Зубова Поляна')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1208, 83449, 7, 2, N'Инсар')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1209, 83448, 7, 2, N'Кадошкино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1210, 83433, 7, 2, N'Кемля')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1211, 83453, 7, 2, N'Ковылкино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1212, 83439, 7, 2, N'Кочкурово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1213, 83443, 7, 2, N'Краснослободск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1214, 83441, 7, 2, N'Лямбирь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1215, 83438, 7, 2, N'Ромоданово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1216, 83451, 7, 2, N'Рузаевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1217, 83432, 7, 2, N'Старое Шайгово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1218, 83446, 7, 2, N'Теньгушево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1219, 83445, 7, 2, N'Темников')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1220, 83456, 7, 2, N'Торбеево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1221, 83437, 7, 2, N'Чамзинка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1222, 83457, 7, 2, N'Явас')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1223, 8452, 7, 2, N'САРАТОВ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1224, 84542, 7, 2, N'Аркадак')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1225, 84552, 7, 2, N'Аткарск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1226, 84570, 7, 2, N'Балаково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1227, 84545, 7, 2, N'Балашов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1228, 84592, 7, 2, N'Балтай')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1229, 84593, 7, 2, N'Вольск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1230, 84577, 7, 2, N'Горный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1231, 84563, 7, 2, N'Дергачи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1232, 84573, 7, 2, N'Духовницкое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1233, 84564, 7, 2, N'Ершов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1234, 84579, 7, 2, N'Ивантеевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1235, 84549, 7, 2, N'Калининск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1236, 84550, 7, 2, N'Красноармейск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1237, 84560, 7, 2, N'Красный Кут')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1238, 84551, 7, 2, N'Лысые Горы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1239, 84567, 7, 2, N'Маркс')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1240, 84565, 7, 2, N'Мокроус')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1241, 84562, 7, 2, N'Новоузенск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1242, 84557, 7, 2, N'Новые Бурасы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1243, 84576, 7, 2, N'Озинки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1244, 84575, 7, 2, N'Перелюб')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1245, 84555, 7, 2, N'Петровск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1246, 84574, 7, 2, N'Пугачев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1247, 84544, 7, 2, N'Романовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1248, 84540, 7, 2, N'Ртищево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1249, 84548, 7, 2, N'Самойловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1250, 84566, 7, 2, N'Степное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1251, 84558, 7, 2, N'Татищево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1252, 84595, 7, 2, N'Хвалынск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1253, 8122, 7, 2, N'СМОЛЕНСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1254, 8132, 7, 2, N'Велиж')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1255, 8131, 7, 2, N'Вязьма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1256, 8135, 7, 2, N'Гагарин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1257, 8165, 7, 2, N'Глинка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1258, 8147, 7, 2, N'Демидов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1259, 8153, 7, 2, N'Десногорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1260, 8144, 7, 2, N'Дорогобуж')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1261, 8166, 7, 2, N'Духовщина')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1262, 8146, 7, 2, N'Ельня')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1263, 8155, 7, 2, N'Ершичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1264, 8167, 7, 2, N'Кардымово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1265, 8145, 7, 2, N'Красный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1266, 8148, 7, 2, N'Монастырщина')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1267, 8138, 7, 2, N'Новодугино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1268, 8149, 7, 2, N'Починок')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1269, 8134, 7, 2, N'Рославль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1270, 8141, 7, 2, N'Рудня')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1271, 8130, 7, 2, N'Сычевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1272, 8136, 7, 2, N'Темкино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1273, 8137, 7, 2, N'Угра')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1274, 8140, 7, 2, N'Хиславичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1275, 8139, 7, 2, N'Холм-Жирковский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1276, 8133, 7, 2, N'Шумячи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1277, 8143, 7, 2, N'Ярцево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1278, 8622, 7, 2, N'СОЧИ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1279, 8652, 7, 2, N'СТАВРОПОЛЬ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1280, 86557, 7, 2, N'Александровское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1281, 86549, 7, 2, N'Благодарный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1282, 87934, 7, 2, N'Ессентуки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1283, 86545, 7, 2, N'Изобильный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1284, 86542, 7, 2, N'Ипатово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1285, 87937, 7, 2, N'Кисловодск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1286, 86550, 7, 2, N'Кочубеевское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1287, 86541, 7, 2, N'Красногвардейское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1288, 86556, 7, 2, N'Курсавка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1289, 879, 7, 2, N'Минеральные Воды')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1290, 86554, 7, 2, N'Невинномысск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1291, 86544, 7, 2, N'Новоалександровск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1292, 86548, 7, 2, N'Новоселицкое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1293, 87933, 7, 2, N'Пятигорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1294, 3462, 7, 2, N'СУРГУТ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1295, 34674, 7, 2, N'Березово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1296, 8212, 7, 2, N'СЫКТЫВКАР')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1297, 82134, 7, 2, N'Айкино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1298, 82131, 7, 2, N'Визинга')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1299, 82151, 7, 2, N'Воркута')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1300, 82146, 7, 2, N'Вуктыл')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1301, 82139, 7, 2, N'Емва')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1302, 82140, 7, 2, N'Ижма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1303, 82145, 7, 2, N'Инта')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1304, 82132, 7, 2, N'Койгородок')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1305, 82136, 7, 2, N'Корткерос')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1306, 82133, 7, 2, N'Объячево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1307, 82142, 7, 2, N'Печора')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1308, 82149, 7, 2, N'Сосногорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1309, 82138, 7, 2, N'Троицко-Печорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1310, 82144, 7, 2, N'Усинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1311, 82135, 7, 2, N'Усогорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1312, 82137, 7, 2, N'Усть-Кулом')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1313, 82141, 7, 2, N'Усть-Цильма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1314, 82147, 7, 2, N'Ухта')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1315, 752, 7, 2, N'ТАМБОВ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1316, 7534, 7, 2, N'Бондари')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1317, 7551, 7, 2, N'Гавриловка-2-я')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1318, 7536, 7, 2, N'Дмитриевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1319, 7535, 7, 2, N'Жердевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1320, 7552, 7, 2, N'Знаменка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1321, 7553, 7, 2, N'Инжавино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1322, 7537, 7, 2, N'Кирсанов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1323, 7541, 7, 2, N'Котовск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1324, 7545, 7, 2, N'Мичуринск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1325, 7542, 7, 2, N'Мордово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1326, 7533, 7, 2, N'Моршанск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1327, 7546, 7, 2, N'Мучкапский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1328, 7548, 7, 2, N'Первомайский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1329, 7544, 7, 2, N'Петровское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1330, 7554, 7, 2, N'Пичаево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1331, 7531, 7, 2, N'Рассказово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1332, 7555, 7, 2, N'Ржакса')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1333, 7556, 7, 2, N'Сатинка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1334, 7532, 7, 2, N'Сосновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1335, 7543, 7, 2, N'Староюрьево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1336, 7557, 7, 2, N'Токаревка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1337, 7558, 7, 2, N'Уварово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1338, 7559, 7, 2, N'Умет')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1339, 8222, 7, 2, N'ТВЕРЬ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1340, 8267, 7, 2, N'Андреаполь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1341, 8231, 7, 2, N'Бежецк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1342, 82502, 7, 2, N'Белый')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1343, 8238, 7, 2, N'Бологое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1344, 8233, 7, 2, N'Вышний Волочек')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1345, 8265, 7, 2, N'Западная Двина')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1346, 8262, 7, 2, N'Зубцов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1347, 8249, 7, 2, N'Калязин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1348, 8234, 7, 2, N'Кашин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1349, 8236, 7, 2, N'Кимры')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1350, 8242, 7, 2, N'Конаково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1351, 8257, 7, 2, N'Кувшиново')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1352, 8271, 7, 2, N'Лесное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1353, 8261, 7, 2, N'Лихославль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1354, 8253, 7, 2, N'Максатиха')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1355, 8266, 7, 2, N'Нелидово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1356, 8258, 7, 2, N'Оленино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1357, 8235, 7, 2, N'Осташков')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1358, 826300, 7, 2, N'Старица')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1359, 8232, 7, 2, N'Ржев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1360, 8251, 7, 2, N'Торжок')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1361, 8268, 7, 2, N'Торопец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1362, 8255, 7, 2, N'Удомля')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1363, 3822, 7, 2, N'ТОМСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1364, 38255, 7, 2, N'Александровское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1365, 38241, 7, 2, N'Асино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1366, 38253, 7, 2, N'Каргасок')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1367, 38244, 7, 2, N'Кожевниково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1368, 38254, 7, 2, N'Колпашево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1369, 38247, 7, 2, N'Мельниково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1370, 38252, 7, 2, N'Парабель')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1371, 38245, 7, 2, N'Первомайское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1372, 38259, 7, 2, N'Стрежевой')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1373, 38246, 7, 2, N'Тегульдет')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1374, 4872, 7, 2, N'ТУЛА')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1375, 48753, 7, 2, N'Алексин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1376, 48733, 7, 2, N'Арсеньево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1377, 48744, 7, 2, N'Архангельское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1378, 48742, 7, 2, N'Белев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1379, 48761, 7, 2, N'Богородицк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1380, 48745, 7, 2, N'Венев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1381, 48768, 7, 2, N'Волово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1382, 48746, 7, 2, N'Донской')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1383, 48732, 7, 2, N'Дубна')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1384, 48741, 7, 2, N'Ефремов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1385, 48734, 7, 2, N'Заокский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1386, 48735, 7, 2, N'Кимовск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1387, 48754, 7, 2, N'Киреевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1388, 48743, 7, 2, N'Куркино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1389, 48767, 7, 2, N'Ленинский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1390, 48762, 7, 2, N'Новомосковск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1391, 48736, 7, 2, N'Одоев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1392, 48752, 7, 2, N'Плавск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1393, 48763, 7, 2, N'Суворов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1394, 48755, 7, 2, N'Теплое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1395, 48731, 7, 2, N'Узловая')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1396, 48756, 7, 2, N'Чернь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1397, 48751, 7, 2, N'Щекино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1398, 48766, 7, 2, N'Ясногорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1399, 3452, 7, 2, N'ТЮМЕНЬ')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1400, 3455622, 7, 2, N'Абатский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1401, 34547, 7, 2, N'Армизонское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1402, 34545, 7, 2, N'Аромашево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1403, 34554, 7, 2, N'Бердюжье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1404, 34550, 7, 2, N'Большое Сорокино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1405, 34557, 7, 2, N'Викулово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1406, 34546, 7, 2, N'Голышманово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1407, 34536, 7, 2, N'Губкинский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1408, 34542, 7, 2, N'Заводоуковск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1409, 34537, 7, 2, N'Исетское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1410, 34551, 7, 2, N'Ишим')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1411, 34553, 7, 2, N'Казанское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1412, 34594, 7, 2, N'Мужи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1413, 34538, 7, 2, N'Муравленко')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1414, 34995, 7, 2, N'Надым')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1415, 34533, 7, 2, N'Нижняя Тавда')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1416, 34564, 7, 2, N'Ноябрьск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1417, 34549, 7, 2, N'Новый Уренгой')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1418, 34544, 7, 2, N'Омутинский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1419, 34591, 7, 2, N'Салехард')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1420, 34555, 7, 2, N'Сладково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1421, 34540, 7, 2, N'Тазовский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1422, 34597, 7, 2, N'Тарко-Сале')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1423, 34511, 7, 2, N'Тобольск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1424, 34516, 7, 2, N'Уват')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1425, 34541, 7, 2, N'Упорово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1426, 34535, 7, 2, N'Ялуторовск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1427, 34531, 7, 2, N'Ярково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1428, 30122, 7, 2, N'УЛАН-УДЭ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1429, 30145, 7, 2, N'Гусиноозерск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1430, 30139, 7, 2, N'Северобайкальск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1431, 30148, 7, 2, N'Хоринск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1432, 8422, 7, 2, N'УЛЬЯНОВСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1433, 84253, 7, 2, N'Барыш')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1434, 84245, 7, 2, N'Большое Нагаткино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1435, 84243, 7, 2, N'Вешкайма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1436, 84235, 7, 2, N'Димитровград')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1437, 84241, 7, 2, N'Инза')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1438, 84254, 7, 2, N'Ишеевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1439, 84246, 7, 2, N'Карсун')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1440, 84237, 7, 2, N'Кузоватово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1441, 84244, 7, 2, N'Майна')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1442, 84247, 7, 2, N'Николаевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1443, 84232, 7, 2, N'Новая Малыкла')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1444, 84238, 7, 2, N'Новоспасское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1445, 84255, 7, 2, N'Новоульяновск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1446, 84239, 7, 2, N'Радищево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1447, 84249, 7, 2, N'Старая Кулатка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1448, 84230, 7, 2, N'Старая Майна')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1449, 84242, 7, 2, N'Сурское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1450, 84234, 7, 2, N'Тереньга')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1451, 84231, 7, 2, N'Чердаклы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1452, 3472, 7, 2, N'УФА')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1453, 34774, 7, 2, N'Архангельское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1454, 34772, 7, 2, N'Аскарово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1455, 34771, 7, 2, N'Аскино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1456, 34751, 7, 2, N'Баймак')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1457, 34742, 7, 2, N'Бакалы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1458, 34716, 7, 2, N'Белебей')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1459, 34792, 7, 2, N'Белорецк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1460, 34743, 7, 2, N'Бижбуляк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1461, 34714, 7, 2, N'Бирск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1462, 34766, 7, 2, N'Благовещенск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1463, 34770, 7, 2, N'Большеустьикинское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1464, 34773, 7, 2, N'Буздяк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1465, 34756, 7, 2, N'Бураево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1466, 34762, 7, 2, N'Верхнеяркеево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1467, 34748, 7, 2, N'Верхние Киги')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1468, 34768, 7, 2, N'Давлеканово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1469, 34717, 7, 2, N'Дюртюли')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1470, 34741, 7, 2, N'Ермекеево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1471, 34757, 7, 2, N'Ермолаево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1472, 34752, 7, 2, N'Зилаир')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1473, 34795, 7, 2, N'Иглино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1474, 34715, 7, 2, N'Исянгулово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1475, 34794, 7, 2, N'Ишимбай')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1476, 34779, 7, 2, N'Калтасы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1477, 34744, 7, 2, N'Караидель')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1478, 34765, 7, 2, N'Кармаскалы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1479, 34718, 7, 2, N'Киргиз-Мияки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1480, 34776, 7, 2, N'Красная Горка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1481, 34740, 7, 2, N'Красноусольский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1482, 34761, 7, 2, N'Кумертау')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1483, 34710, 7, 2, N'Кушнаренково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1484, 34777, 7, 2, N'Малояз')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1485, 34764, 7, 2, N'Мелеуз')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1486, 34798, 7, 2, N'Месягутово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1487, 34749, 7, 2, N'Мишкино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1488, 34719, 7, 2, N'Мраково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1489, 34713, 7, 2, N'Нефтекамск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1490, 34759, 7, 2, N'Николо-Березовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1491, 34750, 7, 2, N'Новобелокатай')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1492, 34767, 7, 2, N'Октябрьский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1493, 34754, 7, 2, N'Раевский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1494, 34763, 7, 2, N'Салават')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1495, 34775, 7, 2, N'Сибай')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1496, 34793, 7, 2, N'Стерлибашево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1497, 3473, 7, 2, N'Стерлитамак')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1498, 34745, 7, 2, N'Толбазы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1499, 34712, 7, 2, N'Туймазы')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1500, 34791, 7, 2, N'Учалы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1501, 34746, 7, 2, N'Федоровка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1502, 34796, 7, 2, N'Чекмагуш')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1503, 34797, 7, 2, N'Чишмы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1504, 34769, 7, 2, N'Шаран')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1505, 34747, 7, 2, N'Языково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1506, 34760, 7, 2, N'Янаул')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1507, 4212, 7, 2, N'ХАБАРОВСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1508, 42153, 7, 2, N'Вяземский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1509, 42138, 7, 2, N'Советская Гавань')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1510, 42146, 7, 2, N'Солнечный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1511, 42156, 7, 2, N'Троицкое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1512, 8352, 7, 2, N'ЧЕБОКСАРЫ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1513, 83531, 7, 2, N'Алатырь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1514, 83535, 7, 2, N'Аликово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1515, 83532, 7, 2, N'Батырево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1516, 83537, 7, 2, N'Вурнары')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1517, 83538, 7, 2, N'Ибреси')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1518, 83533, 7, 2, N'Канаш')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1519, 83534, 7, 2, N'Козловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1520, 83539, 7, 2, N'Комсомольское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1521, 83530, 7, 2, N'Красноармейское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1522, 83551, 7, 2, N'Красные Четаи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1523, 83540, 7, 2, N'Кугеси')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1524, 83542, 7, 2, N'Мариинский Посад')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1525, 83541, 7, 2, N'Моргауши')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1526, 83543, 7, 2, N'Порецкое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1527, 83544, 7, 2, N'Урмары')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1528, 83545, 7, 2, N'Цивильск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1529, 83546, 7, 2, N'Шемурша')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1530, 83536, 7, 2, N'Шумерля')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1531, 83549, 7, 2, N'Яльчики')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1532, 83548, 7, 2, N'Янтиково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1533, 3512, 7, 2, N'ЧЕЛЯБИНСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1534, 35140, 7, 2, N'Агаповка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1535, 35132, 7, 2, N'Аргаяш')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1536, 35159, 7, 2, N'Аша')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1537, 35141, 7, 2, N'Бреды')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1538, 35142, 7, 2, N'Варна')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1539, 35164, 7, 2, N'Верхний Уфалей')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1540, 35143, 7, 2, N'Верхнеуральск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1541, 35144, 7, 2, N'Долгодеревенское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1542, 35138, 7, 2, N'Еманжелинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1543, 35145, 7, 2, N'Еткуль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1544, 35136, 7, 2, N'Златоуст')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1545, 35133, 7, 2, N'Карталы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1546, 35149, 7, 2, N'Касли')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1547, 35147, 7, 2, N'Катав-Ивановск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1548, 35155, 7, 2, N'Кизильское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1549, 35139, 7, 2, N'Копейск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1550, 35152, 7, 2, N'Коркино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1551, 35148, 7, 2, N'Кунашак')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1552, 35154, 7, 2, N'Куса')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1553, 35151, 7, 2, N'Кыштым')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1554, 3511, 7, 2, N'Магнитогорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1555, 35135, 7, 2, N'Миасс')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1556, 35150, 7, 2, N'Миасское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1557, 35156, 7, 2, N'Нязепетровск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1558, 35171, 7, 2, N'Озерск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1559, 35158, 7, 2, N'Октябрьское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1560, 35160, 7, 2, N'Пласт')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1561, 35161, 7, 2, N'Сатка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1562, 35172, 7, 2, N'Снежинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1563, 35146, 7, 2, N'Солнечная Долина')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1564, 35163, 7, 2, N'Троицк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1565, 35166, 7, 2, N'Увельский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1566, 35167, 7, 2, N'Усть-Катав')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1567, 35157, 7, 2, N'Фершампенуаз')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1568, 35168, 7, 2, N'Чебаркуль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1569, 35169, 7, 2, N'Чесма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1570, 35134, 7, 2, N'Южноуральск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1571, 8202, 7, 2, N'ЧЕРЕПОВЕЦ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1572, 87822, 7, 2, N'ЧЕРКЕССК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1573, 3022, 7, 2, N'ЧИТА')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1574, 30232, 7, 2, N'Балей')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1575, 30256, 7, 2, N'Дульдурга')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1576, 30234, 7, 2, N'Карымская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1577, 30230, 7, 2, N'Красный Чикой')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1578, 30242, 7, 2, N'Нерчинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1579, 30236, 7, 2, N'Петровск-Забайкальск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1580, 30238, 7, 2, N'Улеты')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1581, 30266, 7, 2, N'Шелопугино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1582, 30244, 7, 2, N'Шилка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1583, 4112, 7, 2, N'ЯКУТСК')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1584, 41145, 7, 2, N'Алдан')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1585, 41142, 7, 2, N'Амга')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1586, 41131, 7, 2, N'Бердигестях')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1587, 41133, 7, 2, N'Верхневилюйск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1588, 41132, 7, 2, N'Вилюйск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1589, 41137, 7, 2, N'Ленск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1590, 41143, 7, 2, N'Майя')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1591, 41136, 7, 2, N'Мирный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1592, 41147, 7, 2, N'Нерюнгри')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1593, 41134, 7, 2, N'Нюрба')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1594, 41138, 7, 2, N'Олекминск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1595, 41144, 7, 2, N'Покровск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1596, 41163, 7, 2, N'Сангар')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1597, 41135, 7, 2, N'Сунтар')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1598, 41167, 7, 2, N'Тикси')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1599, 41141, 7, 2, N'Усть-Мая')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1600, 41153, 7, 2, N'Хандыга')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1601, 852, 7, 2, N'ЯРОСЛАВЛЬ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1602, 8542, 7, 2, N'Большое Село')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1603, 8539, 7, 2, N'Борисоглебский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1604, 8545, 7, 2, N'Брейтово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1605, 8534, 7, 2, N'Гаврилов Ям')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1606, 8538, 7, 2, N'Данилов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1607, 8543, 7, 2, N'Любим')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1608, 8544, 7, 2, N'Мышкин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1609, 8531, 7, 2, N'Некрасовское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1610, 8547, 7, 2, N'Новый Некоуз')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1611, 8535, 7, 2, N'Переславль-Залесский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1612, 8549, 7, 2, N'Пречистое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1613, 8546, 7, 2, N'Пошехонье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1614, 8536, 7, 2, N'Ростов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1615, 855, 7, 2, N'Рыбинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1616, 8533, 7, 2, N'Тутаев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1617, 8532, 7, 2, N'Углич')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1618, 6564212, 380, 2, N'Азовское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1619, 6131, 380, 2, N'Акимовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1620, 5235, 380, 2, N'Александрия')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1621, 6269, 380, 2, N'Александровка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1622, 5242, 380, 2, N'Александровка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1623, 65114, 380, 2, N'Алупка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1624, 6560, 380, 2, N'Алушта')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1625, 6442, 380, 2, N'Алчевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1626, 6259, 380, 2, N'Амвросиевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1627, 4863, 380, 2, N'Ананьев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1628, 6154443, 380, 2, N'Андреевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1629, 4136, 380, 2, N'Андрушевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1630, 6431, 380, 2, N'Антрацит')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1631, 5656, 380, 2, N'Апостолово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1632, 5132, 380, 2, N'Арбузинка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1633, 6567, 380, 2, N'Армянск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1634, 6274, 380, 2, N'Артемовск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1635, 4845, 380, 2, N'Арциз')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1636, 5538, 380, 2, N'Аскания-Нова')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1637, 5446, 380, 2, N'Ахтырка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1638, 65212, 380, 2, N'Аэрофлотский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1639, 5762, 380, 2, N'Бабай')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1640, 5749, 380, 2, N'Балаклея')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1641, 4866, 380, 2, N'Балта')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1642, 4341, 380, 2, N'Бар')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1643, 4144, 380, 2, N'Барановка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1644, 5757, 380, 2, N'Барвенково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1645, 4476, 380, 2, N'Барышевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1646, 463500, 380, 2, N'Батурин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1647, 46350909, 380, 2, N'Бахмач')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1648, 65532324, 380, 2, N'Бахчисарай')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1649, 5158, 380, 2, N'Баштанка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1650, 4463, 380, 2, N'Белая Церковь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1651, 4849, 380, 2, N'Белгород-Днестровский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1652, 6466, 380, 2, N'Белгородск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1653, 6559, 380, 2, N'Беловодск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1654, 5547, 380, 2, N'Белозерка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1655, 6462, 380, 2, N'Белокукарино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1656, 5443, 380, 2, N'Белополье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1657, 4852, 380, 2, N'Беляевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1658, 4143, 380, 2, N'Бердичев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1659, 6153, 380, 2, N'Бердянск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1660, 3141, 380, 2, N'Берегово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1661, 3548, 380, 2, N'Бережаны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1662, 5153, 380, 2, N'Березанка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1663, 5168, 380, 2, N'Березниговатое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1664, 3653, 380, 2, N'Березно')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1665, 4856, 380, 2, N'Березовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1666, 5728, 380, 2, N'Березовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1667, 5546, 380, 2, N'Берислав')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1668, 4352, 380, 2, N'Бершадь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1669, 5754, 380, 2, N'Близнюки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1670, 5257, 380, 2, N'Бобринец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1671, 32611233, 380, 2, N'Бобрка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1672, 4632, 380, 2, N'Бобровица')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1673, 5758, 380, 2, N'Богодухов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1674, 3471, 380, 2, N'Богородчаны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1675, 4461, 380, 2, N'Богуслав')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1676, 4846, 380, 2, N'Болград')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1677, 3437, 380, 2, N'Болехов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1678, 4653, 380, 2, N'Борзна')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1679, 3248, 380, 2, N'Борислав')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1680, 4495, 380, 2, N'Борисполь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1681, 5759, 380, 2, N'Боровая')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1682, 4412377, 380, 2, N'Бородянка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1683, 3541, 380, 2, N'Борщов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1684, 4498, 380, 2, N'Боярка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1685, 5131, 380, 2, N'Братское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1686, 4494, 380, 2, N'Бровары')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1687, 3266, 380, 2, N'Броды')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1688, 4162, 380, 2, N'Брусилов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1689, 6443, 380, 2, N'Брянка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1690, 3438, 380, 2, N'Бурштын')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1691, 5454, 380, 2, N'Бурынь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1692, 3264, 380, 2, N'Буск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1693, 4497, 380, 2, N'Буча')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1694, 3544, 380, 2, N'Бучач')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1695, 4146, 380, 2, N'Быковка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1696, 5753, 380, 2, N'Валки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1697, 4350, 380, 2, N'Вапнярка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1698, 4636, 380, 2, N'Варва')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1699, 617025, 380, 2, N'Васильевка')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1700, 4471, 380, 2, N'Васильков')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1701, 5639, 380, 2, N'Васильковка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1702, 572246, 380, 2, N'Введенка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1703, 5532, 380, 2, N'Великая Александровка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1704, 6156, 380, 2, N'Великая Белозерка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1705, 5345, 380, 2, N'Великая Богачка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1706, 5543, 380, 2, N'Великая Лепетиха')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1707, 4859, 380, 2, N'Великая Михайловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1708, 6243, 380, 2, N'Великая Новоселовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1709, 5457, 380, 2, N'Великая Писаревка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1710, 3135, 380, 2, N'Великий Березный')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1711, 511109752, 380, 2, N'Великий Бурлук')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1712, 5658, 380, 2, N'Верхнеднепровск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1713, 5545, 380, 2, N'Верхний Рогачик')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1714, 3432, 380, 2, N'Верховина')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1715, 5163, 380, 2, N'Веселиново')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1716, 6136, 380, 2, N'Веселое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1717, 3730, 380, 2, N'Вижница')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1718, 432, 380, 2, N'Винница')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1719, 65004, 380, 2, N'Виноградное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1720, 3143, 380, 2, N'Виноградов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1721, 3846, 380, 2, N'Виньковцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1722, 4000498, 380, 2, N'Вишнево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1723, 3342, 380, 2, N'Владимир-Волынский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1724, 3634, 380, 2, N'Владимирец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1725, 5741, 380, 2, N'Вовчанск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1726, 5134, 380, 2, N'Вознесенск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1727, 6244, 380, 2, N'Волноваха')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1728, 3136, 380, 2, N'Воловец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1729, 4469, 380, 2, N'Володарка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1730, 4145, 380, 2, N'Володарск-Волынский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1731, 6246, 380, 2, N'Володарское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1732, 3845, 380, 2, N'Волочиск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1733, 5653, 380, 2, N'Вольногорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1734, 6, 380, 2, N'Вольнянск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1735, 5135, 380, 2, N'Врадиевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1736, 5732, 380, 2, N'Высокий')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1737, 5535, 380, 2, N'Высокополье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1738, 4498886, 380, 2, N'Вышгород')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1739, 5354, 380, 2, N'Гадяч')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1740, 5254, 380, 2, N'Гайворон')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1741, 4334, 380, 2, N'Гайсин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1742, 3431, 380, 2, N'Галич')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1743, 6754, 380, 2, N'Гаспра')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1744, 5534, 380, 2, N'Геническ')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1745, 3740, 380, 2, N'Герца')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1746, 5365, 380, 2, N'Глобино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1747, 5444, 380, 2, N'Глухов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1748, 3734, 380, 2, N'Глыбока')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1749, 5539, 380, 2, N'Голая Пристань')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1750, 5252, 380, 2, N'Голованевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1751, 65654, 380, 2, N'Голубая Затока')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1752, 624, 380, 2, N'Горловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1753, 5544, 380, 2, N'Горностаевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1754, 3430, 380, 2, N'Городенка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1755, 4734, 380, 2, N'Городище')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1756, 4141, 380, 2, N'Городница')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1757, 4645, 380, 2, N'Городня')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1758, 3231121, 380, 2, N'Городок')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1759, 3851, 380, 2, N'Городок')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1760, 3379, 380, 2, N'Горохов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1761, 3650, 380, 2, N'Гоща')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1762, 5359, 380, 2, N'Гребенка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1763, 41438, 380, 2, N'Гришковцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1764, 6145, 380, 2, N'Гуляйполе')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1765, 65400, 380, 2, N'Гурзуф')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1766, 350101, 380, 2, N'Гусятин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1767, 32411, 380, 2, N'Дашава')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1768, 4345, 380, 2, N'Дашев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1769, 5750, 380, 2, N'Двуречная')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1770, 6249, 380, 2, N'Дебальцево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1771, 3637, 380, 2, N'Демидовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1772, 3856, 380, 2, N'Деражня')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1773, 576311, 380, 2, N'Дергачи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1774, 656409, 380, 2, N'Джанкой')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1775, 6247, 380, 2, N'Дзержинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1776, 41460808, 380, 2, N'Дзержинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1777, 5351, 380, 2, N'Диканька')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1778, 46350808, 380, 2, N'Дмитровка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1779, 569, 380, 2, N'Днепродзержинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1780, 56, 380, 2, N'Днепропетровск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1781, 562, 380, 2, N'Днепропетровск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1782, 6175, 380, 2, N'Днепрорудное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1783, 5253, 380, 2, N'Добровеличковка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1784, 6277, 380, 2, N'Доброполье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1785, 414400, 380, 2, N'Довбыш')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1786, 6275, 380, 2, N'Докучаевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1787, 3477, 380, 2, N'Долина')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1788, 5234, 380, 2, N'Долинская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1789, 5152, 380, 2, N'Доманевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1790, 62, 380, 2, N'Донецк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1791, 622, 380, 2, N'Донецк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1792, 4738, 380, 2, N'Драбов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1793, 3244, 380, 2, N'Дрогобыч')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1794, 4135, 380, 2, N'Дружба')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1795, 5456, 380, 2, N'Дружба')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1796, 6267, 380, 2, N'Дружковка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1797, 3656, 380, 2, N'Дубно')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1798, 5447, 380, 2, N'Дубовязовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1799, 3658, 380, 2, N'Дубровица')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1800, 3858, 380, 2, N'Дунаевцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1801, 4496, 380, 2, N'Дымер')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1802, 656009, 380, 2, N'Евпатория')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1803, 5159, 380, 2, N'Еланец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1804, 4149, 380, 2, N'Емильчина')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1805, 6252, 380, 2, N'Енакиево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1806, 4747, 380, 2, N'Жашков')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1807, 5652, 380, 2, N'Желтые Воды')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1808, 3239, 380, 2, N'Жидачев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1809, 412, 380, 2, N'Житомир')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1810, 4332, 380, 2, N'Жмеринка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1811, 3251112, 380, 2, N'Жовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1812, 4470, 380, 2, N'Журовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1813, 3554, 380, 2, N'Залещики')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1814, 600569, 380, 2, N'Заозерное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1815, 6122, 380, 2, N'Запорожье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1816, 3632, 380, 2, N'Заречное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1817, 5761, 380, 2, N'Зачепиловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1818, 3550, 380, 2, N'Збараж')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1819, 3540, 380, 2, N'Зборов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1820, 4740, 380, 2, N'Звенигородка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1821, 3652, 380, 2, N'Здолбунов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1822, 5655, 380, 2, N'Зеленодольск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1823, 5353, 380, 2, N'Зеньков')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1824, 6473, 380, 2, N'Зимогория')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1825, 51747, 380, 2, N'Змиев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1826, 5233, 380, 2, N'Знаменка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1827, 3551, 380, 2, N'Золотники')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1828, 4737, 380, 2, N'Золотоноша')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1829, 3265, 380, 2, N'Золочев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1830, 5764, 380, 2, N'Золочев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1831, 3372, 380, 2, N'Иваничи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1832, 4491, 380, 2, N'Иванков')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1833, 342, 380, 2, N'Ивано-Франковск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1834, 3422, 380, 2, N'Ивано-Франковск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1835, 4854, 380, 2, N'Ивановка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1836, 5531, 380, 2, N'Ивановка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1837, 4131119, 380, 2, N'Иванополь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1838, 4841, 380, 2, N'Измаил')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1839, 5743, 380, 2, N'Изюм')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1840, 3852, 380, 2, N'Изяслав')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1841, 43450203, 380, 2, N'Ильинцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1842, 4868, 380, 2, N'Ильичевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1843, 41450707, 380, 2, N'Иршанск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1844, 4633, 380, 2, N'Ичня')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1845, 4473, 380, 2, N'Кагарлык')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1846, 5164, 380, 2, N'Казанка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1847, 4342, 380, 2, N'Казатин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1848, 5530, 380, 2, N'Каланчак')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1849, 4333, 380, 2, N'Калиновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1850, 4411194, 380, 2, N'Калита')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1851, 3472, 380, 2, N'Калуш')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1852, 3849, 380, 2, N'Каменец-Подольский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1853, 4732, 380, 2, N'Каменка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1854, 3254, 380, 2, N'Каменка-Бугская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1855, 6138, 380, 2, N'Каменка-Днепровская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1856, 3357, 380, 2, N'Камень-Каширский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1857, 4736, 380, 2, N'Канев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1858, 5346, 380, 2, N'Карловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1859, 4742, 380, 2, N'Катеринополь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1860, 65904, 380, 2, N'Катцивели')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1861, 5536, 380, 2, N'Каховка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1862, 692, 380, 2, N'Кача')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1863, 5755, 380, 2, N'Кегичевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1864, 3732, 380, 2, N'Кельменцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1865, 6561, 380, 2, N'Керчь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1866, 3365, 380, 2, N'Киверцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1867, 44, 380, 2, N'Киев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1868, 4843, 380, 2, N'Килия')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1869, 6131231, 380, 2, N'Кириловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1870, 522, 380, 2, N'Кировоград')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1871, 6446, 380, 2, N'Кировск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1872, 6555, 380, 2, N'Кировское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1873, 6250, 380, 2, N'Кировское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1874, 3736, 380, 2, N'Кировское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1875, 5343, 380, 2, N'Кобеляки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1876, 3352, 380, 2, N'Ковель')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1877, 4867, 380, 2, N'Кодыма')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1878, 576333, 380, 2, N'Козачья Лопань')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1879, 46212146, 380, 2, N'Козелец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1880, 5342, 380, 2, N'Козельщина')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1881, 3542227, 380, 2, N'Козлов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1882, 3547, 380, 2, N'Козова')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1883, 5766, 380, 2, N'Коломак')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1884, 3433, 380, 2, N'Коломыя')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1885, 3231, 380, 2, N'Комарно')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1886, 4855, 380, 2, N'Коментерновское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1887, 5240, 380, 2, N'Компанеевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1888, 5348, 380, 2, N'Комсомольск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1889, 622253, 380, 2, N'Комсомольское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1890, 571147, 380, 2, N'Комсомольское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1891, 54401017, 380, 2, N'Конотоп')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1892, 6272, 380, 2, N'Константиновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1893, 3557, 380, 2, N'Копыченцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1894, 3651, 380, 2, N'Корец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1895, 65784, 380, 2, N'Кориез')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1896, 4656, 380, 2, N'Короп')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1897, 3555, 380, 2, N'Коропец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1898, 4142, 380, 2, N'Коростень')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1899, 4130, 380, 2, N'Коростышев')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1900, 4735, 380, 2, N'Корсунь-Шевченко')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1901, 4657, 380, 2, N'Корюковка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1902, 6152123, 380, 2, N'Коса')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1903, 3478, 380, 2, N'Косов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1904, 3657, 380, 2, N'Костополь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1905, 5350, 380, 2, N'Котельва')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1906, 4862, 380, 2, N'Котовск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1907, 574446, 380, 2, N'Кочеток')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1908, 621116, 380, 2, N'Краматорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1909, 6264, 380, 2, N'Краматорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1910, 3855, 380, 2, N'Красилов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1911, 623, 380, 2, N'Красноармейск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1912, 6556, 380, 2, N'Красногвардейское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1913, 5744, 380, 2, N'Красноград')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1914, 6435, 380, 2, N'Краснодон')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1915, 65764, 380, 2, N'Краснокаменка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1916, 5756, 380, 2, N'Краснокутск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1917, 5745, 380, 2, N'Краснопавловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1918, 6565, 380, 2, N'Красноперекопск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1919, 5459, 380, 2, N'Краснополье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1920, 4861, 380, 2, N'Красные Окны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1921, 6261, 380, 2, N'Красный Лиман')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1922, 6432, 380, 2, N'Красный Луч')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1923, 4492, 380, 2, N'Красятичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1924, 3546, 380, 2, N'Кременец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1925, 6454, 380, 2, N'Кременная')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1926, 536, 380, 2, N'Кременчуг')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1927, 5133, 380, 2, N'Кривое Озеро')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1928, 564, 380, 2, N'Кривой Рог')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1929, 5654, 380, 2, N'Кринички')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1930, 5453, 380, 2, N'Кролевец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1931, 4340, 380, 2, N'Крыжополь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1932, 3636, 380, 2, N'Кузнецовск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1933, 65542121, 380, 2, N'Куйбышево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1934, 6147, 380, 2, N'Куйбышево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1935, 4643, 380, 2, N'Куликовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1936, 5742, 380, 2, N'Купянск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1937, 64454, 380, 2, N'Курпаты')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1938, 4343, 380, 2, N'Ладыжин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1939, 5537, 380, 2, N'Лазурное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1940, 3549, 380, 2, N'Лановцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1941, 5445, 380, 2, N'Лебедин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1942, 6557, 380, 2, N'Ленино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1943, 3857, 380, 2, N'Летичев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1944, 6654, 380, 2, N'Ливадия')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1945, 574700, 380, 2, N'Лиман')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1946, 5452, 380, 2, N'Липовая Долина')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1947, 4358, 380, 2, N'Липовец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1948, 6451, 380, 2, N'Лисичанск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1949, 4749, 380, 2, N'Лисянка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1950, 4347, 380, 2, N'Литин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1951, 5742225, 380, 2, N'Лозовая')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1952, 3374, 380, 2, N'Локачи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1953, 5356, 380, 2, N'Лохвица')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1954, 53615, 380, 2, N'Лубны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1955, 642, 380, 2, N'Луганск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1956, 4161, 380, 2, N'Лугины')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1957, 6436, 380, 2, N'Лутугино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1958, 332, 380, 2, N'Луцк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1959, 32, 380, 2, N'Львов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1960, 4147, 380, 2, N'Любар')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1961, 4864, 380, 2, N'Любашевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1962, 3362, 380, 2, N'Любешов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1963, 3377, 380, 2, N'Любомль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1964, 5172, 380, 2, N'Люботин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1965, 5691, 380, 2, N'Магдалиновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1966, 4478, 380, 2, N'Макаров')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1967, 623000, 380, 2, N'Макеевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1968, 5258, 380, 2, N'Малая Виска')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1969, 500763, 380, 2, N'Малая Даниловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1970, 4133, 380, 2, N'Малин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1971, 5746, 380, 2, N'Малиновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1972, 612, 380, 2, N'Малокатериновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1973, 3376, 380, 2, N'Маневичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1974, 4748, 380, 2, N'Маньковка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1975, 5665, 380, 2, N'Марганец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1976, 629, 380, 2, N'Мариуполь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1977, 6464, 380, 2, N'Марковка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1978, 6278, 380, 2, N'Марьинка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1979, 410044, 380, 2, N'Марьяновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1980, 61564, 380, 2, N'Масандра')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1981, 5364, 380, 2, N'Машевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1982, 3146, 380, 2, N'Межгорье')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1983, 5630, 380, 2, N'Межевая')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1984, 619, 380, 2, N'Мелитополь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1985, 6465, 380, 2, N'Меловое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1986, 4644, 380, 2, N'Мена')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1987, 5702, 380, 2, N'Мерефа')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1988, 5355, 380, 2, N'Миргород')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1989, 4474, 380, 2, N'Мироновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1990, 41460880, 380, 2, N'Мирополь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1991, 6254, 380, 2, N'Мисхор')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1992, 6132222, 380, 2, N'Михайловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1993, 3659, 380, 2, N'Млинов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1994, 4337, 380, 2, N'Могилев-Подольский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1995, 65232, 380, 2, N'Молодежное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1996, 311555, 380, 2, N'Монастыриска')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1997, 4746, 380, 2, N'Монастырище')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1998, 32606, 380, 2, N'Моршин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (1999, 3234, 380, 2, N'Мостиска')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2000, 3131, 380, 2, N'Мукачево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2001, 4356, 380, 2, N'Мурованные Куриловцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2002, 3475, 380, 2, N'Надворная')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2003, 4140, 380, 2, N'Народичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2004, 5455, 380, 2, N'Недригайлов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2005, 4631, 380, 2, N'Нежин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2006, 4331, 380, 2, N'Немиров')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2007, 3259, 380, 2, N'Немиров')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2008, 3848, 380, 2, N'Нетешин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2009, 6550, 380, 2, N'Нижнегорский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2010, 5540, 380, 2, N'Нижние Серегозы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2011, 511112, 380, 2, N'Николаев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2012, 512, 380, 2, N'Николаев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2013, 4857, 380, 2, N'Николаевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2014, 5662, 380, 2, N'Никополь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2015, 352151, 380, 2, N'Никулинцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2016, 414075, 380, 2, N'Новая Боровая')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2017, 5740, 380, 2, N'Новая Водолага')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2018, 5549, 380, 2, N'Новая Каховка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2019, 5167, 380, 2, N'Новая Одесса')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2020, 3847, 380, 2, N'Новая Ушица')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2021, 4658, 380, 2, N'Новгород-Северский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2022, 5241, 380, 2, N'Новгородка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2023, 6296, 380, 2, N'Новоазовск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2024, 5255, 380, 2, N'Новоархангельск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2025, 3344, 380, 2, N'Нововолынск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2026, 5533, 380, 2, N'Нововоронцовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2027, 3741, 380, 2, N'Новоднестровск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2028, 351143, 380, 2, N'Новое Село')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2029, 5256, 380, 2, N'Новомиргород')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2030, 5693, 380, 2, N'Новомосковск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2031, 6144, 380, 2, N'Новониколаевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2032, 6569, 380, 2, N'Новоозерное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2033, 570046, 380, 2, N'Новопокровка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2034, 6463, 380, 2, N'Новопсков')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2035, 3733, 380, 2, N'Новоселица')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2036, 5548, 380, 2, N'Новотроицкое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2037, 5251, 380, 2, N'Новоукраинка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2038, 4135211, 380, 2, N'Новые Белокоровичи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2039, 5344, 380, 2, N'Новые Санжары')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2040, 6445, 380, 2, N'Новый Айдар')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2041, 5151, 380, 2, N'Новый Буг')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2042, 3261, 380, 2, N'Новый Раздол')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2043, 6566, 380, 2, N'Новый Свет')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2044, 625311, 380, 2, N'Новый Свет')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2045, 4642, 380, 2, N'Носовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2046, 4471112, 380, 2, N'Обухов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2047, 4851, 380, 2, N'Овидиополь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2048, 4148, 380, 2, N'Овруч')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2049, 48, 380, 2, N'Одесса')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2050, 482, 380, 2, N'Одесса')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2051, 4132325, 380, 2, N'Олевск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2052, 5238, 380, 2, N'Онуфриевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2053, 4330, 380, 2, N'Оратов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2054, 650062, 380, 2, N'Ордженикидзе')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2055, 5667, 380, 2, N'Ордженикидзе')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2056, 6141, 380, 2, N'Орехов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2057, 5357, 380, 2, N'Оржица')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2058, 4646, 380, 2, N'Остер')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2059, 3654, 380, 2, N'Острог')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2060, 5154, 380, 2, N'Очаков')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2061, 5632, 380, 2, N'Павлоград')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2062, 65432, 380, 2, N'Парковое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2063, 6455, 380, 2, N'Первомайск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2064, 5161, 380, 2, N'Первомайск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2065, 5748, 380, 2, N'Первомайский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2066, 6552, 380, 2, N'Первомайское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2067, 6441, 380, 2, N'Перевальск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2068, 3263, 380, 2, N'Перемышляны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2069, 3145, 380, 2, N'Перечин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2070, 4467, 380, 2, N'Переяслав-Хмильницкий')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2071, 5633, 380, 2, N'Першотравинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2072, 410944, 380, 2, N'Першотравинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2073, 6297, 380, 2, N'Першотравневое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2074, 4111148, 380, 2, N'Першотравневое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2075, 4477, 380, 2, N'Песковка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2076, 4341119, 380, 2, N'Песчанка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2077, 5237, 380, 2, N'Петрово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2078, 5671, 380, 2, N'Петропавловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2079, 5765, 380, 2, N'Печенеги')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2080, 5358, 380, 2, N'Пирятин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2081, 655562, 380, 2, N'Планерское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2082, 4346, 380, 2, N'Погребище')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2083, 351243, 380, 2, N'Подволочинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2084, 3542, 380, 2, N'Подгайцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2085, 78572, 380, 2, N'Покатиловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2086, 5638, 380, 2, N'Покровское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2087, 6165, 380, 2, N'Пологи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2088, 3843, 380, 2, N'Полонное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2089, 532, 380, 2, N'Полтава')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2090, 5322, 380, 2, N'Полтава')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2091, 414894, 380, 2, N'Полянка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2092, 61154, 380, 2, N'Понизовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2093, 6474, 380, 2, N'Попасная')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2094, 4137, 380, 2, N'Попельня')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2095, 6554, 380, 2, N'Почтовое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2096, 6133, 380, 2, N'Приазовское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2097, 5752, 380, 2, N'Приколотное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2098, 4637, 380, 2, N'Прилуки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2099, 6137, 380, 2, N'Приморск')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2100, 656662, 380, 2, N'Приморский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2101, 6132, 380, 2, N'Пришиб')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2102, 5763, 380, 2, N'Прудянка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2103, 3230, 380, 2, N'Пустомыты')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2104, 5442, 380, 2, N'Путивль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2105, 3738, 380, 2, N'Путила')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2106, 56510, 380, 2, N'Пятихватки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2107, 3252, 380, 2, N'Рава-Русская')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2108, 3255, 380, 2, N'Радехов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2109, 3633, 380, 2, N'Радивилов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2110, 4132, 380, 2, N'Радомышль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2111, 4853, 380, 2, N'Раздельная')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2112, 6553, 380, 2, N'Раздольное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2113, 4462, 380, 2, N'Ракитное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2114, 3366, 380, 2, N'Ратно')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2115, 3132, 380, 2, N'Рахов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2116, 4840, 380, 2, N'Рени')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2117, 4641, 380, 2, N'Репки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2118, 5363, 380, 2, N'Решетиловка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2119, 4411173, 380, 2, N'Ржищев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2120, 6433, 380, 2, N'Ровеньки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2121, 362, 380, 2, N'Ровно')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2122, 3622, 380, 2, N'Ровно')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2123, 59872, 380, 2, N'Роган')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2124, 3435, 380, 2, N'Рогатин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2125, 3368, 380, 2, N'Рожище')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2126, 3474, 380, 2, N'Рожнятов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2127, 6162, 380, 2, N'Розовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2128, 3635, 380, 2, N'Рокитное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2129, 5448, 380, 2, N'Ромны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2130, 6453, 380, 2, N'Рубежное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2131, 4349, 380, 2, N'Рудница')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2132, 4138, 380, 2, N'Ружин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2133, 4865, 380, 2, N'Саврань')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2134, 6563, 380, 2, N'Саки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2135, 3236, 380, 2, N'Самбор')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2136, 62254, 380, 2, N'Санаторное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2137, 4848, 380, 2, N'Сарата')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2138, 3655, 380, 2, N'Сарны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2139, 57162, 380, 2, N'Сахновщина')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2140, 3133, 380, 2, N'Свалява')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2141, 6471, 380, 2, N'Сватово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2142, 6434, 380, 2, N'Свердловск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2143, 5236, 380, 2, N'Светловодск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2144, 6452, 380, 2, N'Северодонецк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2145, 6237, 380, 2, N'Селидово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2146, 5341, 380, 2, N'Семеновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2147, 4659, 380, 2, N'Семеновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2148, 5451, 380, 2, N'Середина-Буда')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2149, 65154, 380, 2, N'Симеиз')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2150, 652, 380, 2, N'Симферополь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2151, 5663, 380, 2, N'Синельниково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2152, 51111537, 380, 2, N'Скадовск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2153, 3543, 380, 2, N'Скалат')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2154, 4468, 380, 2, N'Сквира')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2155, 3251, 380, 2, N'Сколе')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2156, 3842, 380, 2, N'Славута')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2157, 4479, 380, 2, N'Славутич')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2158, 6262, 380, 2, N'Славяногорск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2159, 6411173, 380, 2, N'Славяносербск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2160, 626, 380, 2, N'Славянск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2161, 4733, 380, 2, N'Смела')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2162, 6256, 380, 2, N'Снежное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2163, 5162, 380, 2, N'Снигиревка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2164, 3476, 380, 2, N'Снятын')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2165, 6551, 380, 2, N'Советский')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2166, 65444, 380, 2, N'Советское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2167, 3257, 380, 2, N'Сокаль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2168, 3739, 380, 2, N'Сокиряны')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2169, 5669, 380, 2, N'Соленое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2170, 4655, 380, 2, N'Сосница')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2171, 5650, 380, 2, N'Софиевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2172, 4639, 380, 2, N'Сребное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2173, 4464, 380, 2, N'Ставище')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2174, 6472, 380, 2, N'Станично-Луганское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2175, 3850, 380, 2, N'Старая Синева')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2176, 6461, 380, 2, N'Старобельск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2177, 6253, 380, 2, N'Старобешево')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2178, 3854, 380, 2, N'Староконстантинов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2179, 61212555, 380, 2, N'Старый Крым')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2180, 3238, 380, 2, N'Старый Самбор')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2181, 3346, 380, 2, N'Старя Выжевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2182, 6444, 380, 2, N'Стаханов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2183, 3735, 380, 2, N'Сторожинец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2184, 3245, 380, 2, N'Стрый')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2185, 656611, 380, 2, N'Судак')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2186, 542, 380, 2, N'Сумы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2187, 4634, 380, 2, N'Талалаевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2188, 4731, 380, 2, N'Тальное')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2189, 4466, 380, 2, N'Тараща')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2190, 4847, 380, 2, N'Тарутино')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2191, 4844, 380, 2, N'Татарбунары')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2192, 6279, 380, 2, N'Тельманово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2193, 3844, 380, 2, N'Теофиполь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2194, 4353, 380, 2, N'Теплик')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2195, 355211, 380, 2, N'Теребовля')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2196, 352, 380, 2, N'Тернополь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2197, 4460, 380, 2, N'Тетиев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2198, 3479, 380, 2, N'Тлумач')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2199, 6178, 380, 2, N'Токмак')
GO
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2200, 5668, 380, 2, N'Томаковка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2201, 4348, 380, 2, N'Томашполь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2202, 62541, 380, 2, N'Торез')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2203, 6456, 380, 2, N'Троицкое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2204, 4341113, 380, 2, N'Тростянец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2205, 5458, 380, 2, N'Тростянец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2206, 3247, 380, 2, N'Трускавец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2207, 4335, 380, 2, N'Тульчин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2208, 3363, 380, 2, N'Турийск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2209, 3269, 380, 2, N'Турка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2210, 4355, 380, 2, N'Тывров')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2211, 3436, 380, 2, N'Тысменица')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2212, 3134, 380, 2, N'Тячев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2213, 312, 380, 2, N'Ужгород')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2214, 3122, 380, 2, N'Ужгород')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2215, 4472, 380, 2, N'Украинка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2216, 5259, 380, 2, N'Ульяновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2217, 4744, 380, 2, N'Умань')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2218, 5239, 380, 2, N'Устиновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2219, 50372, 380, 2, N'Утковка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2220, 4465, 380, 2, N'Фастов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2221, 6562, 380, 2, N'Феодосия')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2222, 65554, 380, 2, N'Форос')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2223, 4860, 380, 2, N'Фрунзовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2224, 6257, 380, 2, N'Харцызк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2225, 57, 380, 2, N'Харьков')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2226, 552, 380, 2, N'Херсон')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2227, 43388, 380, 2, N'Хмельник')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2228, 382, 380, 2, N'Хмельницкий')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2229, 3822, 380, 2, N'Хмельницкий')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2230, 5362, 380, 2, N'Хорол')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2231, 54112, 380, 2, N'Хотень')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2232, 3731, 380, 2, N'Хотин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2233, 4745, 380, 2, N'Христоновка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2234, 3142, 380, 2, N'Хуст')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2235, 5690, 380, 2, N'Царичанка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2236, 3348, 380, 2, N'Цумань')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2237, 5542, 380, 2, N'Цюрюпинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2238, 3859, 380, 2, N'Чемеровцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2239, 3249, 380, 2, N'Червоноград')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2240, 472, 380, 2, N'Черкассы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2241, 4357, 380, 2, N'Черневцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2242, 462, 380, 2, N'Чернигов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2243, 4622, 380, 2, N'Чернигов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2244, 6140, 380, 2, N'Черниговка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2245, 4739, 380, 2, N'Чернобай')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2246, 4493, 380, 2, N'Чернобыль')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2247, 372, 380, 2, N'Черновцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2248, 3722, 380, 2, N'Черновцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2249, 6558, 380, 2, N'Черноморское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2250, 5340, 380, 2, N'Чернухи')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2251, 4134, 380, 2, N'Черняхов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2252, 4351, 380, 2, N'Чечельник')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2253, 4730, 380, 2, N'Чигирин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2254, 3137, 380, 2, N'Чоп')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2255, 3552, 380, 2, N'Чортков')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2256, 571146, 380, 2, N'Чугуев')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2257, 4139, 380, 2, N'Чуднов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2258, 5347, 380, 2, N'Чутово')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2259, 4344, 380, 2, N'Шаргород')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2260, 6255, 380, 2, N'Шахтерск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2261, 3355, 380, 2, N'Шацк')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2262, 5751, 380, 2, N'Шевченково')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2263, 3840, 380, 2, N'Шепетовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2264, 5657, 380, 2, N'Широкое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2265, 5352, 380, 2, N'Шишаки')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2266, 325900, 380, 2, N'Шкло')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2267, 5449, 380, 2, N'Шостка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2268, 4741, 380, 2, N'Шпола')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2269, 3558, 380, 2, N'Шумское')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2270, 6512166, 380, 2, N'Щебетовка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2271, 4654, 380, 2, N'Щорс')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2272, 6139, 380, 2, N'Энергодар')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2273, 5136, 380, 2, N'Южноукраинск')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2274, 5635, 380, 2, N'Юрьевка')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2275, 41020249, 380, 2, N'Яблунец')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2276, 300259, 380, 2, N'Яворов')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2277, 4475, 380, 2, N'Яготин')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2278, 60054, 380, 2, N'Ялта')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2279, 6211197, 380, 2, N'Ялта')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2280, 4336, 380, 2, N'Ямполь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2281, 54010156, 380, 2, N'Ямполь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2282, 3841, 380, 2, N'Ямполь')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2283, 3434, 380, 2, N'Яремча')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2284, 3853, 380, 2, N'Ярмолинцы')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2285, 6236, 380, 2, N'Ясиноватое')
INSERT [dbo].[Cities] ([Id], [CitiesCode], [CountryCode], [Language], [Title]) VALUES (2286, 219, 65, 2, N'Frankfurt am Main')
SET IDENTITY_INSERT [dbo].[Cities] OFF
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([Id], [CountryCode], [Language], [Title]) VALUES (1, 7, 1, N'Russia')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Language], [Title]) VALUES (2, 7, 2, N'Россия')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Language], [Title]) VALUES (3, 375, 1, N'Belarus')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Language], [Title]) VALUES (4, 375, 2, N'Беларусь')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Language], [Title]) VALUES (5, 380, 1, N'Ukraine')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Language], [Title]) VALUES (6, 380, 2, N'Украина')
INSERT [dbo].[Countries] ([Id], [CountryCode], [Language], [Title]) VALUES (7, 65, 2, N'Германия')
SET IDENTITY_INSERT [dbo].[Countries] OFF
SET IDENTITY_INSERT [dbo].[Sessions] ON 

INSERT [dbo].[Sessions] ([Id], [Number], [Type], [DateStart], [Expired], [UserId]) VALUES (1, N'a01858b6ff66d66a55804f04281a088f4654a50156b9652b2cc192661a5f886a5612eac6cd8168366c4d5dca1c816ee4e451f6c56ff868fbf5c9ab21', 1, CAST(N'2019-10-29T10:56:15.743' AS DateTime), 0, 0)
INSERT [dbo].[Sessions] ([Id], [Number], [Type], [DateStart], [Expired], [UserId]) VALUES (2, N'f93f9b0d9d640a3e91549ea933effdbd2f55596af4f03ad99baf3d4dff9e664bfd46545df991015bb49e9df6ba5b556d44ae505f609faf903a966e3d', 1, CAST(N'2019-10-29T10:59:45.980' AS DateTime), 0, 0)
INSERT [dbo].[Sessions] ([Id], [Number], [Type], [DateStart], [Expired], [UserId]) VALUES (3, N'afdcbabbaabbc6aacdbc0dbbf8aaff5badc7aa6fa58c5adbcbdab7bc7daa5b5c5dc75f5aa68dfb885ad78abbaabffa7b68b5dfaa56aaaaac5bb5f5b7', 3, CAST(N'2019-10-29T10:59:58.333' AS DateTime), 0, 2)
INSERT [dbo].[Sessions] ([Id], [Number], [Type], [DateStart], [Expired], [UserId]) VALUES (4, N'30fa9ab0552d2f9b0b22a89299924baf3d05cdf6b6005d246d53d08936969b64a90b5ab2b2d9ba9d302242b933ddb0565b6d9d9d3659905ad0bd9022', 3, CAST(N'2019-10-29T11:00:44.363' AS DateTime), 1, 9)
INSERT [dbo].[Sessions] ([Id], [Number], [Type], [DateStart], [Expired], [UserId]) VALUES (5, N'1dc1c17058c130ffcd5bbbcff90a2b5de97f0c4c022bc020f85e02bb290cfc01a25079a85cfab8dfb11a2310a20acf9c8585c07f195222d1ac571528', 1, CAST(N'2019-10-29T11:14:35.320' AS DateTime), 0, 0)
INSERT [dbo].[Sessions] ([Id], [Number], [Type], [DateStart], [Expired], [UserId]) VALUES (6, N'e8d7b66143773bb3d8b0124d049803329c067f84d0f76b26d8b88643628c888da806c04ef3432d38768a747698138273324d733ed9cc733d61bdd993', 3, CAST(N'2019-10-29T11:14:45.913' AS DateTime), 0, 3)
INSERT [dbo].[Sessions] ([Id], [Number], [Type], [DateStart], [Expired], [UserId]) VALUES (7, N'e1f3d3155e0d055e63bb63cd3eeb0340043031bf653e153b89f13ef3f8ee9135025815bc03630382201d3b038f4492bd139536010d03423e953f04f5', 2, CAST(N'2019-10-29T11:15:09.860' AS DateTime), 0, 9)
INSERT [dbo].[Sessions] ([Id], [Number], [Type], [DateStart], [Expired], [UserId]) VALUES (8, N'215116bd96fa77aa9a9aa351979125153dfcaa1a91595a20aa60af14fa6a707d9695da31a55127a57bf3f096a0a523ae51e633aa09a12a57afa55aa7', 2, CAST(N'2019-10-29T11:15:13.597' AS DateTime), 0, 10)
SET IDENTITY_INSERT [dbo].[Sessions] OFF
SET IDENTITY_INSERT [dbo].[UserConnections] ON 

INSERT [dbo].[UserConnections] ([Id], [ConnectionId], [IpAddress], [CultureCode], [UserId], [IsActive], [UpdateTime], [ConnectionOff]) VALUES (1, N'c15b2fb5-0b8e-44df-b70d-2797467cdc51', N'::1', N'ru-RU', 9, 0, CAST(N'2019-10-29T14:20:42.443' AS DateTime), NULL)
INSERT [dbo].[UserConnections] ([Id], [ConnectionId], [IpAddress], [CultureCode], [UserId], [IsActive], [UpdateTime], [ConnectionOff]) VALUES (2, N'b65e206d-7cf9-4b17-880b-e94f495708c3', N'::1', N'ru-RU', 9, 0, CAST(N'2019-10-29T11:03:25.577' AS DateTime), NULL)
INSERT [dbo].[UserConnections] ([Id], [ConnectionId], [IpAddress], [CultureCode], [UserId], [IsActive], [UpdateTime], [ConnectionOff]) VALUES (57, N'bbc6f3e1-3d15-44b3-b831-687069e9634c', N'::1', N'ru-RU', 9, 0, CAST(N'2019-10-29T11:21:55.300' AS DateTime), NULL)
INSERT [dbo].[UserConnections] ([Id], [ConnectionId], [IpAddress], [CultureCode], [UserId], [IsActive], [UpdateTime], [ConnectionOff]) VALUES (58, N'593d4742-ef0f-416e-bc32-a040e50116e7', N'::1', N'ru-RU', 9, 0, CAST(N'2019-10-29T11:21:56.893' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[UserConnections] OFF
SET IDENTITY_INSERT [dbo].[UsersRelations] ON 

INSERT [dbo].[UsersRelations] ([Id], [OfferSendedDate], [UserOferFrienshipSender], [UserConfirmer], [Status], [IsInvitationSeen], [BlockEntrie]) VALUES (1, CAST(N'2019-10-29T11:20:24.717' AS DateTime), 10, 9, 1, 1, N'00000000-0000-0000-0000-000000000000')
SET IDENTITY_INSERT [dbo].[UsersRelations] OFF
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
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Posts_dbo.Users_UserPosted_Id] FOREIGN KEY([UserPosted_Id])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_dbo.Posts_dbo.Users_UserPosted_Id]
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
