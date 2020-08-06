using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics;

namespace KerioConnect.Entities
{
	[DebuggerDisplay("{displayName} - {emailAddress} - {role}")]
	public class Attendee
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string displayName { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string emailAddress { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public bool isNotified { get; set; } = false;

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Role role { get; set; } = Role.RoleOrganizer;

		public enum Role
		{
			RoleOrganizer,
			RoleRequiredAttendee,
			RoleOptionalAttendee,
			RoleRoom,
			RoleEquipment
		}
	}
}