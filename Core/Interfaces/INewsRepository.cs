using Core.Entities;

namespace Core.Interfaces
{
    public interface INewsRepository
    {
         Task<IEnumerable<NewsArticle>> GetAllNewsAsync();
        Task<NewsArticle> GetNewsByIdAsync(int id);
        Task AddNewsAsync(NewsArticle newsArticle);
        Task UpdateNewsAsync(NewsArticle newsArticle);
        Task DeleteNewsAsync(int id);
    }
}