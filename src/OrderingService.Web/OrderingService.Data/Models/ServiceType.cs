﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderingService.Data.Models
{
    public class ServiceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceType>{
        public void Configure(EntityTypeBuilder<ServiceType> builder){
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Name)
                .HasMaxLength(50);
        }
    }
}
