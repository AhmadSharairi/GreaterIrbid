using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class NewsRepository : INewsRepository
    {

        private readonly IrbidContext _context;

        public NewsRepository(IrbidContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NewsArticle>> GetAllNewsAsync()
        {
            return await _context.NewsArticles
                .Include(n => n.Images)
                .ToListAsync();
        }

        public async Task<NewsArticle> GetNewsByIdAsync(int id)
        {
            return await _context.NewsArticles
                .Include(n => n.Images)
                .FirstOrDefaultAsync(n => n.Id == id);
        }


        public async Task AddNewsAsync(NewsArticle newsArticle)
        {
            _context.NewsArticles.Add(newsArticle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNewsAsync(NewsArticle newsArticle)
        {
            _context.Entry(newsArticle).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNewsAsync(int id)
        {
            var newsArticle = await _context.NewsArticles.FindAsync(id);
            if (newsArticle != null)
            {
                _context.NewsArticles.Remove(newsArticle);
                await _context.SaveChangesAsync();
            }
        }
    }
}