using System;
using Microsoft.EntityFrameworkCore;
using OrderingService.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderingService.Data.Models
{
    public class ServiceOrder
    {
        public int Id { get; set; }
        public Guid ClientId { get; set; }
        public User Client { get; set; }
        public Guid? EmployeeId { get; set; }
        public EmployeeProfile Employee { get; set; }
        public string ServiceDetails { get; set; }
        public string BriefTask { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string ContactPhone { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
    }

    public class ServiceOrderConfiguration : IEntityTypeConfiguration<ServiceOrder>{
        public void Configure(EntityTypeBuilder<ServiceOrder> builder){
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.BriefTask)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.ServiceDetails)
                .HasMaxLength(255);
            builder.HasOne(x => x.Client)
                .WithMany()
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,4)");
        }
    } 
}
