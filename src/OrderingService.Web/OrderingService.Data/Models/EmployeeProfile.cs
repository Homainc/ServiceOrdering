using System.Security.Cryptography.X509Certificates;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingService.Data.EF;

namespace OrderingService.Data.Models
{
    public class EmployeeProfile
    {
        public Guid Id { get; set; }
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public decimal ServiceCost { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public double? AverageRate { get; private set; }
        public int ReviewsCount { get; private set; }

        internal EmployeeProfile() => User = new User();

        internal EmployeeProfile(double? averageRate, int reviewsCount)
        {
            AverageRate = averageRate;
            ReviewsCount = reviewsCount;
            User = new User();
        }

        public void AddReview(ApplicationContext context, Review review)
        {
            var reviews = context.Reviews.Where(x => x.EmployeeId == Id).ToList();
            if(reviews.Count > 0)
                AverageRate = (reviews.Sum(x => x.Rate) + review.Rate) / (reviews.Count + 1);
            else
                AverageRate = review.Rate;
            ReviewsCount++;
        }
    }

    public class EmployeeProfileConfiguration : IEntityTypeConfiguration<EmployeeProfile>
    {
        public void Configure(EntityTypeBuilder<EmployeeProfile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.HasOne(x => x.ServiceType)
                .WithMany()
                .HasForeignKey(x => x.ServiceTypeId);
            builder.Property(x => x.Description)
                .HasMaxLength(500);
            builder.Property(x => x.ServiceCost)
                .HasColumnType("decimal(18,4)");
        }
    }
}
