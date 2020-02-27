using Data;
using Data.Models;
using NUnit.Framework;
using System.Linq;

namespace DataTests
{
    class UserRepositoryTests : BaseTests
    {
        [Test]
        public void GetUserById()
        {
            var repo = new UserRepository(ConnectionString);
            var expectedUser = new UserDataModel
            {
                Id = 1,
                Name = "Bob",
                Password = "TestPassword1",
                State = true
            };

            var user = repo.GetUserById(1);

            Assert.True(
                expectedUser.Id == user.Id
                && expectedUser.Name == user.Name
                && expectedUser.Password == user.Password
                && expectedUser.State == user.State
            );
        }

        [Test]
        public void GetUserByName()
        {
            var repo = new UserRepository(ConnectionString);
            var expectedUser = new UserDataModel
            {
                Id = 1,
                Name = "Bob",
                Password = "TestPassword1",
                State = true
            };

            var user = repo.GetUserByName("Bob");

            Assert.True(
                expectedUser.Id == user.Id
                && expectedUser.Name == user.Name
                && expectedUser.Password == user.Password
                && expectedUser.State == user.State
            );
        }

        [Test]
        public void SearchUserByName()
        {
            var repo = new UserRepository(ConnectionString);
            var expectedUsers = new[]
            {
                new UserDataModel
                {
                    Id = 2,
                    Name = "Tom1",
                    Password = "TestPassword2",
                    State = false
                },
                new UserDataModel
                {
                    Id = 4,
                    Name = "Tom2",
                    Password = "TestPassword4",
                    State = false
                }
            };
            var users = repo.SearchUserByName("Tom").ToArray();
            Assert.True(
                users.Length == 2
                && expectedUsers[0].Id == users[0].Id
                && expectedUsers[0].Name == users[0].Name
                && expectedUsers[0].Password == users[0].Password
                && expectedUsers[0].State == users[0].State
                && expectedUsers[1].Id == users[1].Id
                && expectedUsers[1].Name == users[1].Name
                && expectedUsers[1].Password == users[1].Password
                && expectedUsers[1].State == users[1].State
            );
        }

        [Test]
        public void AddUser()
        {
            var repo = new UserRepository(ConnectionString);
            var newUser = new UserDataModel
            {
                Name = "NewUser",
                Password = "NewPassword",
                State = true
            };

            var id = repo.AddUser(newUser);

            var user = repo.GetUserById(id);
            Assert.True(user.Name == newUser.Name
                        && user.Password == newUser.Password
                        && user.State == newUser.State);
        }

        [Test]
        public void UpdateUser()
        {
            var repo = new UserRepository(ConnectionString);
            var user = repo.GetUserById(1);
            user.Name = "updatedName";

            repo.UpdateUser(user);

            var updatedUser = repo.GetUserById(1);
            Assert.True(user.Name == updatedUser.Name
                        && user.Password == updatedUser.Password
                        && user.State == updatedUser.State);
        }

        [Test]
        public void UpdateUserState()
        {
            var repo = new UserRepository(ConnectionString);

            repo.UpdateUserState(1, false);

            var user = repo.GetUserById(1);
            Assert.False(user.State);
        }
    }
}