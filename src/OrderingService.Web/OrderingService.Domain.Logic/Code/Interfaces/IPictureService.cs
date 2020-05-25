using System.Threading.Tasks;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface IPictureService
    {
        Task DeleteImageAsync(string publicId);
        Task ChangeImageTagAsync(string publicId, string newTag);
        Task DeleteTemporaryImagesAsync();
    }
}
