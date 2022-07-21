namespace FinFolio.PortFolio.DTO
{
    public class PortFolioItemDto
    {
        public int Id { get; set; }
        public int NoOfUnits { get; set; }
        public decimal CostValue { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsSIP { get; set; }
        public ItemTypeDto PortFolioItemType { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SchemeDto Scheme { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}