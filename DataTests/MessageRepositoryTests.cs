using System;
using Data;
using Data.Models;
using NUnit.Framework;
using System.Linq;

namespace DataTests
{
    class MessageRepositoryTests : BaseTests
    {
        [Test]
        public void GetUserMessages()
        {
            var repo = new MessageRepository(ConnectionString);
            var expectedMessages = new[]
            {
                new MessageDataModel
                {
                    Id = 1,
                    UserId = 1,
                    ContactId = 2,
                    SendTime = DateTime.Parse("2019-05-18 16:18"),
                    DeliveryTime = DateTime.Parse("2019-05-18 16:19"),
                    Content = "Hi Tom!"
                },
                new MessageDataModel
                {
                    Id = 3,
                    UserId = 1,
                    ContactId = 3,
                    SendTime = DateTime.Parse("2019-05-18 16:19"),
                    DeliveryTime = DateTime.Parse("2019-05-18 16:25"),
                    Content = "Hi Mark!"
                }
            };

            var messages = repo.GetUserMessages(1).ToArray();

            Assert.AreEqual(messages.Length, 2);
            Assert.True(Enumerable.Range(0, 2).All
                (
                    i => messages[i].Id == expectedMessages[i].Id
                         && messages[i].ContactId == expectedMessages[i].ContactId
                         && messages[i].UserId == expectedMessages[i].UserId
                         && messages[i].SendTime == expectedMessages[i].SendTime
                         && messages[i].DeliveryTime == expectedMessages[i].DeliveryTime
                         && messages[i].Content == expectedMessages[i].Content)
            );
        }

        [Test]
        public void SearchUserMessages()
        {
            var repo = new MessageRepository(ConnectionString);
            var expectedMessages = new[]
            {
                new MessageDataModel
                {
                    Id = 3,
                    UserId = 1,
                    ContactId = 3,
                    SendTime = DateTime.Parse("2019-05-18 16:19"),
                    DeliveryTime = DateTime.Parse("2019-05-18 16:25"),
                    Content = "Hi Mark!"
                }
            };

            var messages = repo.SearchUserMessages(1, 3, "Hi").ToArray();

            Assert.AreEqual(messages.Length, 1);
            Assert.True(Enumerable.Range(0, 1).All
                (
                    i => messages[i].Id == expectedMessages[i].Id
                         && messages[i].ContactId == expectedMessages[i].ContactId
                         && messages[i].UserId == expectedMessages[i].UserId
                         && messages[i].SendTime == expectedMessages[i].SendTime
                         && messages[i].DeliveryTime == expectedMessages[i].DeliveryTime
                         && messages[i].Content == expectedMessages[i].Content)
            );
        }

        [Test]
        public void AddMessage()
        {
            var repo = new MessageRepository(ConnectionString);
            var newMessage = new MessageDataModel
            {
                UserId = 1,
                ContactId = 4,
                SendTime = DateTime.Parse("2019-05-18 16:19"),
                DeliveryTime = DateTime.Parse("2019-05-18 16:25"),
                Content = "Hello Tom2!"
            };

            var newMessageId = repo.AddMessage(newMessage);

            var message = repo.SearchUserMessages(1, 4, "Hello Tom2").First();
            Assert.True(
                message.Id == newMessageId
                && message.ContactId == newMessage.ContactId
                && message.UserId == newMessage.UserId
                && message.SendTime == newMessage.SendTime
                && message.DeliveryTime == newMessage.DeliveryTime
                && message.Content == newMessage.Content);
        }
    }
}