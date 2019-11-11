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
        private static Timer timer;
        private static IConfigurationRoot configuration;
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();

            Console.WriteLine("Vehicle ping Service started");
            // timer will start after 5 seconds to ping cars
            timer = new Timer(timer_Elapsed);
            timer.Change(5000, 5000);
            Console.ReadLine();
        }

        private static void timer_Elapsed(object o)
        {
            RunAsync().Wait();
        }
        static async Task RunAsync()
        {
            PingServiceHandler.RandomPingVehicles(configuration["APIGawtewayURL"]);
        }
    }
}
