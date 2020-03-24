using System;

namespace OrderingService.Domain
{
    public class EmployeeProfileDTO
    {
        public Guid Id { get; set; }
        public string ServiceType { get; set; }
        public decimal ServiceCost { get; set; }
        public string Description { get; set; }
        public UserDTO User { get; set; }

        public EmployeeProfileDTO() => User = new UserDTO();
    }
}
