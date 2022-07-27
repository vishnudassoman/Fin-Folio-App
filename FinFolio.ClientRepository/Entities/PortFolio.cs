namespace FinFolio.PortFolioRepository.Entities
{
    public class PortFolio : BaseEntity
    {
        public int UserID { get; set; }
        public string Name { get; set; } = string.Empty;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual List<PortFolioItem> Items { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
