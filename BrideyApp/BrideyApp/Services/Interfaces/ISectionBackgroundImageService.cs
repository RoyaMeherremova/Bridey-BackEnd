using BrideyApp.Models;

namespace BrideyApp.Services.Interfaces
{
    public interface ISectionBackgroundImageService
    {
        Task<List<SectionBackgroundImage>> GetSectionBackgroundImageDatasAsync();

        Task<SectionBackgroundImage> GetSectionBackgroundImageByIdAsync(int? id);
    }
}
