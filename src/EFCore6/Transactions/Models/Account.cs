namespace Transactions.Models
{
    public class Account
    {
        public int Id { get; set; }        
        public string Owner { get; set; }
        public decimal Balance { get; set; }

        public static readonly decimal BalanceLimit = 1200m;

        public override string ToString() => $"{Owner} {Balance:C2}";
    }
}
