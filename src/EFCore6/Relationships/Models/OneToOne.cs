namespace Relationships.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public EmployeePhoto EmployeePhoto { get; set; }
    }

    public class EmployeePhoto
    {
        public int EmployeePhotoId { get; set; }
        public byte[] Image { get; set; }
        public string ContentType { get; set; }
        // public int EmployeeId { get; set; }

        // public int EmployeeRefId { get; set; }
        public Employee Employee { get; set; }
    }
}
