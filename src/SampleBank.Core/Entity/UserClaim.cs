namespace SampleBank.Core.Entity
{
    public class UserClaim : BaseEntity
    {
        public virtual int UserId { get; set; }  
        public virtual int ClaimId { get; set; }

        public virtual User User { get; set; }
        public virtual Claim Claim { get; set; }
    }
}