using System.Collections.Generic;

namespace SampleBank.Core.Entity
{
    public class Account : BaseEntity 
    {
        public virtual int UserId { get; set; }
        public virtual decimal Balance { get; set; }
        public virtual decimal InitialCredit { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
