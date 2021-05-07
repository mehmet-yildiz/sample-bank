using System.Collections.Generic;

namespace SampleBank.Core.Entity
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
            Accounts = new HashSet<Account>();
        }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}