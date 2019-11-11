using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Middleware.Multiplexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.APIComposition
{
    public class APIComposer : IDefinedAggregator
    {

        /*
         * http://localhost:5000/getvehicleswithdetails
         * http://localhost:5000/getcustomervehicleswithdetails/2
         * http://localhost:5000/getvehiclesbystatuswithdetails/1 online
         * http://localhost:5000/getvehiclesbystatuswithdetails/0 offline
         */
        public Task<DownstreamResponse> Aggregate(List<DownstreamContext> responses)
        {
            try
            {
                Task<string> vehiclesContent;
                Task<string> customersContent;
                string vehiclesKey = responses.FirstOrDefault(r => r.DownstreamReRoute.Key.ToLower().Contains("vehicle")).DownstreamReRoute.Key;
                string customerKey = responses.FirstOrDefault(r => r.DownstreamReRoute.Key.ToLower().Contains("customer")).DownstreamReRoute.Key;
                vehiclesContent = responses.FirstOrDefault(r => r.DownstreamReRoute.Key.Equals(vehiclesKey)).DownstreamResponse.Content.ReadAsStringAsync();
                customersContent = responses.FirstOrDefault(r => r.DownstreamReRoute.Key.Equals(customerKey)).DownstreamResponse.Content.ReadAsStringAsync();

                List<ComposerModel> model = CreateComposerModel(vehiclesContent, customersContent);

                var stringContent = new StringContent(JsonConvert.SerializeObject(model))
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                };

                DownstreamResponse response =
                    new DownstreamResponse(stringContent, HttpStatusCode.OK,
                    new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
                return Task.FromResult(response);
            }
            catch(Exception ex)
            {
                DownstreamResponse response =
                    new DownstreamResponse(null, HttpStatusCode.BadRequest,
                    new List<KeyValuePair<string, IEnumerable<string>>>(), "BadRequest");
                return Task.FromResult(response);
            }
        }

        private List<ComposerModel> CreateComposerModel(Task<string> vehiclesContent, Task<string> customersContent)
        {
            List<ComposerModel> composerModel = new List<ComposerModel>();
            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(customersContent.Result);
            List<Vehicle> vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(vehiclesContent.Result);
            var customerIds = vehicles.Select(s => s.CustomerId).Distinct();
            customers = customers.Where(c => customerIds.Contains(c.Id)).ToList();
            foreach (Customer cust in customers)
            {
                ComposerModel model = new ComposerModel()
                {
                    Id = cust.Id,
                    Name = cust.Name,
                    Address = cust.Address,
                    Vehicles = vehicles.Where(v => v.CustomerId == cust.Id).ToList()
                };
                composerModel.Add(model);
            }
            return composerModel;
        }
    }
}
