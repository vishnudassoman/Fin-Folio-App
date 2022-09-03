namespace FinFolio.Web.Models
{
    public class SaveAndListViewModel<T>
    {
        public OperationResultViewModel Result { get; set; }
        public List<T> ListItems { get; set; }
    }
}
