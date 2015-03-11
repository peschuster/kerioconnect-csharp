using System;
using System.DirectoryServices.Protocols;
using System.Linq;

namespace KerioConnect.LdapSync
{
    internal class LdapContact
    {
        public string DisplayName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NamePraefix { get; set; }

        public string Mail { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Locality { get; set; }

        public string PostalCode { get; set; }

        public string Street { get; set; }

        public string TelephoneNumber { get; set; }

        public string FaxNumber { get; set; }

        public byte[] Photo { get; set; }

        public static LdapContact Parse(SearchResultEntry entry)
        {
            if (!entry.Attributes.Contains("displayName"))
                return null;

            var result = new LdapContact();

            string name = (string)entry.Attributes["displayName"][0];
            result.DisplayName = name;
            string[] splittedName = name.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();

            result.LastName = splittedName[0].TryTim();
            result.FirstName = (splittedName.Length > 1 ? splittedName[1] : string.Empty).TryTim();
            result.NamePraefix = string.Empty;

            string[] validPraefixes = { "dr", "dipl", "ing", "med", "prof" };

            if (splittedName.Length > 2)
            {
                foreach (string element in splittedName.Skip(2))
                {
                    if (element.Split(' ').Any(s => validPraefixes.Any(p => s.IndexOf(p, StringComparison.OrdinalIgnoreCase) > -1)))
                    {
                        result.NamePraefix += " " + element;
                    }
                    else
                    {
                        result.LastName = element + " " + result.LastName;
                    }
                }
            }

            result.NamePraefix = result.NamePraefix.Trim();

            if (entry.Attributes.Contains("jpegPhoto"))
            {
                result.Photo = (byte[])entry.Attributes["jpegPhoto"][0];
            }

            if (entry.Attributes.Contains("mail"))
            {
                result.Mail = (string)entry.Attributes["mail"][0];
                result.Mail = result.Mail.TryTim();
            }

            if (entry.Attributes.Contains("dateOfBirth"))
            {
                string dateOfBirth = (string)entry.Attributes["dateOfBirth"][0];
                result.DateOfBirth = new DateTime(Convert.ToInt32(dateOfBirth.Substring(0, 4)), Convert.ToInt32(dateOfBirth.Substring(4, 2)), Convert.ToInt32(dateOfBirth.Substring(6, 2)), 0, 0, 0);
            }

            if (entry.Attributes.Contains("telephoneNumber"))
            {
                result.TelephoneNumber = (string)entry.Attributes["telephoneNumber"][0];
                result.TelephoneNumber = result.TelephoneNumber.TryTim();
            }

            if (entry.Attributes.Contains("facsimilieTelephoneNumber"))
            {
                result.FaxNumber = (string)entry.Attributes["facsimilieTelephoneNumber"][0];
                result.FaxNumber = result.FaxNumber.TryTim();
            }

            if (entry.Attributes.Contains("l"))
            {
                result.Locality = (string)entry.Attributes["l"][0];
                result.Locality = result.Locality.TryTim();
            }

            if (entry.Attributes.Contains("postalCode"))
            {
                result.PostalCode = (string)entry.Attributes["postalCode"][0];
                result.PostalCode = result.PostalCode.TryTim();
            }

            if (entry.Attributes.Contains("street"))
            {
                result.Street = (string)entry.Attributes["street"][0];
                result.Street = result.Street.TryTim();
            }

            return result;
        }
    }
}