using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KerioConnect.Entities
{
    public class PhoneNumber
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public PhoneNumberType type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string number { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ABExtension extension { get; set; }
    }
}