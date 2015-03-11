using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KerioConnect.Entities
{
    public class EmailAddress
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string address { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool preferred { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool hasCertificate { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public EmailAddressType type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string refId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ABExtension extension { get; set; }
    }
}