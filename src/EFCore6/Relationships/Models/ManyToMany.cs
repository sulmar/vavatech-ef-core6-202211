using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relationships.Models
{
    // Many-to-many
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }

    public class Tag
    {
        public string TagId { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
