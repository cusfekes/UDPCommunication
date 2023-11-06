using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUDPLogRepository, UDPLogRepository>();
            services.AddScoped<IUDPLogService, UDPLogService>();
        }
    }
}
