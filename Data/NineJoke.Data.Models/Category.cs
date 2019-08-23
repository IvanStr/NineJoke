﻿using NineJoke.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NineJoke.Data.Models
{
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
