namespace SampleBank.Core.Entity
{
    public interface IEntityKey<out TKey>
    {
        TKey Id { get; }
    }
}