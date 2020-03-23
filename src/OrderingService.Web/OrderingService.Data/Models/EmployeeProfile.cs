using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderingService.Data.Models
{
    public class EmployeeProfile
    {
        public Guid Id { get; set; }
        public ServiceType ServiceType { get; set; }
        public decimal ServiceCost { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
    }

    public class EmployeeProfileConfiguration : IEntityTypeConfiguration<EmployeeProfile>
    {
        public void Configure(EntityTypeBuilder<EmployeeProfile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.ServiceType)
                .WithMany();
            builder.Property(x => x.Description).HasMaxLength(255);
        }
    }
}
