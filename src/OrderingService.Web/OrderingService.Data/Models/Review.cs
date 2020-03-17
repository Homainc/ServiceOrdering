using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderingService.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public User Client { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public User Employee { get; set; }
    }
}
