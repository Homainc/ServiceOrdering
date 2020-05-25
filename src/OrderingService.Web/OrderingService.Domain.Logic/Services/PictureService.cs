using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OrderingService.Domain.Logic.Code.Configs;
using OrderingService.Domain.Logic.Code.Constants;
using OrderingService.Domain.Logic.Code.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class PictureService : IPictureService
    {
        private readonly Cloudinary _cloudinary;
        private readonly CancellationToken _cancellationToken;

        public PictureService(IOptions<CloudinaryCredentials> options, IHttpContextAccessor httpContextAccessor)
        {
            // Configure cloudinary
            var credentials = options.Value;
            var account = new Account
            {
                ApiKey = credentials.ApiKey,
                ApiSecret = credentials.ApiSecret,
                Cloud = credentials.CloudName
            };
            _cloudinary = new Cloudinary(account);

            _cancellationToken = httpContextAccessor.HttpContext.RequestAborted;
        }

        public async Task DeleteImageAsync(string publicId) =>
            await _cloudinary.DeleteResourcesAsync(new DelResParams {PublicIds = new List<string>(new[] {publicId})},
                _cancellationToken);

        public async Task ChangeImageTagAsync(string publicId, string newTag)
        {
            await _cloudinary.TagAsync(new TagParams
            {
                Command = TagCommand.Replace,
                PublicIds = new List<string>(new[] {publicId}),
                ResourceType = ResourceType.Image,
                Tag = newTag
            }, _cancellationToken);
        }

        public async Task DeleteTemporaryImagesAsync() =>
            await _cloudinary.DeleteResourcesByTagAsync(CloudinaryTagDefaults.Temp, _cancellationToken);
    }
}
