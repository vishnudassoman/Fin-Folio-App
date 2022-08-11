namespace FinFolio.PortFolio.DTO
{
    public class SchemeDto
    {
        public int Id { get; set; }
        public long Code { get; set; }
        public string AMC { get; set; } = string.Empty;
        public string NAVName { get; set; } = string.Empty;
        public DateTime LaunchDate { get; set; }
        public bool? IsActive { get; set; }
        public List<NavDto> NAV { get; set; }
    }
}