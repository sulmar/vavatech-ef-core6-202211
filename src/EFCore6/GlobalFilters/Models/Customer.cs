namespace GlobalFilters.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Age { get; set; }
        public int TenantId { get; set; }
        public bool IsRemoved { get; set; }
        

        public Customer(string firstName, string lastName, byte age, int tenantId, bool isRemoved)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            IsRemoved = isRemoved;
            TenantId = tenantId;
        }
    }
}
