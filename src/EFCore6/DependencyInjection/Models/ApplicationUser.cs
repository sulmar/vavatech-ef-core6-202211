namespace DependencyInjection.Models
{

    //public class IdentityUser
    //{
    //    public int Id { get; set; }
    //    public string Username { get; set; }
    //    public string Password { get; set; }
    //}


    public class ApplicationUser 
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccountNumber { get; set; }
    }

    public class CRMUser 
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}
