using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Net;

namespace KerioConnect.LdapSync
{
    internal class LdapConnector
    {
        private readonly LdapConnection ldapConnection;

        private readonly string searchBaseDn;

        private readonly int pageSize;

        public LdapConnector(string searchBaseDn, string hostName, int portNumber, AuthType authType, string connectionAccountName, string connectionAccountPassword, int pageSize)
        {
            var ldapDirectoryIdentifier = new LdapDirectoryIdentifier(hostName, portNumber, true, false);

            var networkCredential = new NetworkCredential(connectionAccountName, connectionAccountPassword);

            this.ldapConnection = new LdapConnection(ldapDirectoryIdentifier, networkCredential, authType)
            {
                AutoBind = true
            };

            this.ldapConnection.SessionOptions.ProtocolVersion = 3;
            this.ldapConnection.SessionOptions.SecureSocketLayer = true;
            this.ldapConnection.SessionOptions.VerifyServerCertificate = (connection, certificate) => true;

            this.searchBaseDn = searchBaseDn;
            this.pageSize = pageSize;
        }

        public IEnumerable<SearchResultEntryCollection> PagedSearch(string searchFilter, string[] attributesToLoad)
        {
            var searchRequest = new SearchRequest(this.searchBaseDn, searchFilter, SearchScope.Subtree, attributesToLoad);

            var pageResultRequestControl = new PageResultRequestControl(this.pageSize);
            searchRequest.Controls.Add(pageResultRequestControl);

            while (true)
            {
                var searchResponse = (SearchResponse)this.ldapConnection.SendRequest(searchRequest);

                if (searchResponse == null)
                    break;

                var pageResponse = (PageResultResponseControl)searchResponse.Controls[0];

                yield return searchResponse.Entries;

                if (pageResponse.Cookie.Length == 0)
                    break;

                pageResultRequestControl.Cookie = pageResponse.Cookie;
            }
        }
    }
}
