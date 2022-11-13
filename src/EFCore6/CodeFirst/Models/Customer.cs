namespace CodeFirst.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public MembershipType MembershipType { get; set; }
        public decimal Debit { get; set; }      
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool CanSend { get; set; }

        public override string ToString() => $"{FirstName} {LastName} {MembershipType} {CreatedOn} {CanSend}";
    }

    public enum MembershipType
    {
        Free,
        Standard,
        Premium
    }
}
