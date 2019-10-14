 delete from [Users] where UserStatus = 0;
 DELETE FROM [UserSettings] WHERE (SELECT COUNT(*) FROM [Users] WHERE [Users].Id = [UserSettings].Id) = 0;
 DELETE FROM [UserInfoes] WHERE (SELECT COUNT(*) FROM [Users] WHERE [Users].Id = [UserInfoes].Id) = 0;
