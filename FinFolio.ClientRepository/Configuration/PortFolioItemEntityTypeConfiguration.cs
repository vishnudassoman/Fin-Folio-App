using FinFolio.ClientRepository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFolio.ClientRepository.Configuration
{
    public class PortFolioItemEntityTypeConfiguration : IEntityTypeConfiguration<PortFolioItem>
    {
        public void Configure(EntityTypeBuilder<PortFolioItem> builder)
        {
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.ItemCode)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(false);
            builder.Property(prop => prop.CostValue)
                .HasPrecision(5, 2)
                .IsRequired();
            builder.Property(prop => prop.NoOfUnits)
                .IsRequired();
            builder.Property(prop => prop.PurchaseDateTimeUTC)
                .IsRequired();
            builder.Property(prop => prop.IsSIP)
                .IsRequired(false);
            builder.Property(prop => prop.ItemName)
                .IsRequired(true)
                .HasMaxLength(200);
            builder.Property(prop => prop.PortFolioItemType)
                .IsRequired();

        }
    }
}
