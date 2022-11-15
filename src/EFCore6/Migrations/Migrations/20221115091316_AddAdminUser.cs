using Microsoft.EntityFrameworkCore.Migrations;
using System.Xml.Linq;

#nullable disable

namespace Migrations.Migrations
{
    public partial class AddAdminUser : Migration
    {
        // Utworzenie własnej operacji:
        // https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/operations

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string name = "Admin";
            string password = "123";

            migrationBuilder.Sql($"CREATE LOGIN {name} WITH PASSWORD = '{password}'");            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string name = "Admin";

            migrationBuilder.Sql($"DROP LOGIN {name}");
        }
    }
}
