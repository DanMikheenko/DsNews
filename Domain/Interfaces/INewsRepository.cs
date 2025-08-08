using Domain.Entities;

namespace Domain.Interfaces
{
    public interface INewsRepository
    {
        Task<IEnumerable<Article>> GetLatestNewsAsync(string query, int page = 1);
    }
}
