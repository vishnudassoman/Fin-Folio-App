namespace FinFolio.PortFolioRepository.Entities
{
    public class Wishlist : BaseEntity
    {
        public int UserID { get; set; }
        public int SchemeId { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual Scheme Scheme { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
