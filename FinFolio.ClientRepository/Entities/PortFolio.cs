namespace FinFolio.ClientRepository.Entities
{
    public class PortFolio : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<PortFolioItem> Items { get; set; }
    }
}
