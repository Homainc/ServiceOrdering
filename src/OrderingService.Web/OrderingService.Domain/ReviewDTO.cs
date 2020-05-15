using System;

namespace OrderingService.Domain
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rate { get; set; }
        public DateTime Date { get; set; }
        public Guid ClientId { get; set; }
        public UserDto Client { get; set; }
        public Guid EmployeeId { get; set; }
        public EmployeeProfileDTO Employee { get; set; }
    }
}
