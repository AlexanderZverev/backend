using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
using Data.Abstract;
using Data.Models;

namespace Data
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public UserDataModel GetUserById(int userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                return connection.QueryFirstOrDefault<UserDataModel>(
                    @"EXEC dbo.User_Get_ById @UserId = @UserId",
                    new {UserId = userId});
            }
        }

        public UserDataModel GetUserByName(string userName)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                return connection.QueryFirstOrDefault<UserDataModel>(
                    @"EXEC dbo.User_Get_ByName @UserName = @UserName",
                    new {UserName = userName});
            }
        }

        public IEnumerable<UserDataModel> SearchUserByName(string query)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                return connection.Query<UserDataModel>(
                    @"EXEC dbo.User_Search_ByName @UserName = @UserName",
                    new {UserName = query});
            }
        }

        public int AddUser(UserDataModel newUser)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                return connection.QueryFirst<int>(
                    @"EXEC dbo.User_Add @Name = @Name, @Password = @Password, @State = @State",
                    newUser);
            }
        }

        public bool UpdateUser(UserDataModel updatedUser)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var rowAffected = connection.Execute(
                    @"EXEC dbo.User_Update @UserId = @Id, @Name = @Name, @Password = @Password, @State = @State",
                    updatedUser);

                return rowAffected > 0;
            }
        }

        public bool UpdateUserState(int userId, bool userState)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var rowAffected = connection.Execute(
                    @"EXEC dbo.User_UpdateState @UserId = @UserId, @State = @State",
                    new
                    {
                        UserId = userId, State = userState
                    });

                return rowAffected > 0;
            }
        }
    }
}