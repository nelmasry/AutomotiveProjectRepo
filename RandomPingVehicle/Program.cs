using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RandomPingVehicle
{
    class Program
    {
        private static Timer timer;
        //static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            // timer will start after 5 seconds to ping cars
            timer = new Timer(timer_Elapsed);
            timer.Change(5000, 5000);
            Console.ReadKey();
        }

        private static void timer_Elapsed(object o)
        {
            //Random random = new Random();
            //int vehicleId = random.Next(1, 7);
            //Random boolRandom = new Random();
            //int isPing = boolRandom.Next(0, 1);

            RunAsync().Wait();
        }
        static async Task RunAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                // Update port # in the following line.
                client.BaseAddress = new Uri("http://localhost:53824/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                Random random = new Random();
                int vehicleId = random.Next(1, 7);
                Random boolRandom = new Random();
                bool isPing = boolRandom.Next(100) <= 20;
                if (isPing)
                {
                    Console.WriteLine("Ping vehicle with id : " + vehicleId);
                    HttpResponseMessage response = await client.PutAsync($"api/vehicle/ping/{vehicleId}", null);
                    response.EnsureSuccessStatusCode();
                    Console.WriteLine("Ping succeeded at " + DateTime.Now);
                }
                //await PingVehicle(vehicleId);
            }
        }
        //static async Task<Uri> PingVehicle(int id)
        //{

        //    Console.WriteLine("Ping succeeded");

        //    // return URI of the created resource.
        //    //return response.Headers.Location;
        //}
    }
}
