namespace FinFolio.ClientRepository.Entities
{
    public class PortFolio : BaseEntity
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public ICollection<PortFolioItem> Items { get; set; }
    }
}
