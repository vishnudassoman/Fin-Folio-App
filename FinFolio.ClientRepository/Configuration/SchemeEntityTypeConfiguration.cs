using FinFolio.PortFolioRepository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFolio.PortFolioRepository.Configuration
{
    public class SchemeEntityTypeConfiguration : IEntityTypeConfiguration<Scheme>
    {
        public void Configure(EntityTypeBuilder<Scheme> builder)
        {
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Code)
                .HasColumnType("int")
                .IsRequired();
            builder.Property(prop => prop.AMC)
                .HasColumnType("nvarchar(255)")
                .IsRequired();
            builder.Property(prop => prop.NAVName)
                .HasColumnType("nvarchar(255)")
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(prop => prop.LaunchDate)
                .HasColumnType("datetime")
                .IsRequired();
            builder.Property(prop => prop.IsActive)
                .HasColumnType("bit")
                .IsRequired()
                .HasDefaultValue(true);
            builder.HasMany(prop => prop.PortFolioItems)
                .WithOne(p => p.Scheme)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
            builder.HasMany(prop => prop.Wishlist)
               .WithOne(p => p.Scheme)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired(false);
        }
    }
}
