using System.ComponentModel.DataAnnotations;

namespace OrderingService.Data.Models
{
    public class EmployeeProfile
    {
        [Key]
        public int Id { get; set; }
        public ServiceType ServiceType { get; set; }
        public decimal ServiceCost { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
