namespace FinFolio.ClientRepository.Entities
{
    public class Scheme : BaseEntity
    {
        public long Code { get; set; }
        public string NAVName { get; set; } = string.Empty;
        public DateTime LaunchDate { get; set; }
        public bool? IsActive { get; set; }
        public List<PortFolioItem>? PortFolioItems { get; set; }
        public List<Wishlist>? Wishlist { get; set; }
    }
}
