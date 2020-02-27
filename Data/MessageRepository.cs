using System.Collections.Generic;
using Data.Models;
using Data.Abstract;
using System.Data.SqlClient;
using Dapper;

namespace Data
{
    public class MessageRepository : Repository, IMessageRepository
    {
        public MessageRepository(string connectionString) : base(connectionString)
        {
        }

        public IEnumerable<MessageDataModel> GetUserMessages(int userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                return connection.Query<MessageDataModel>(
                    @"EXEC dbo.Message_Get_ByUserId @UserId = @UserId",
                    new {UserId = userId});
            }
        }

        public IEnumerable<MessageDataModel> SearchUserMessages(int userId, int contactId, string query)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                return connection.Query<MessageDataModel>(
                    @"EXEC dbo.Message_Search @UserId = @UserId, @ContactId = @ContactId, @QueryString = @QueryString",
                    new {UserId = userId, ContactId = contactId, QueryString = query});
            }
        }

        public int AddMessage(MessageDataModel newMessage)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                return connection.QueryFirst<int>(
                    @"EXEC dbo.Message_Add @UserId = @UserId, @ContactId = @ContactId, @SendTime = @SendTime, @DeliveryTime = @DeliveryTime, @Content = @Content",
                    newMessage);
            }
        }
    }
}