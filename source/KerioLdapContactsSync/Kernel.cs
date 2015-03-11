using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using JsonRPC;
using KerioConnect.Entities;

namespace KerioConnect.LdapSync
{
    internal class Kernel
    {
        private readonly IConfiguration configuration;

        private readonly LdapConnector ldap;

        private readonly KerioConnectClient kerio;

        public Kernel(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            this.configuration = configuration;

            this.ldap = new LdapConnector(
                configuration.LdapBaseDn, 
                configuration.LdapServer, 
                configuration.LdapPort, 
                AuthType.Basic, 
                configuration.LdapUser, 
                configuration.LdapPassword, 
                100);

            this.kerio = new KerioConnectClient(configuration.KerioServer);
        }

        public SyncResult Run()
        {
            this.KerioLogin();

            Folder folder = this.GetFolder(this.configuration.KerioFolderName);

            if (folder == null)
                throw new InvalidOperationException("Folder " + this.configuration.KerioFolderName + " not found.");
            
            var contacts = this.kerio.GetContacts(folder.id);

            this.kerio.Logout();

            var ldapContacts = this.GetLdapContacts(this.configuration.LdapFilter, this.configuration.SyncPhotos); ;

            var newContacts = new List<Contact>();
            var updatedContacts = new List<Contact>();
            var unchangedContacts = new List<Contact>();

            foreach (var ldapContact in ldapContacts)
            {
                var kerioContact = contacts.FirstOrDefault(c => c.surName == ldapContact.LastName && c.firstName == ldapContact.FirstName && c.titleBefore == ldapContact.NamePraefix);

                bool changed = false;

                if (kerioContact == null)
                {
                    kerioContact = new Contact
                    {
                        folderId = folder.id,
                        type = ContactType.ctContact,
                        emailAddresses = new List<EmailAddress>(),
                        phoneNumbers = new List<PhoneNumber>(),
                        postalAddresses = new List<PostalAddress>()
                    };

                    changed = true;
                }

                string commonName = (ldapContact.FirstName + " " + ldapContact.LastName).Trim();

                if (commonName != kerioContact.commonName || kerioContact.surName != ldapContact.LastName ||
                    kerioContact.firstName != ldapContact.FirstName ||
                    kerioContact.titleBefore != ldapContact.NamePraefix)
                {
                    changed = true;
                }

                kerioContact.commonName = commonName;
                kerioContact.surName = ldapContact.LastName.TryTim();
                kerioContact.firstName = ldapContact.FirstName.TryTim();
                kerioContact.titleBefore = ldapContact.NamePraefix.TryTim();

                if (kerioContact.birthDay != ldapContact.DateOfBirth)
                {
                    changed = true;
                    kerioContact.birthDay = ldapContact.DateOfBirth;
                }

                EmailAddress homeMail = kerioContact.emailAddresses.FirstOrDefault(e => e.type == EmailAddressType.EmailHome);
                if (!string.IsNullOrWhiteSpace(ldapContact.Mail))
                {
                    if (homeMail == null)
                    {
                        changed = true;
                        homeMail = new EmailAddress { type = EmailAddressType.EmailHome };
                        kerioContact.emailAddresses.Add(homeMail);
                    }

                    if (homeMail.address != ldapContact.Mail)
                    {
                        changed = true;
                        homeMail.address = ldapContact.Mail.TryTim();
                    }
                }
                else if (homeMail != null)
                {
                    changed = true;
                    kerioContact.emailAddresses.Remove(homeMail);
                }

                PhoneNumber homePhone = kerioContact.phoneNumbers.FirstOrDefault(n => n.type == PhoneNumberType.TypeHomeVoice);
                if (!string.IsNullOrWhiteSpace(ldapContact.TelephoneNumber))
                {
                    if (homePhone == null)
                    {
                        changed = true;
                        homePhone = new PhoneNumber { type = PhoneNumberType.TypeHomeVoice };
                        kerioContact.phoneNumbers.Add(homePhone);
                    }

                    if (homePhone.number != ldapContact.TelephoneNumber)
                    {
                        changed = true;
                        homePhone.number = ldapContact.TelephoneNumber.TryTim();
                    }
                }
                else if (homePhone != null)
                {
                    changed = true;
                    kerioContact.phoneNumbers.Remove(homePhone);
                }

                PhoneNumber homeFax = kerioContact.phoneNumbers.FirstOrDefault(n => n.type == PhoneNumberType.TypeHomeFax);
                if (!string.IsNullOrWhiteSpace(ldapContact.FaxNumber))
                {
                    if (homeFax == null)
                    {
                        changed = true;
                        homeFax = new PhoneNumber { type = PhoneNumberType.TypeHomeFax };
                        kerioContact.phoneNumbers.Add(homeFax);
                    }

                    if (homeFax.number != ldapContact.FaxNumber)
                    {
                        changed = true;
                        homeFax.number = ldapContact.FaxNumber.TryTim();
                    }
                }
                else if (homeFax != null)
                {
                    changed = true;
                    kerioContact.phoneNumbers.Remove(homeFax);
                }

                PostalAddress homeAddress = kerioContact.postalAddresses.FirstOrDefault(n => n.type == PostalAddressType.AddressHome);
                if (homeAddress == null)
                {
                    homeAddress = new PostalAddress { type = PostalAddressType.AddressHome };
                    kerioContact.postalAddresses.Add(homeAddress);
                }

                if ((homeAddress.locality != ldapContact.Locality && !(string.IsNullOrEmpty(homeAddress.locality) && string.IsNullOrEmpty(ldapContact.Locality)))
                    || (homeAddress.zip != ldapContact.PostalCode && !(string.IsNullOrEmpty(homeAddress.zip) && string.IsNullOrEmpty(ldapContact.PostalCode)))
                    || (homeAddress.street != ldapContact.Street && !(string.IsNullOrEmpty(homeAddress.street) && string.IsNullOrEmpty(ldapContact.Street))))
                {
                    changed = true;

                    homeAddress.locality = ldapContact.Locality.TryTim();
                    homeAddress.zip = ldapContact.PostalCode.TryTim();
                    homeAddress.street = ldapContact.Street.TryTim();
                }

                if (!changed)
                {
                    unchangedContacts.Add(kerioContact);
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(kerioContact.id))
                {
                    updatedContacts.Add(kerioContact);
                }
                else
                {
                    newContacts.Add(kerioContact);
                }
            }

            var deletedContacts = new List<Contact>(
                contacts.Except(updatedContacts).Except(unchangedContacts));

            this.KerioLogin();

            GenericResponse response;
            var result = new SyncResult();

            if (updatedContacts.Any())
            {
                result.Updated = updatedContacts.Count;
                response = this.kerio.SetContacts(updatedContacts);
            }

            if (newContacts.Any())
            {
                result.Created = updatedContacts.Count;
                response = this.kerio.CreateContacts(newContacts);
            }

            if (deletedContacts.Any())
            {
                result.Deleted = updatedContacts.Count;
                response = this.kerio.RemoveContacts(deletedContacts.Select(c => c.id).ToArray());
            }

            result.SyncedPhotos = 0;

            if (this.configuration.SyncPhotos)
            {
                contacts = this.kerio.GetContacts(folder.id);
                updatedContacts.Clear();
                newContacts.Clear();


                foreach (var ldapContact in ldapContacts.Where(c => c.Photo != null))
                {
                    Contact kerioContact =
                        contacts.FirstOrDefault(
                            c => c.surName == ldapContact.LastName && c.firstName == ldapContact.FirstName);

                    if (kerioContact == null)
                        continue;

                    response = this.kerio.UploadAttachment("image/jpeg", ldapContact.Photo);

                    if (response.Error != null)
                        continue;

                    string uploadId = response.Result["fileUpload"].Value<string>("id");
                    string uploadName = response.Result["fileUpload"].Value<string>("name");

                    kerioContact.photo = new Photo
                    {
                        id = uploadId,
                        url = "/webmail/api/download/attachment/tmp/" + uploadId + "/" + uploadName
                    };

                    this.kerio.SetContacts(new List<Contact> { kerioContact });
                    result.SyncedPhotos++;
                }
            }
            
            this.kerio.Logout();

            return result;
        }

