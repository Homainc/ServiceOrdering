using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderingService.Data.Models
{
    public class EmployeeProfile
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public ServiceType ServiceType { get; set; }
        public decimal ServiceCost { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
    }
}
