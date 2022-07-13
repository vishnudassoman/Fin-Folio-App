namespace FinFolio.ClientRepository.Entities
{
    public class Wishlist : BaseEntity
    {
        public int UserID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
    }
}
