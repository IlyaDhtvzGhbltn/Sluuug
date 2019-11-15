USE [slugDtB]
GO
SET IDENTITY_INSERT [dbo].[Avatars] ON 

INSERT [dbo].[Avatars] ([Id], [UploadTime], [IsStandart], [CountryCode], [LargeAvatar], [MediumAvatar], [SmallAvatar], [AvatarType]) VALUES (1, CAST(N'2019-11-11T13:23:14.940' AS DateTime), 1, 375, N'https://res.cloudinary.com/dlk1sqmj4/image/upload/v1569835906/system/standart_avatars/no-avatar-bel.jpg', N'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_100,w_100,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-bel.jpg', N'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_50,w_50,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-bel.jpg', 0)
INSERT [dbo].[Avatars] ([Id], [UploadTime], [IsStandart], [CountryCode], [LargeAvatar], [MediumAvatar], [SmallAvatar], [AvatarType]) VALUES (2, CAST(N'2019-11-11T13:23:14.940' AS DateTime), 1, 380, N'https://res.cloudinary.com/dlk1sqmj4/image/upload/v1569835906/system/standart_avatars/no-avatar-ua.jpg', N'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_100,w_100,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-ua.jpg', N'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_50,w_50,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-ua.jpg', 0)
INSERT [dbo].[Avatars] ([Id], [UploadTime], [IsStandart], [CountryCode], [LargeAvatar], [MediumAvatar], [SmallAvatar], [AvatarType]) VALUES (3, CAST(N'2019-11-11T13:23:14.940' AS DateTime), 1, 7, N'https://res.cloudinary.com/dlk1sqmj4/image/upload/v1569835906/system/standart_avatars/no-avatar-ru.jpg', N'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_100,w_100,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-ru.jpg', N'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_50,w_50,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-ru.jpg', 0)
SET IDENTITY_INSERT [dbo].[Avatars] OFF
