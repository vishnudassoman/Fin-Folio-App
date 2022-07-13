using FinFolio.ClientRepository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinFolio.ClientRepository.Configuration
{
    public class WishlistEntityTypeConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.UserID)
               .IsRequired();
            builder.Property(prop => prop.ItemCode)
               .HasColumnType("varchar(100)")
               .HasMaxLength(100)
               .IsRequired()
               .IsUnicode(false);
            builder.Property(prop => prop.ItemName)
                .HasColumnType("varchar(200)")
                .IsRequired(true)
                .HasMaxLength(200);
        }
    }
}
