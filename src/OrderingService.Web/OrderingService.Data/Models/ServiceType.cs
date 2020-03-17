using System.ComponentModel.DataAnnotations;

namespace OrderingService.Data.Models
{
    public class ServiceType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
