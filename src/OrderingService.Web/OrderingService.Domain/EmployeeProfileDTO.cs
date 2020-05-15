using System;

namespace OrderingService.Domain
{
    public class EmployeeProfileDTO
    {
        public Guid Id { get; set; }
        public string ServiceType { get; set; }
        public decimal ServiceCost { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
        public double? AverageRate { get; set; }
        public int ReviewsCount { get; set; }
    }
}
