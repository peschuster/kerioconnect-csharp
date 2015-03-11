namespace KerioConnect.LdapSync
{
    public interface IConfiguration
    {
        string LdapServer { get; }

        int LdapPort { get; }

        string LdapUser { get; }

        string LdapPassword { get; }

        string LdapBaseDn { get; }

        string LdapFilter { get; }

        bool SyncPhotos { get; }

        string KerioServer { get; }

        string KerioUser { get; }

        string KerioPassword { get; }

        string KerioFolderName { get; }
    }
}
