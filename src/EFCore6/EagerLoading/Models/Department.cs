using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagerLoading.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // public Department Parent { get; set; }
        public ICollection<Department> Childs { get; set; }
    }
}
