namespace KerioConnect.LdapSync
{
    internal static class Extensions
    {
        public static string TryTim(this string value)
        {
            if (value == null)
                return null;

            return value.Trim();
        }
    }
}
