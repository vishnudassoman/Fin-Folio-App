using FinFolio.ClientRepository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFolio.ClientRepository.Configuration
{
    public class PortFolioEntityTypeConfiguration : IEntityTypeConfiguration<PortFolio>
    {
        public void Configure(EntityTypeBuilder<PortFolio> builder)
        {
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.UserId)
                .IsRequired();
            builder.Property(prop => prop.Name)
                .HasMaxLength(225)
                .IsRequired()
                .IsUnicode();
            builder.HasMany<PortFolioItem>(prop => prop.Items);

        }
    }
}
