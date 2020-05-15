using System;

namespace OrderingService.Domain
{
    public class EmployeeProfileDtoBase
    {
        public string ServiceType { get; set; }
        public decimal ServiceCost { get; set; }
        public string Description { get; set; }
    }

    public class EmployeeProfileCreateDto : EmployeeProfileDtoBase
    {
        public Guid UserId { get; set; }
    }

    public class EmployeeProfileUpdateDto : EmployeeProfileDtoBase
    {
        public Guid Id { get; set; }
    }

    public class EmployeeProfileDto : EmployeeProfileDtoBase
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
        public double? AverageRate { get; set; }
        public int ReviewsCount { get; set; }
    }
}
