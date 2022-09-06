namespace FinFolio.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string ObjectIdentifier { get; set; }
        public string Email { get; set; }
        public DateTime LastLoginDateTimeUtc { get; set; }
    }
}