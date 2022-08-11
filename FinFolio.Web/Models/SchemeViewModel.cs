namespace FinFolio.Web.Models
{
    public class SchemeViewModel
    {
        public int Id { get; set; }
        public long Code { get; set; }
        public string AMC { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<SchemeNavViewModel> NAVHistory { get; set; }
        public string NavCSS
        {
            get
            {
                if (NAVHistory != null && NAVHistory.Count > 1)
                {
                    decimal currentNav = NAVHistory[0].Value;
                    decimal previousNav = NAVHistory[1].Value;
                    if (currentNav > previousNav)
                    {
                        return "bg-success arrow-up";
                    }
                    else if (currentNav < previousNav)
                    {
                        return "bg-danger arrow-down";
                    }
                }
                return "bg-dark";
            }
        }
        public DateTime LaunchDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
