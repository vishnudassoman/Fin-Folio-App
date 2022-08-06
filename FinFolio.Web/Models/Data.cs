namespace FinFolio.Web.Models
{
    public class Data
    {
        public int take { get; set; }
        public List<Wheres>? where { get; set; }
    }

    public class Wheres
    {
        public string? field { get; set; }
        public bool ignoreAccent { get; set; }

        public bool ignoreCase { get; set; }

        public bool isComplex { get; set; }

        public string? value { get; set; }
        public string? Operator { get; set; }

    }

}
