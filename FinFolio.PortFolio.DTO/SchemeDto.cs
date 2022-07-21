namespace FinFolio.PortFolio.DTO
{
    public class SchemeDto
    {
        public int Id { get; set; }
        public long Code { get; set; }
        public string NAVName { get; set; } = string.Empty;
        public DateTime LaunchDate { get; set; }
        public bool? IsActive { get; set; }
    }
}