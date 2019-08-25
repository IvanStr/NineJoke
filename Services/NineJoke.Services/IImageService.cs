namespace NineJoke.Services
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IImageService
    {
        Task<string> UploadImage(IFormFile formImage, string template, string gameName, string gameId);
    }
}
