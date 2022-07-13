namespace FinFolio.ClientRepository.Entities
{
    public class PortFolioItem : BaseEntity
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int NoOfUnits { get; set; }
        public decimal CostValue { get; set; }
        public DateTime PurchaseDateTimeUTC { get; set; }
        public bool IsSIP { get; set; }
        public ItemType PortFolioItemType { get; set; }
    }
}
