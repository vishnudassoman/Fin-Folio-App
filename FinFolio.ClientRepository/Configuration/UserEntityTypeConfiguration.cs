using FinFolio.PortFolioRepository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFolio.PortFolioRepository.Configuration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.ObjectIdentifier)
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasMaxLength(36)
                .IsRequired();
        }
    }
}
