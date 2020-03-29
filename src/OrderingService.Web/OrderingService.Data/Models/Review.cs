using System;
using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderingService.Data.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Guid ClientId { get; set; }
        public User Client { get; set; }
        public Guid EmployeeId { get; set; }
        public float Rate { get; set; }
        public EmployeeProfile Employee { get; set; }
    }

    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Text).HasMaxLength(255);
            builder.HasOne(x => x.Client)
                .WithMany().HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(x => x.Employee)
                .WithMany().HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Property(x => x.Rate)
                .IsRequired();
        }
    }
}
