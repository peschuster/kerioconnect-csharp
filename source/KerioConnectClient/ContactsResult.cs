using System.Collections.Generic;
using KerioConnect.Entities;

namespace KerioConnect
{
    public class ContactsResult
    {
        public List<Contact> list { get; set; }

        public int totalItems { get; set; }
    }
}
