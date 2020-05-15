using System;
using OrderingService.Common;

namespace OrderingService.Domain
{
    public class OrderCreateDto
    {
        public Guid ClientId { get; set; }
        public Guid EmployeeId { get; set; }
        public string BriefTask { get; set; }
        public string ServiceDetails { get; set; }
        public string Address { get; set; }
        public string ContactPhone { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderDto : OrderCreateDto
    {
        public int Id { get; set; }
        public UserDto Client { get; set; }
        public EmployeeProfileDto Employee { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
    }
}
