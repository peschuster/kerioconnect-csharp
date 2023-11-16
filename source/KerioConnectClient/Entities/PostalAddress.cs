using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KerioConnect.Entities
{
    public class PostalAddress
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool preferred { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string pobox { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string extendedAddress { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string street { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string locality { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string state { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string zip { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string country { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string label { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public PostalAddressType type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ABExtension extension { get; set; }
    }
}