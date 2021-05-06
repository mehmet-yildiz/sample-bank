namespace SampleBank.Core.Model.Account
{
    public class OpenAccountModel : BaseModel
    {
        public int CustomerId { get; set; }
        public decimal InitialCredit { get; set; }
    }
}
