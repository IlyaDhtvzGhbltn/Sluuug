
  truncate table [slugDtB].[dbo].[Avatars];
  insert into [slugDtB].[dbo].[Avatars](UploadTime, IsStandart, CountryCode, ImgPath)
  values
  (GETDATE(), 1, 7, 'https://res.cloudinary.com/dlk1sqmj4/image/upload/v1569835906/system/standart_avatars/no-avatar-ru.jpg'),
  (GETDATE(), 1, 380, 'https://res.cloudinary.com/dlk1sqmj4/image/upload/v1569835906/system/standart_avatars/no-avatar-ua.jpg'),
  (GETDATE(), 1, 375, 'https://res.cloudinary.com/dlk1sqmj4/image/upload/v1569835906/system/standart_avatars/no-avatar-bel.jpg')
