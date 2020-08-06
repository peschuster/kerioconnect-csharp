using Newtonsoft.Json;
using System.Diagnostics;

namespace KerioConnect.Entities
{
	[DebuggerDisplay("{minutesBeforeStart} - {isSet}")]
	public class EventReminder
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public bool isSet { get; set; } = true;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public long minutesBeforeStart { get; set; } = 15;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string type { get; set; } = "ReminderRelative";
	}
}