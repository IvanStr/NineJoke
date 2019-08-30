namespace NineJoke.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using NineJoke.Data;
    using NineJoke.Data.Models;
    using Xunit;

    public class UserServiceTests
    {
        [Fact]
        public void GetUserByNameShouldReturnCorrectUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(databaseName: "GetUserByNameShouldReturnCorrectUserDB")
                  .Options;

            var dbContext = new ApplicationDbContext(options);

            var usersService = new UserService(dbContext);

            var user = new ApplicationUser
            {
                UserName = "Zippo",
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var userTest = usersService.GetUserByName(user.UserName);

            Assert.Equal(user.UserName, userTest.UserName);
        }

        [Fact]
        public void GetUserByPostIdShouldReturnCorrectUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(databaseName: "GetUserByPostIdShouldReturnCorrectUserDB")
                  .Options;

            var dbContext = new ApplicationDbContext(options);

            var usersService = new UserService(dbContext);

            var user = new ApplicationUser
            {
                UserName = "Zippo",
            };

            var post = new Post
            {
                UserId = user.Id,
            };

            dbContext.Users.Add(user);
            dbContext.Posts.Add(post);
            dbContext.SaveChanges();

            var userTest = usersService.GetUserByPostId(post.Id);

            Assert.Equal(user.UserName, userTest.UserName);
        }
    }
}
