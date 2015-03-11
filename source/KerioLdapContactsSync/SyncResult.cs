using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KerioConnect.LdapSync
{
    internal class SyncResult
    {
        public int Updated { get; set; }

        public int Created { get; set; }

        public int Deleted { get; set; }

        public int SyncedPhotos { get; set; }
    }
}
