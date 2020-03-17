using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OrderingService.Data.Models
{
    public class UserProfile
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        public EmployeeProfile EmployeeProfile { get; set; }
        public User User { get; set; }
    }
}
