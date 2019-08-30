namespace NineJoke.Services
{
    using NineJoke.Data.Models;

    public interface IUserService
    {
        ApplicationUser GetUserByName(string name);

        ApplicationUser GetUserByPostId(string id);
    }
}
