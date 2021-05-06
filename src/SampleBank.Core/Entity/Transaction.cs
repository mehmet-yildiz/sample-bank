using System;
using SampleBank.Core.Enums;

namespace SampleBank.Core.Entity
{
    public class Transaction : BaseEntity
    {
        public virtual ProcessesType TransactionProcess { get; set; }
        public virtual DateTime ProcessDate { get; set; }
        public virtual int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}