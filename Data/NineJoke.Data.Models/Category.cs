namespace NineJoke.Data.Models
{
    using System.Collections.Generic;

    using NineJoke.Data.Common.Models;

    public class Category : BaseModel<string>
    {
        public Category()
        {
            this.Posts = new HashSet<Post>();
        }

        public string Name { get; set; }

        public int Popularity { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
