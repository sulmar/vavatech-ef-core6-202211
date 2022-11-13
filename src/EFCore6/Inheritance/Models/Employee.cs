namespace Inheritance.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }

    public abstract class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        protected Employee(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }

    public class FullTimeEmployee : Employee
    {
        public FullTimeEmployee(string firstName, string lastName) : base(firstName, lastName)
        {
        }

        public decimal Salary { get; set; }
    }

    public class Contractor : Employee
    {
        public Contractor(string firstName, string lastName, DateTime from, DateTime to, decimal ratePerHour) : base(firstName, lastName)
        {
            From = from;
            To = to;
            RatePerHour = ratePerHour;
        }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal RatePerHour { get; set; }
    }

}
