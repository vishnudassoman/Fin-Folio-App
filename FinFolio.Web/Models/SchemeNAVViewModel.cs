namespace FinFolio.Web.Models
{
    public class SchemeNavViewModel
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public decimal PreviousNAV { get; set; }
        public decimal LatestNAV { get; set; }
        public string NavCSS
        {
            get
            {
                if (LatestNAV > 0)
                {
                    if (LatestNAV > PreviousNAV)
                    {
                        return "bg-success arrow-up";
                    }
                    else if (LatestNAV < PreviousNAV)
                    {
                        return "bg-danger arrow-down";
                    }
                }
                return "bg-dark";
            }
        }
    }
}
