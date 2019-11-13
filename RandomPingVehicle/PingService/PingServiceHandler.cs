using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RandomPingVehicle.PingService
{
    public static class PingServiceHandler
    {
        public static async void RandomPingVehicles(string apiGatwayUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiGatwayUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    Random random = new Random();
                    int vehicleId = random.Next(1, 7); // get random vehicle id from 1 to 7 (Ids predefined in vehicle service)
                    Random boolRandom = new Random();
                    bool isPing = boolRandom.Next(100) <= 50; // run ping with percentage 20% 
                    if (isPing)
                    {
                        Console.WriteLine("Ping vehicle with id : " + vehicleId);
                        HttpResponseMessage response = await client.PutAsync($"api/vehicle/ping/{vehicleId}", null);
                        response.EnsureSuccessStatusCode();
                        Console.WriteLine("Ping succeeded at " + DateTime.Now);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ping failed at " + DateTime.Now);
                Console.WriteLine(ex.Message);
            }
        }

    }
}
