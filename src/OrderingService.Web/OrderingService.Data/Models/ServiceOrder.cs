using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderingService.Data.Models
{
    public class ServiceOrder
    {
        public int Id { get; set; }
        public Guid ClientId { get; set; }
        public User Client { get; set; }
        public Guid EmployeeId { get; set; }
        public User Employee { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public bool IsClosed { get; set; }
    }

    public class ServiceOrderConfiguration : IEntityTypeConfiguration<ServiceOrder>{
        public void Configure(EntityTypeBuilder<ServiceOrder> builder){
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Description)
                .HasMaxLength(255);
            builder.HasOne(x => x.Client)
                .WithMany()
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,4)");
        }
    } 
}
