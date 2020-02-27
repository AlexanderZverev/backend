using System.Collections.Generic;
using Data.Models;

namespace Data.Abstract
{
    interface IContactRepository
    {
        ContactDataModel GetUserContact(int userId, int contactId);

        IEnumerable<ContactDataModel> GetUserContacts(int userId);

        ContactDataModel SearchUserContacts(int userId, string query);

        bool AddContact(ContactDataModel newContact);

        bool UpdateContact(ContactDataModel updatedContact);

        bool DeleteContact(int userId, int contactId);
    }
}
