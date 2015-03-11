using System.Configuration;

namespace KerioConnect.LdapSync
{
    class AppSettingsConfiguration : IConfiguration
    {
        public string LdapServer
        {
            get { return ConfigurationManager.AppSettings["ldapServer"]; }
        }

        public int LdapPort
        {
            get
            {
                int value;
                return int.TryParse(ConfigurationManager.AppSettings["ldapPort"], out value) ? value : 389;
            }
        }

        public string LdapUser
        {
            get { return ConfigurationManager.AppSettings["ldapUser"]; }
        }

        public string LdapPassword
        {
            get { return ConfigurationManager.AppSettings["ldapPassword"]; }
        }

        public string LdapBaseDn
        {
            get { return ConfigurationManager.AppSettings["ldapBaseDn"]; }
        }

        public string LdapFilter
        {
            get { return ConfigurationManager.AppSettings["ldapFilter"]; }
        }

        public bool SyncPhotos
        {
            get
            {
                bool value;
                return bool.TryParse(ConfigurationManager.AppSettings["syncPhotos"], out value) && value;
            }
        }

        public string KerioServer
        {
            get { return ConfigurationManager.AppSettings["kerioServer"]; }
        }

        public string KerioUser
        {
            get { return ConfigurationManager.AppSettings["kerioUser"]; }
        }

        public string KerioPassword
        {
            get { return ConfigurationManager.AppSettings["kerioPassword"]; }
        }

        public string KerioFolderName
        {
            get { return ConfigurationManager.AppSettings["kerioFolderName"]; }
        }
    }
}
