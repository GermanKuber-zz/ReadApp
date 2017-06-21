using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Common.Models;

namespace Common.Admins
{
    public  class ContactAdmin
    {
        public  async Task<List<ContactWrapper>> GetAllContacts()
        {
            var contactStore =
                    await ContactManager.RequestStoreAsync(ContactStoreAccessType.AllContactsReadOnly);
                
            return (from contact in await contactStore.FindContactsAsync() select new ContactWrapper(contact)).ToList();
        }
    }
}
