using System;

namespace OrderingService.Domain
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public UserDTO Client { get; set; }
        public int EmployeeId { get; set; }
        public UserDTO Employee { get; set; }
    }
}
