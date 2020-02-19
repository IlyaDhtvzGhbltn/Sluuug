using SharedModels.Enums;
using SharedModels.Users.Registration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Context
{
    // TODO - REFACTOR THIS CLASS AND Context in Slug Wep.App
    // CREATE SHARED ASSEMBLY FOR `DataBaseContext`
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("name=DBConnection")
        {
            Database.Initialize(false);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserInfo> UsersInfo { get; set; }
        public virtual DbSet<UserSettings> UserSettings { get; set; }
        public virtual DbSet<UserStatus> UserStatuses { get; set; }
        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<ConversationGroup> ConversationGroup { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<UsersRelation> UserRelations { get; set; }
        public virtual DbSet<FakeUser> FakeUsers { get; set; }
    }

    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public int UserStatus { get; set; }
        public Guid AvatarGuidId { get; set; }
        public RegistrationTypeService UserType { get; set; }
        public DateTime RegisterDate { get; set; }
        public int? ReferalUserId { get; set; }
        public bool IsFakeBot { get; set; }

        public virtual UserInfo UserFullInfo { get; set; }
        public virtual UserSettings Settings { get; set; }
    }
    public class UserInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string SurName { get; set; }
        [Required]
        public int Sex { get; set; }
        public int NowCountryCode { get; set; }
        public int NowCityCode { get; set; }
        public string HelloMessage { get; set; }
        public int userDatingSex { get; set; }
        public int DatingPurpose { get; set; }
        public int userDatingAge { get; set; }
        public long IdVkUser { get; set; }
        public long IdFBUser { get; set; }
        public long IdOkUser { get; set; }

        public virtual List<Education> Educations { get; set; }
        public virtual List<ImportantEvent> Events { get; set; }
        public virtual List<WorkPlaces> Works { get; set; }
        public DateTime? VipStatusExpiredDate { get; set; }
    }
    public class UserSettings
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public NotificationType NotificationType { get; set; }

        [Required]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        [Required]
        public bool QuickMessage { get; set; }
    }

    public class UserStatus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }
    }
    public class Conversation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public Guid ConversationGuidId { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
    }
    public class ConversationGroup
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Guid ConversationGuidId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Guid ConvarsationGuidId { get; set; }
        [Required]
        public int UserId { get; set; }
        [MaxLength(5000)]
        public string Text { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime SendingDate { get; set; }
        [Required]
        public bool IsReaded { get; set; }
    }
    public class UsersRelation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OfferSendedDate { get; set; }
        [Required]
        public FriendshipItemStatus Status { get; set; }

        public virtual User UserOferFrienshipSender { get; set; }
        public virtual User UserConfirmer { get; set; }
        public bool IsInvitationSeen { get; set; }
        public Guid BlockEntrie { get; set; }
    }
    public class FakeUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CityCode { get; set; }
        [Required]
        public int CountryCode { get; set; }
        [Required]
        public int UsersCount { get; set; }
    }

    public class Education : BaseLiveEpisode
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public EducationTypes EducationType { get; set; }
        [Required]
        public string Title { get; set; }
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public virtual User User { get; set; }
    }
    public class BaseLiveEpisode
    {
        [Required]
        public int CountryCode { get; set; }
        [Required]
        public int CityCode { get; set; }
        [Required]
        public bool UntilNow { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }
        [DataType(DataType.Date)]
        public DateTime? End { get; set; }
        [MaxLength(500)]
        public string Comment { get; set; }
        [Range(0, 5)]
        public int PersonalRating { get; set; }
    }
    public class ImportantEvent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid AlbumGuid { get; set; }

        [Required]
        public string EventTitle { get; set; }

        [Required]
        public DateTime DateEvent { get; set; }

        [Required]
        public virtual User User { get; set; }

        public string TextEventDescription { get; set; }

        public virtual List<Foto> Photos { get; set; }
    }
    public class Foto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Guid FotoGUID { get; set; }
        [Required]
        public Guid AlbumID { get; set; }
        public string Title { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime UploadDate { get; set; }
        [Required]
        public int UploadUserID { get; set; }
        [Required]
        public string ImagePublicID { get; set; }
        public long Height { get; set; }
        public long Width { get; set; }
        public int PositiveRating { get; set; }
        public int NegativeRating { get; set; }
        public string Description { get; set; }
        public virtual List<FotoComment> UserComments { get; set; }
    }
    public class FotoComment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int UserCommenter { get; set; }
        [Required]
        [MaxLength(5000)]
        public string CommentText { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CommentWriteDate { get; set; }
        public virtual Foto Foto { get; set; }
    }
    public class WorkPlaces : BaseLiveEpisode
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string CompanyTitle { get; set; }
        [Required]
        public string Position { get; set; }
        public virtual User User { get; set; }
    }

}
