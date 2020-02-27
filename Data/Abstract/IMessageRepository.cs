using System.Collections.Generic;
using Data.Models;

namespace Data.Abstract
{
    public interface IMessageRepository
    {
        IEnumerable<MessageDataModel> GetUserMessages(int userId);

        IEnumerable<MessageDataModel> SearchUserMessages(int userId, int contactId, string query);

        int AddMessage(MessageDataModel newMessage);
    }
}
