using Newtonsoft.Json;
using System.Diagnostics;

namespace KerioConnect.Entities
{
	[DebuggerDisplay("isSet: {isSet}")]
	public class EventRule
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public bool isSet { get; set; }
	}
}