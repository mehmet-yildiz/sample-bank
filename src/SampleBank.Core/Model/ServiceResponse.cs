namespace SampleBank.Core.Model
{
    public class ServiceResponse
    {
        public string ErrorMessage { get; set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public T Data { get; set; }
    }
}
