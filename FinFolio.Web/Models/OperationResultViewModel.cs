namespace FinFolio.Web.Models
{
    public class OperationResultViewModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public bool ShowAlert { get; set; }
        public AlertType Type { get; set; }
    }
}
