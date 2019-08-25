namespace NineJoke.Services
{
    using System.IO;

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using NineJoke.Common;

    public class ImageService : IImageService
    {
        public async Task<string> UploadImage(IFormFile formImage, string template, string title, string postId)
        {
            string urlName = title.Replace(" ", string.Empty).Substring(0, 5) + postId.Substring(0, 8);

            var imagePath = string.Format(template, urlName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await formImage.CopyToAsync(stream);
            }

            var imageRoot = imagePath.Replace(GlobalConstants.WWWROOT, string.Empty);

            return imageRoot;
        }
    }
}
