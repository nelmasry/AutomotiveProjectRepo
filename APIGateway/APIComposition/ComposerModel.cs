using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.APIComposition
{
    public class ComposerModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("vehicles")]
        public List<Vehicle> Vehicles { get; set; } 
    }

    public class Vehicle
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("customerid")]
        public int CustomerId { get; set; }
        [JsonProperty("vehicleid")]
        public string VehicleId { get; set; }

        [JsonProperty("registrationNumber")]
        public string RegistrationNumber { get; set; }

        [JsonProperty("lastpingdate")]
        public DateTime LastPingDate { get; set; }
        [JsonProperty("isonline")]
        public bool IsOnline { get; set; }
    }
    public class Customer
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
