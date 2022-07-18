using FinFolio.PortFolioRepository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFolio.PortFolioRepository.Configuration
{
    public class WishlistEntityTypeConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.UserID)
               .IsRequired();
            builder.HasOne(prop => prop.Scheme)
                .WithMany(prop => prop.Wishlist)
                .HasForeignKey(prop => prop.SchemeId)
                .HasConstraintName("FK_Scheme_Wishlist")
                .IsRequired(true);
        }
    }
}
