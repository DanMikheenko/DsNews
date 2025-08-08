using Domain.Entities;
using Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class NewsApiRepository : INewsRepository
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public NewsApiRepository(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "DsNewsApp");
        }

        public async Task<IEnumerable<Article>> GetLatestNewsAsync(string query, int page = 1)
        {
            var url = $"https://newsapi.org/v2/everything?q={query}&apiKey={_apiKey}&page={page}";
            var response = await _httpClient.GetStringAsync(url);
            var newsResult = JsonConvert.DeserializeObject<NewsApiResponse>(response);
            return newsResult.Articles.Select(a => new Article
            {
                Title = a.Title,
                Description = a.Description,
                Url = a.Url,
                ImageUrl = a.UrlToImage,
                Source = a.Source.Name,
                PublishedAt = a.PublishedAt
            });
        }
        private class NewsApiResponse
        {
            public List<ApiArticle> Articles { get; set; }
        }
        private class ApiArticle
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Url { get; set; }
            public string UrlToImage { get; set; }
            public ApiSource Source { get; set; }
            public DateTime PublishedAt { get; set; }
        }
        private class ApiSource
        {
            public string Name { get; set; }
        }
    }
}
