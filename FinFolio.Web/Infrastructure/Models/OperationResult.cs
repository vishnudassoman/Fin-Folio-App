namespace FinFolio.Web.Infrastructure.Models
{
    public class OperationResult
    {
        public OperationResult()
        {

        }

        public OperationResult(string message)
        {
            ErrorMessage = message;
            IsSuccess = false;
        }

        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

    }

    public class OperationResult<T> : OperationResult
    {
        public OperationResult()
        {

        }

        public OperationResult(string message) : base(message)
        {
        }

        public OperationResult(T data)
        {
            Data = data;
            IsSuccess = true;
        }


        public T? Data { get; set; }
    }
}
