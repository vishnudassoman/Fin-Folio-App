namespace FinFolio.Web.Infrastructure.Models
{
    public class Nav
    {
        public string date { get; set; }
        public string nav { get; set; }
    }

    public class SchemeData
    {
        public string fund_house { get; set; }
        public string scheme_type { get; set; }
        public string scheme_category { get; set; }
        public int scheme_code { get; set; }
        public string scheme_name { get; set; }
    }

    public class MfApiData
    {
        public SchemeData meta { get; set; }
        public List<Nav> data { get; set; }
        public string status { get; set; }
    }
}
