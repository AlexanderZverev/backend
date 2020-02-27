using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Data.Abstract;
using Data.Models;

namespace Data
{
    public class ContactRepository : Repository, IContactRepository
    {
        public ContactRepository(string connectionString) : base(connectionString)
        {
        }

        public ContactDataModel GetUserContact(int userId, int contactId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                return connection.QueryFirstOrDefault<ContactDataModel>(
                    @"EXEC dbo.Contact_GetUserContacts @UserId = @UserId, @ContactId = @ContactId",
                    new {UserId = userId, ContactId = contactId});
            }
        }

        public IEnumerable<ContactDataModel> GetUserContacts(int userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                return connection.Query<ContactDataModel>(
                    @"EXEC dbo.Contact_GetUserContacts @UserId = @UserId",
                    new {UserId = userId});
            }
        }

        public ContactDataModel SearchUserContacts(int userId, string contactName)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                return connection.QueryFirstOrDefault<ContactDataModel>(
                    @"EXEC dbo.Contact_SearchUserContacts @UserId = @UserId, @ContactName = @Query",
                    new {UserId = userId, Query = contactName});
            }
        }

        public bool AddContact(ContactDataModel newContact)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var rowsAffected = connection.Execute(
                    @"EXEC dbo.Contact_Add @UserId = @UserId, @ContactId = @ContactId, @LastUpdateTime = @LastUpdateTime",
                    newContact);

                return rowsAffected > 0;
            }
        }

        public bool UpdateContact(ContactDataModel updatedContact)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var rowAffected = connection.Execute(
                    @"EXEC dbo.Contact_Update @UserId = @UserId, @ContactId = @ContactId, @LastUpdateTime = @LastUpdateTime",
                    updatedContact);

                return rowAffected > 0;
            }
        }

        public bool DeleteContact(int userId, int contactId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var rowAffected = connection.Execute(
                    @"EXEC dbo.Contact_Delete @UserId = @UserId, @ContactId = @ContactId",
                    new {UserId = userId, ContactId = contactId});

                return rowAffected > 0;
            }
        }
    }
}