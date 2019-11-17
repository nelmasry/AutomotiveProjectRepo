using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VehicleAPI.InMemoryDB;
using Xunit;

namespace VehicleAPI.IntegrationTests
{
    public class VehicleIntegrationTest
    {
        private TestServer _server;
        public HttpClient Client { get; private set; }

        public VehicleIntegrationTest()
        {
            SetUpClient();
        }

        [Fact]
        public void Getvehicles_success()
        {
            Task<HttpResponseMessage> httpResponse = Client.GetAsync("api/vehicle/getvehicles");
            httpResponse.Result.EnsureSuccessStatusCode();

            
            var stringResponse = httpResponse.Result.Content.ReadAsStringAsync().Result;
            var vehicles = JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, httpResponse.Result.StatusCode);
            Assert.NotNull(vehicles);
            int count = vehicles.Count();
            Assert.Equal(7, count);
        }
        [Fact]
        public void Getcutomervehicles_success()
        {
            int fakeCustomerId = 3;
            Task<HttpResponseMessage> httpResponse = Client.GetAsync($"api/vehicle/getcustomervehicles/{fakeCustomerId}");
            httpResponse.Result.EnsureSuccessStatusCode();

            
            var stringResponse = httpResponse.Result.Content.ReadAsStringAsync().Result;
            var vehicles = JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, httpResponse.Result.StatusCode);
            Assert.NotNull(vehicles);
            Assert.Equal(fakeCustomerId, vehicles.FirstOrDefault().CustomerId);
        }
        [Fact]
        public void Getcutomervehicles_nonexisitngcustomer_emptyresult()
        {
            int fakeVehicleId = 40;
            Task<HttpResponseMessage> httpResponse = Client.GetAsync($"api/vehicle/getcustomervehicles/{fakeVehicleId}");
            httpResponse.Result.EnsureSuccessStatusCode();
            
            Assert.Equal(HttpStatusCode.OK, httpResponse.Result.StatusCode);
        }
        [Fact]
        public void GetOnlinceVehicles_initialstart_emptyresult()
        {
            int fakeStatus = 1;
            Task<HttpResponseMessage> httpResponse = Client.GetAsync($"api/Vehicle/getvehiclesbystatus/{fakeStatus}");
            httpResponse.Result.EnsureSuccessStatusCode();

            
            var stringResponse = httpResponse.Result.Content.ReadAsStringAsync().Result;
            var vehicles = JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, httpResponse.Result.StatusCode);
            Assert.NotNull(vehicles);
            int count = vehicles.Count();
            Assert.Equal(0, count);
        }
        [Fact]
        public void GetOfflineVehicles_initialstart_getall()
        {
            int fakeStatus = 0;
            Task<HttpResponseMessage> httpResponse = Client.GetAsync($"api/Vehicle/getvehiclesbystatus/{fakeStatus}");
            httpResponse.Result.EnsureSuccessStatusCode();

            
            var stringResponse = httpResponse.Result.Content.ReadAsStringAsync().Result;
            var vehicles = JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, httpResponse.Result.StatusCode);
            Assert.NotNull(vehicles);
            int count = vehicles.Count();
            Assert.Equal(7, count);
        }
        [Fact]
        public void Pingvehicle_exisitingvehicle_nocontentresult()
        {
            int fakeVehicleId = 3;
            Task<HttpResponseMessage> httpResponse = Client.PutAsync($"/api/Vehicle/ping/{fakeVehicleId}",null);
            httpResponse.Result.EnsureSuccessStatusCode();

            
            var stringResponse = httpResponse.Result.Content.ReadAsStringAsync().Result;
            Assert.Equal(HttpStatusCode.NoContent, httpResponse.Result.StatusCode);


            // get vehicle we just ping to check last ping date 
            httpResponse = Client.GetAsync("api/vehicle/getvehicles");
            httpResponse.Result.EnsureSuccessStatusCode();
            
            stringResponse = httpResponse.Result.Content.ReadAsStringAsync().Result;
            var vehicles = JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(stringResponse);
            var veh = vehicles.Where(v => v.Id == fakeVehicleId).FirstOrDefault();
            Assert.True(veh.LastPingDate > DateTime.Now.AddMinutes(-1));
        }
        [Fact]
        public void Pingvehicle_nonexisitngvehicle_emptyresult()
        {
            int fakeVehicleId = 40;
            Task<HttpResponseMessage> httpResponse = Client.PutAsync($"/api/Vehicle/ping/{fakeVehicleId}",null);
            httpResponse.Result.EnsureSuccessStatusCode();

            
            var stringResponse = httpResponse.Result.Content.ReadAsStringAsync().Result;

            Assert.Equal(HttpStatusCode.OK, httpResponse.Result.StatusCode);
            Assert.Equal(stringResponse, string.Empty);
        }


        // helper functions
        private void SetUpClient()
        {
            _server = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>());

            Client = _server.CreateClient();
        }
    }
}
