namespace FinFolio.PortFolio.DTO
{
    public class SchemeRequestDto
    {
        public SchemeRequestDto()
        {
            NAVName = string.Empty;
        }
        public int Id { get; set; }
        public int Code { get; set; }
        public string NAVName { get; set; }
    }
}
