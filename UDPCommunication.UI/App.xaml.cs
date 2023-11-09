using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using UDPCommunication.Data.Interfaces;
using UDPCommunication.Data.Repository;
using UDPCommunication.Service.Interfaces;
using UDPCommunication.Service.Services;

namespace UDPCommunication.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder();
            Configuration = builder.Build();

            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            // Start application from main window
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Inject services to application by using dependency injection
            services.AddScoped<IUDPLogRepository, UDPLogRepository>();
            services.AddScoped<IUDPLogService, UDPLogService>();
            services.AddScoped<IUDPService, UDPService>();
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddTransient(typeof(MainWindow));
        }
    }
}
