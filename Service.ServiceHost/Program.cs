using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;
using Topshelf;

namespace Service.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location))
                .AddJsonFile("appsettings.json")
                .Build();

            var host = HostFactory.New(config =>
            {
                config.Service(() => new MyService(configuration));
            });

            var exitCode = host.Run();

            Environment.ExitCode = (int)exitCode;
        }
    }
}
