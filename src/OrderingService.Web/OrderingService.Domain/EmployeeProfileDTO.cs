namespace OrderingService.Domain
{
    public class EmployeeProfileDTO
    {
        public int Id { get; set; }
        public ServiceTypeDTO ServiceType { get; set; }
        public decimal ServiceCost { get; set; }
    }
}
