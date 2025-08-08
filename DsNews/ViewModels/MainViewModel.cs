using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.UseCases;

namespace Presentation.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly GetLatestNewsUseCase _getLatestNewsUseCase;

        public ObservableCollection<Article> Articles { get; set; } = new();

        private string _query = "technology";
        public string Query
        {
            get => _query;
            set
            {
                _query = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(GetLatestNewsUseCase getLatestNewsUseCase)
        {
            _getLatestNewsUseCase = getLatestNewsUseCase;
        }

        public async Task LoadNewsAsync()
        {
            var news = await _getLatestNewsUseCase.ExecuteAsync(Query);
            Articles.Clear();
            foreach (var article in news)
            {
                Articles.Add(article);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
