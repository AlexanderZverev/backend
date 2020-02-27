using System.Collections.Generic;
using Data.Models;

namespace Data.Abstract
{
    public interface IUserRepository
    {
        UserDataModel GetUserById(int userId);

        UserDataModel GetUserByName(string name);

        IEnumerable<UserDataModel> SearchUserByName(string query);

        int AddUser(UserDataModel newUser);

        bool UpdateUser(UserDataModel updatedUser);

        bool UpdateUserState(int userId, bool userState);
    }
}