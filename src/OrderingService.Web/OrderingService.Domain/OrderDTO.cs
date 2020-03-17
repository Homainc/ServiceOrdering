using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public UserDTO Client { get; set; }
        public UserDTO Employee { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
