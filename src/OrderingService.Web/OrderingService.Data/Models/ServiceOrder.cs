using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderingService.Data.Models
{
    public class ServiceOrder
    {
        [Key]
        public int Id { get; set; }
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public User Client { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public User Employee { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal Price { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Date { get; set; }
    }
}
