using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoading.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }

    public class Blog : BaseEntity
    {
        private ILazyLoader lazyLoader;

        public Blog()
        {

        }

        private Blog(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }

        public int Id { get; set; }
        public string Title { get; set; }

        private Person _owner;
        public Person Owner 
        {
            get => lazyLoader.Load(this, ref _owner);
            set => _owner = value;
        }

        private ICollection<Post> _posts;
        public ICollection<Post> Posts 
        { 
            get => lazyLoader.Load(this, ref _posts); 
            set => _posts = value;
        }

    }

    public class Person : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Post : BaseEntity
    {
        public string Slug { get; set; }
        public string Content { get; set; }
        public byte Rating { get; set; }
        public Person Author { get; set; }

        public override string ToString() => $"[{Slug}] {new string('*', Rating)} \n {Content} \n";

    }


}
