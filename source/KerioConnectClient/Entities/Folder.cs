using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KerioConnect.Entities
{
    [DebuggerDisplay("{name}, {type}")]
    public class Folder
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string parentId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ownerName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string emailAddress { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public FolderType type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public FolderSubType subType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public FolderPlaceType placeType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public FolderAccessType access { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool isShared { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool isDelegated { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool canBeSubscribed { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int nestingLevel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int messageCount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int messageUnread { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int messageSize { get; set; }

        [JsonProperty("checked", NullValueHandling = NullValueHandling.Ignore)]
        public bool isChecked { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string color { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool published { get; set; }
    }
}
