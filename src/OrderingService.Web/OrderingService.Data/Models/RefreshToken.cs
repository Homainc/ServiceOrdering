using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrderingService.Data.Models
{
    public class RefreshToken
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }

    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Token).HasMaxLength(100);
        }
    }
}
