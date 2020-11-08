using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace KerioConnect.Entities
{
	[DebuggerDisplay("{summary} - {start}")]
	public class CalendarEvent
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string access { get; set; } = "EAccessCreator";

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<Attendee> attendees;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string description { get; set; }

		[JsonConverter(typeof(CalendarEventDateTimeConverter))]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public DateTime end { get; set; } //20200806T181500-0700

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string folderId { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public FreeBusy freeBusy = FreeBusy.Busy;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public bool isAllDay { get; set; } = false;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public bool isCancelled { get; set; } = false;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public bool isPrivate { get; set; } = false;

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Label label { get; set; } = Label.None;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string location { get; set; } = "";

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public Priority priority { get; set; } = Priority.Normal;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public EventReminder reminder { get; set; } = new EventReminder();

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public EventRule rule { get; set; } = new EventRule();

		[JsonConverter(typeof(CalendarEventDateTimeConverter))]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public DateTime start { get; set; } //20200806T180000-0700

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string summary { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int travelMinutes { get; set; } = 0;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int watermark { get; set; }

		public enum Label
		{
			None,
			Important,
			Business,
			Personal,
			Vacation,
			MustAttend,
			TravelRequired,
			NeedsPreparation,
			BirthDay,
			Anniversary,
			PhoneCall
		}

		public enum Priority
		{
			Normal,
			Low,
			High
		}

		public enum FreeBusy
		{
			Busy,
			Tentative,
			Free,
			OutOfOffice,
			NotAvailable
		}

		private class CalendarEventDateTimeConverter : IsoDateTimeConverter
		{
			public CalendarEventDateTimeConverter()
			{
				base.DateTimeFormat = @"yyyyMMdd""T""HHmmss-zz""00""";
			}
		}
	}
}