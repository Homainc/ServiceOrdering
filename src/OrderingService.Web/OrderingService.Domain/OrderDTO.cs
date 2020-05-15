using System;
using OrderingService.Common;

namespace OrderingService.Domain
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public Guid ClientId { get; set; }
        public UserDto Client { get; set; }
        public Guid EmployeeId { get; set; }
        public EmployeeProfileDTO Employee { get; set; }
        public string BriefTask { get; set; }
        public string ServiceDetails { get; set; }
        public string Address { get; set; }
        public string ContactPhone { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
    }
}
