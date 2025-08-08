using System.Configuration;
using System.Data;
using System.Windows;
using Application.UseCases;
using Infrastructure.Repositories;
using Presentation.ViewModels;

namespace DsNews
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var apiKey = ConfigurationManager.AppSettings["NewsApiKey"];
            var newsRepository = new NewsApiRepository(apiKey);
            var getLatestNewsUseCase = new GetLatestNewsUseCase(newsRepository);
            var mainViewModel = new MainViewModel(getLatestNewsUseCase);
            var mainWindow = new MainWindow(mainViewModel);

            mainWindow.Show();
        }
    }

}
