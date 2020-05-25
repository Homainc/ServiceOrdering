namespace OrderingService.Domain
{
    public class ServiceTypeCreateDto
    {
        public string Name { get; set; }
    }

    public class ServiceTypeDto : ServiceTypeCreateDto
    {
        public int Id { get; set; }
    }
}
