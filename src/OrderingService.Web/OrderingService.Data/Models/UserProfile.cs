using System.ComponentModel.DataAnnotations;

namespace OrderingService.Data.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
