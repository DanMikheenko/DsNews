using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetLatestNewsUseCase
    {
        private readonly INewsRepository _newsRepository;

        public GetLatestNewsUseCase(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<IEnumerable<Article>> ExecuteAsync(string query, int page = 1)
        {
            return await _newsRepository.GetLatestNewsAsync(query, page);
        }
    }
}
