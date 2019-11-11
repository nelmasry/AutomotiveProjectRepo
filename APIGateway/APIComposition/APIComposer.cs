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
        public Task<DownstreamResponse> Aggregate(List<DownstreamContext> responses)
        {
            var vehiclesContent = responses.FirstOrDefault(r => r.DownstreamReRoute.Key.Equals("Vehicles")).DownstreamResponse.Content.ReadAsStringAsync();

            var customersContent = responses.FirstOrDefault(r => r.DownstreamReRoute.Key.Equals("Customers")).DownstreamResponse.Content.ReadAsStringAsync();

            List<ComposerModel> composerModel = JsonConvert.DeserializeObject<List<ComposerModel>>(customersContent.Result);
            List<Vehicle> vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(vehiclesContent.Result);
            foreach(ComposerModel model in composerModel)
            {
                model.Vehicles = vehicles.Where(v => v.CustomerId == model.Id).ToList();
            }
            var contentBuilder = new StringBuilder();
            contentBuilder.Append(vehiclesContent.Result);
            contentBuilder.Append(customersContent.Result);

            var stringContent = new StringContent(JsonConvert.SerializeObject(composerModel))
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };

            DownstreamResponse response =
                new DownstreamResponse(stringContent, HttpStatusCode.OK,
                new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
            return Task.FromResult(response);
        }
    }
}
