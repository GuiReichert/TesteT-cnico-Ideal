using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TesteTécnicoIdeal.WPF.DTO_s;
using TesteTécnicoIdeal.WPF.Service;

namespace TesteTécnicoIdeal.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        private const string API_URL = "https://testetécnicoidealapi20250401171514-dmedfsgpgzdrhqc5.brazilsouth-01.azurewebsites.net/";                             // Conexão base provisória com a API

        public App()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddHttpClient<ApiService>(client =>
            {
                client.BaseAddress = new Uri(API_URL);
            });

            services.AddSingleton<MainWindow>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }

}
