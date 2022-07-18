namespace FinFolio.PortFolio.DTO
{
    public class Scheme
    {
        public long Code { get; set; }
        public string NAVName { get; set; } = string.Empty;
        public DateTime LaunchDate { get; set; }
        public bool? IsActive { get; set; }
    }
}