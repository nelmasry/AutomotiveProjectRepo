using CustomerAPI.InMemoryDB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace CustomerAPI.IntegrationTests
{
    public class CustomerIntegrationTest
    {
        private TestServer _server;
        public HttpClient Client { get; private set; }

        public CustomerIntegrationTest()
        {
            SetUpClient();
        }

        [Fact]
        public async Task Getcustomers_all_success()
        {
            Task<HttpResponseMessage> httpResponse = Client.GetAsync("api/customer/getcustomers");
            //await httpResponse.EnsureSuccessStatusCode();

            
            var stringResponse = (await httpResponse).Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(await stringResponse);

            var res = await httpResponse;
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.NotNull(customers);
            int count = customers.Count();
            Assert.Equal(3, count);
        }

        [Fact]
        public async Task Getcustomer_existingcustomer_success()
        {
            int fakeCustomerId = 3;
            Task<HttpResponseMessage> httpResponse = Client.GetAsync($"api/customer/{fakeCustomerId}");
            //httpResponse.Result.EnsureSuccessStatusCode();

            
            var stringResponse = (await httpResponse).Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<IEnumerable<Customer>>(await stringResponse);

            var res = await httpResponse;
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.NotNull(customer);
            int count = customer.Count();
            Assert.Equal(1,count);
            Assert.Equal(fakeCustomerId, customer.FirstOrDefault().Id);
        }
        [Fact]
        public async Task Getcustomer_nonexistingcustomer_emptyresult()
        {
            int fakeCustomerId = 40;
            Task<HttpResponseMessage> httpResponse = Client.GetAsync($"api/customer/{fakeCustomerId}");
            (await httpResponse).EnsureSuccessStatusCode();

            
            var stringResponse = (await httpResponse).Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<IEnumerable<Customer>>(await stringResponse);

            var res = await httpResponse;
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.NotNull(customer);
            int count = customer.Count();
            Assert.Equal(0, count);
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
