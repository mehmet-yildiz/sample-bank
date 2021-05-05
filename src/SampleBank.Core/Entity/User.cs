namespace SampleBank.Core.Entity
{
    public class User : BaseEntity
    {
        public virtual string Username { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual byte[] PasswordSalt { get; set; }
        public virtual byte[] PasswordHash { get; set; }
    }
}