using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain
{
    public class EmployeeProfileDTO
    {
        public int Id { get; set; }
        public ServiceTypeDTO ServiceType { get; set; }
        public decimal ServiceCost { get; set; }
    }
}
