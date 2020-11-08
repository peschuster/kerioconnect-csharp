using Newtonsoft.Json;
using System.Diagnostics;

namespace KerioConnect
{
	[DebuggerDisplay("{id} - {modification}")]
	public class CalendarEventOccurrence
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string id { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string modification { get; set; } = "modifyThis";
	}
}