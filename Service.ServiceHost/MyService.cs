using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Topshelf;

namespace Service.ServiceHost
{
    public class MyService : ServiceControl
    {
        public IConfiguration Configuration { get; }
        public ServiceProvider ServiceProvider { get; private set; }

        public MyService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<INotifier, Notifier>();
        }

        public bool Start(HostControl hostControl)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            using (var scope = ServiceProvider.CreateScope())
            {
                var notifier = ServiceProvider.GetRequiredService<INotifier>();
                notifier.PrintMessage();
            }

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ServiceProvider.Dispose();

            return true;
        }
    }
}
