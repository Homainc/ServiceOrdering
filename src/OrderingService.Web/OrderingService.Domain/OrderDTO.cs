﻿using System;

namespace OrderingService.Domain
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public UserDTO Client { get; set; }
        public string EmployeeId { get; set; }
        public UserDTO Employee { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public bool IsClosed { get; set; }
    }
}