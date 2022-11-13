namespace Relationships.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public EmployeePhoto Photo { get; set; }
    }

    public class EmployeePhoto
    {
        public int EmployeePhotoId { get; set; }
        public byte[] Image { get; set; }
        public string ContentType { get; set; }
    }
}
