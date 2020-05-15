using System;

namespace OrderingService.Domain
{
    public class ReviewCreateDto
    {
        public string Text { get; set; }
        public int Rate { get; set; }
        public Guid ClientId { get; set; }
        public Guid EmployeeId { get; set; }
    }

    public class ReviewDto : ReviewCreateDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public UserDto Client { get; set; }
        public EmployeeProfileDto Employee { get; set; }
    }
}
