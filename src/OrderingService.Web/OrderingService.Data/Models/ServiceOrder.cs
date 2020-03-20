﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderingService.Data.Models
{
    public class ServiceOrder
    {
        [Key]
        public int Id { get; set; }
        public string ClientId { get; set; }
        [ForeignKey("ClientId")]
        public User Client { get; set; }
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public User Employee { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public bool IsClosed { get; set; }
    }
}