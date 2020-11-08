using System;
using System.Collections.Generic;
using JsonRPC;
using KerioConnect.Entities;
using Newtonsoft.Json.Linq;

namespace KerioConnect
{
    public class KerioConnectClient
    {
        private readonly KerioJsonClient client;

        public KerioConnectClient(string server)
        {
            var uri = new Uri(server, UriKind.Absolute);

            var builder = new UriBuilder(uri)
            {
                Path = "/webmail/api/jsonrpc/"
            };

            this.client = new KerioJsonClient(builder.ToString());
        }

        public bool Login(string username, string password)
        {
            var request = this.client.NewRequest("Session.login", JToken.FromObject(new
            {
                userName = username,
                password,
                application = new { name = "CsharpApiClient", vendor = "Peter Schuster", version = "1.0.0.0" }
            }));

            var response = this.client.Rpc(request);

            return true;
        }

        public void Logout()
        {
            var request = this.client.NewRequest("Session.logout");

            this.client.Rpc(request);
        }

        public string GetApiVersion()
        {
            Request request = this.client.NewRequest("Version.getApiVersion");

            GenericResponse response = this.client.Rpc(request);

            return response.Result.Value<string>("apiVersion");
        }

        public IList<Contact> GetContacts(string folderId)
        {
            var request = this.client.NewRequest("Contacts.get", JToken.FromObject(new
            {
                folderIds = new [] { folderId },
                query = new { start = 0, limit = 10000 }
            }));

            var response = this.client.Rpc<TypedResponse<ContactsResult>>(request);

            return response.Result.list;
        }

        public GenericResponse CreateContacts(IList<Contact> contacts)
        {
            var request = this.client.NewRequest("Contacts.create", JToken.FromObject(new
            {
                contacts = contacts
            }));

            return this.client.Rpc(request);
        }

        public GenericResponse SetContacts(IList<Contact> contacts)
        {
            var request = this.client.NewRequest("Contacts.set", JToken.FromObject(new
            {
                contacts = contacts
            }));

            return this.client.Rpc(request);
        }

        public GenericResponse RemoveContacts(string[] contactIds)
        {
            var request = this.client.NewRequest("Contacts.remove", JToken.FromObject(new
            {
                ids = contactIds
            }));

            return this.client.Rpc(request);
        }

        public GenericResponse UploadAttachment(string type, byte[] data)
        {
            return this.client.Upload("attachment-upload/", type, data);
        }

        public List<Folder> GetFolders()
        {
            var request = this.client.NewRequest("Folders.get");

            var response = this.client.Rpc<TypedResponse<FolderResult>>(request);

            return response == null ? null : response.Result.list;
        }

        public List<Folder> GetPublicFolders()
        {
            var request = this.client.NewRequest("Folders.getPublic");

            var response = this.client.Rpc<TypedResponse<FolderResult>>(request);

            return response == null ? null : response.Result.list;
        }

        public GenericResponse CreateCalendarEvent(CalendarEvent calendarEvent)
        {
            return CreateCalendarEvents(new List<CalendarEvent>() { calendarEvent });
        }

        public GenericResponse CreateCalendarEvents(IList<CalendarEvent> calendarEvents)
        {
            var request = this.client.NewRequest("Events.create", JToken.FromObject(new
            {
                events = calendarEvents
            }));

            return this.client.Rpc(request);
        }

        public GenericResponse RemoveCalendarEventOccurrence(string calendarEventId)
        {
            return RemoveCalendarEventOccurrences(new List<CalendarEventOccurrence>() { new CalendarEventOccurrence() { id = calendarEventId } });
        }

        public GenericResponse RemoveCalendarEventOccurrences(IList<CalendarEventOccurrence> calendarEventOccurrences)
        {
            var request = this.client.NewRequest("Occurrences.remove", JToken.FromObject(new
            {
                occurrences = calendarEventOccurrences
            }));

            return this.client.Rpc(request);
        }
    }
}
