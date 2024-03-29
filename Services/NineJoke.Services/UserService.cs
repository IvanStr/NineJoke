﻿namespace NineJoke.Services
{
    using System.Linq;

    using NineJoke.Data;
    using NineJoke.Data.Models;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ApplicationUser GetUserByName(string name)
        {
            var user = this.context.Users.FirstOrDefault(x => x.UserName == name);

            return user;
        }

        public ApplicationUser GetUserByPostId(string id)
        {
            string userId = this.context.Posts.FirstOrDefault(z => z.Id == id).UserId;
            return this.context.Users.FirstOrDefault(x => x.Id == userId);
        }
    }
}
