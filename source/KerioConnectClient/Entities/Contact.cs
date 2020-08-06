using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KerioConnect.Entities
{
    [DebuggerDisplay("{commonName}")]
    public class Contact
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string folderId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int watermark { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ContactType type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string commonName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string firstName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string middleName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string surName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string titleBefore { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string titleAfter { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string nickName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<PhoneNumber> phoneNumbers { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<EmailAddress> emailAddresses { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<PostalAddress> postalAddresses { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<object> urls { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(JsonDateTimeConverterRfc2445))]
        public DateTime? birthDay { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string anniversary { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string companyName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string departmentName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string profession { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string managerName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string assistantName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string comment { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string IMAddress { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Photo photo { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<object> categories { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string certSourceId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool isGalContact { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> searchList { get; set; }
    }
}