
  truncate table [Avatars];
  insert into [Avatars](UploadTime, IsStandart, CountryCode, LargeAvatar, MediumAvatar, SmallAvatar, AvatarType)
  values
  (GETDATE(), 1, 375, 
  'https://res.cloudinary.com/dlk1sqmj4/image/upload/v1569835906/system/standart_avatars/no-avatar-bel.jpg',
  'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_100,w_100,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-bel.jpg',
  'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_50,w_50,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-bel.jpg', 
  0),
  
  
  (GETDATE(), 1, 380, 
  'https://res.cloudinary.com/dlk1sqmj4/image/upload/v1569835906/system/standart_avatars/no-avatar-ua.jpg',
  'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_100,w_100,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-ua.jpg',
  'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_50,w_50,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-ua.jpg',
  0),
  
  
  (GETDATE(), 1, 7, 
  'https://res.cloudinary.com/dlk1sqmj4/image/upload/v1569835906/system/standart_avatars/no-avatar-ru.jpg',
  'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_100,w_100,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-ru.jpg',
  'https://res.cloudinary.com/dlk1sqmj4/image/upload/c_scale,h_50,w_50,c_thumb,g_face/v1569835906/system/standart_avatars/no-avatar-ru.jpg',
  0)