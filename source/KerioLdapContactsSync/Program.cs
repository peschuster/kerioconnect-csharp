using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Text;

namespace KerioConnect.LdapSync
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new Kernel(new AppSettingsConfiguration());

            try
            {
                kernel.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}
