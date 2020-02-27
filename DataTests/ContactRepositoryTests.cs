using System;
using System.Linq;
using NUnit.Framework;
using Data;
using Data.Models;

namespace DataTests
{
    class ContactRepositoryTests : BaseTests
    {
        [Test]
        public void GetUserContact()
        {
            var repo = new ContactRepository(ConnectionString);
            var expectedContact = new ContactDataModel
            {
                UserId = 1,
                ContactId = 2,
                LastUpdateTime = DateTime.Parse("2019-05-18 16:20")
            };

            var contact = repo.GetUserContact(1, 2);

            Assert.True(
                expectedContact.ContactId == contact.ContactId
                && expectedContact.UserId == contact.UserId
                && expectedContact.LastUpdateTime == contact.LastUpdateTime
            );
        }

        [Test]
        public void GetUserContacts()
        {
            var repo = new ContactRepository(ConnectionString);
            var expectedContacts = new[]
            {
                new ContactDataModel
                {
                    UserId = 1,
                    ContactId = 2,
                    LastUpdateTime = DateTime.Parse("2019-05-18 16:20")
                },
                new ContactDataModel
                {
                    UserId = 1,
                    ContactId = 3,
                    LastUpdateTime = DateTime.Parse("2019-05-18 16:25")
                },
            };

            var contacts = repo.GetUserContacts(1).ToArray();

            Assert.True(
                contacts.Length == 2
                && contacts[0].UserId == expectedContacts[0].UserId
                && contacts[0].ContactId == expectedContacts[0].ContactId
                && contacts[0].LastUpdateTime == expectedContacts[0].LastUpdateTime
                && contacts[1].UserId == expectedContacts[1].UserId
                && contacts[1].ContactId == expectedContacts[1].ContactId
                && contacts[1].LastUpdateTime == expectedContacts[1].LastUpdateTime
            );
        }

        [Test]
        public void SeacrchUserContacts()
        {
            var repo = new ContactRepository(ConnectionString);
            var expectedContact = new ContactDataModel
            {
                UserId = 1,
                ContactId = 3,
                LastUpdateTime = DateTime.Parse("2019-05-18 16:25")
            };

            var contact = repo.SearchUserContacts(1, "Mark");

            Assert.True(
                contact.UserId == expectedContact.UserId
                && contact.ContactId == expectedContact.ContactId
                && contact.LastUpdateTime == expectedContact.LastUpdateTime);
        }

        [Test]
        public void AddContact()
        {
            var repo = new ContactRepository(ConnectionString);
            var newContact = new ContactDataModel
            {
                UserId = 4,
                ContactId = 1,
                LastUpdateTime = DateTime.Parse("2020-05-18 16:25")
            };

            repo.AddContact(newContact);

            var contact = repo.GetUserContact(4, 1);
            Assert.True(
                contact.UserId == newContact.UserId
                && contact.ContactId == newContact.ContactId
                && contact.LastUpdateTime == newContact.LastUpdateTime);
        }

        [Test]
        public void UpdateContact()
        {
            var repo = new ContactRepository(ConnectionString);
            var updatedContact = new ContactDataModel
            {
                UserId = 1,
                ContactId = 3,
                LastUpdateTime = DateTime.Parse("2019-05-18 16:25")
            };

            repo.UpdateContact(updatedContact);

            var contact = repo.GetUserContact(1, 3);
            Assert.True(contact.LastUpdateTime == updatedContact.LastUpdateTime);
        }

        [Test]
        public void DeleteContact()
        {
            var repo = new ContactRepository(ConnectionString);

            repo.DeleteContact(1, 2);

            var contact = repo.GetUserContact(1, 2);
            Assert.Null(contact);
        }
    }
}