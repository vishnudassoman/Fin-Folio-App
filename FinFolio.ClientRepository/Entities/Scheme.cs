namespace FinFolio.PortFolioRepository.Entities
{
    public class Scheme : BaseEntity
    {
        public long Code { get; set; }
        public string NAVName { get; set; } = string.Empty;
        public DateTime LaunchDate { get; set; }
        public bool? IsActive { get; set; }
        public virtual List<PortFolioItem>? PortFolioItems { get; set; }
        public virtual List<Wishlist>? Wishlist { get; set; }
    }
}
