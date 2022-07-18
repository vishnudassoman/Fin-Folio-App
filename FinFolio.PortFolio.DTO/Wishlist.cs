namespace FinFolio.PortFolio.DTO
{
    public class Wishlist
    {
        public int Id { get; set; }
        public int UserID { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Scheme Scheme { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}