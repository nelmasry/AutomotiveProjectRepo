using Microsoft.Extensions.Configuration;
using RandomPingVehicle.PingService;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace RandomPingVehicle
{
    class Program
    {
        private static IConfigurationRoot configuration;
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();

            // Console.WriteLine("Vehicle ping Service started");

            SimulatePingVehicles();
            Console.ReadLine();
        }
        static void SimulatePingVehicles()
        {
            PingServiceHandler.RandomPingVehicles(configuration["APIGawtewayURL"]);
        }
    }
}
