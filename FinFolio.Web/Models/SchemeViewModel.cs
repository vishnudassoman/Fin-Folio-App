namespace FinFolio.Web.Models
{
    public class SchemeViewModel
    {
        public int Id { get; set; }
        public long Code { get; set; }
        public string AMC { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal NAV { get; set; }
        public DateTime LaunchDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
