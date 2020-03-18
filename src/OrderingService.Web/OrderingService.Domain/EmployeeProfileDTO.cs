namespace OrderingService.Domain
{
    public class EmployeeProfileDTO
    {
        public string Id { get; set; }
        public string ServiceType { get; set; }
        public decimal ServiceCost { get; set; }
        public UserDTO User { get; set; }
    }
}
