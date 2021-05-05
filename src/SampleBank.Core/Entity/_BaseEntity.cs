namespace SampleBank.Core.Entity
{
    public class BaseEntity : IEntityKey<int>
    {
        public virtual int Id { get; set; }
    }
}