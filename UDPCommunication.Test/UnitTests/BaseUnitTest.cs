using Microsoft.Extensions.DependencyInjection;
using UDPCommunication.Data.Interfaces;
using UDPCommunication.Data.Repository;
using UDPCommunication.Service.Interfaces;
using UDPCommunication.Service.Services;

namespace UDPCommunication.Test.UnitTests
{
    public class BaseUnitTest
    {
        public ServiceProvider serviceProvider;

        public BaseUnitTest()
        {
            LoadServices();
        }

        private void LoadServices()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IUDPLogRepository, UDPLogRepository>();
            serviceCollection.AddScoped<IUDPLogService, UDPLogService>();
            serviceCollection.AddScoped<ICryptoService, CryptoService>();
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

    }
}
