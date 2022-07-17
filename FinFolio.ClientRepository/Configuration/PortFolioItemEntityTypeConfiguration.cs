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
            builder.Property(prop => prop.CostValue)
                .HasColumnType("decimal")
                .HasPrecision(5, 2)
                .IsRequired();
            builder.Property(prop => prop.NoOfUnits)
                .IsRequired();
            builder.Property(prop => prop.PurchaseDate)
                .HasColumnType("datetime")
                .IsRequired();
            builder.Property(prop => prop.IsSIP)
                .IsRequired(true);
            builder.Property(prop => prop.PortFolioItemType)
                .IsRequired();
            builder.HasOne(prop => prop.Scheme)
                .WithMany(prop => prop.PortFolioItems)
                .HasForeignKey(prop => prop.SchemeId)
                .HasConstraintName("FK_Scheme_PortFolioItem")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(prop => prop.PortFolio)
               .WithMany(prop => prop.Items)
               .HasConstraintName("FK_PortFolio_PortFolioItem")
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        }
    }
}
