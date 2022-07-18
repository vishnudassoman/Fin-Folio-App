namespace FinFolio.PortFolio.DTO
{
    public class PortFolioItem
    {
        public int Id { get; set; }
        public int NoOfUnits { get; set; }
        public decimal CostValue { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsSIP { get; set; }
        public ItemType PortFolioItemType { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Scheme Scheme { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}