        private bool KerioLogin()
        {
            return this.kerio.Login(
                this.configuration.KerioUser, 
                this.configuration.KerioPassword);
        }

        private Folder GetFolder(string name)
        {
            var folders = this.kerio.GetFolders();

            return folders.SingleOrDefault(f => 
                f.access != FolderAccessType.FAccessListingOnly 
                && f.access != FolderAccessType.FAccessReadOnly 
                && f.type == FolderType.FContact 
                && f.placeType == FolderPlaceType.FPlacePublic
                && f.name == name);
        }

        private ICollection<LdapContact> GetLdapContacts(string filter, bool syncPhotos)
        {
            var search = this.ldap.PagedSearch(
                filter,
                syncPhotos
                    ? new[] { "dateOfBirth", "displayName", "l", "mail", "postalCode", "street", "telephoneNumber", "facsimilieTelephoneNumber", "jpegPhoto" }
                    : new[] { "dateOfBirth", "displayName", "l", "mail", "postalCode", "street", "telephoneNumber", "facsimilieTelephoneNumber" });

            var result = new List<LdapContact>();

            foreach (SearchResultEntryCollection collection in search)
            {
                result.AddRange(
                    collection.OfType<SearchResultEntry>()
                        .Where(e => e.Attributes.Contains("displayName"))
                        .Select(LdapContact.Parse));
            }

            return result;
        }
    }
}
