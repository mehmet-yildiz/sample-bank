using System.Runtime.Serialization;

namespace SampleBank.API.Model
{
    [DataContract]
    public class ApiResponse<T>
    {
        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public T Data { get; set; }
    }
}
