using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SampleBank.Core.Entity
{
    public class Account : BaseEntity 
    {
        public Account()
        {
            Transactions = new HashSet<Transaction>();
        }
        public virtual int CustomerId { get; set; }
        public virtual decimal Balance { get; set; }
        public virtual decimal InitialCredit { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
    }
}
