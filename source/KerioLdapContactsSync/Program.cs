using System;
using System.Diagnostics;

namespace KerioConnect.LdapSync
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new EventLogTraceListener("KerioLdapSync"));

            SyncResult result = null;

            try
            {
                var kernel = new Kernel(new AppSettingsConfiguration());

                result = kernel.Run();
            }
            catch (Exception exception)
            {
                Trace.TraceError(exception.ToString());

                Console.WriteLine(exception);
                throw;
            }
            finally
            {
                Trace.TraceInformation(
                    "Kerio LDAP Sync: {0} updated, {1} created, {2} deleted, {3} photos synced.",
                    result == null ? 0 : result.Updated,
                    result == null ? 0 : result.Created,
                    result == null ? 0 : result.Deleted,
                    result == null ? 0 : result.SyncedPhotos);

                Trace.Flush();
            }
        }
    }
}
