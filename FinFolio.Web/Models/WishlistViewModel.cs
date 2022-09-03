namespace FinFolio.Web.Models
{
    public class WishlistViewModel
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public long Code { get; set; }
        public string NAVName { get; set; } = string.Empty;
        public decimal NAV { get; set; }
    }
}
