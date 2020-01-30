using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slug.Context.Tables
{
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
}