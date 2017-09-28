using Domain;
using Domain.Entities;
using Domain.Interfaces;
using Domain.TelegramEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Bson;

namespace DomainTests
{
    [TestClass]
    public class DomainTests
    {
        [TestMethod]
        public void GetCommandFromUpdateWhenCommandInMesssage()
        {
            IUpdateReader reader = new UpdateReader();
            var update = new Update();
            update.message = new Message();
            update.message.text = "/test";
            var command = reader.GetActionData(update);
            Assert.IsTrue(command.Equals("/test"));
        }

        [TestMethod]
        public void GetCommandFromUpdateWhenCommandInCallBackData()
        {
            IUpdateReader reader = new UpdateReader();
            var update = new Update();
            update.callback_query = new CallbackQuery();
            update.callback_query.data = "/test";
            var command = reader.GetActionData(update);
            Assert.IsTrue(command.Equals("/test"));
        }

        [TestMethod]
        public void GetUserIdFromUpdateWhenMessageIsNotNull()
        {
            IUpdateReader reader = new UpdateReader();
            var update = new Update();
            update.message = new Message();
            update.message.from = new User();
            update.message.from.id = "777";
            var id = reader.GetUserId(update);
            Assert.AreEqual("777", id);
        }

        [TestMethod]
        public void GetUserIdFromUpdateWhenMessageIsNull()
        {
            IUpdateReader reader = new UpdateReader();
            var update = new Update();
            update.callback_query = new CallbackQuery();
            update.callback_query.from = new User();
            update.callback_query.from.id = "777";
            var id = reader.GetUserId(update);
            Assert.AreEqual(id,"777");
        }

        [TestMethod]
        public void CheckUpdateForInlineQuery_ReturnsTrue()
        {
            IUpdateReader reader = new UpdateReader();
            var update = new Update();
            update.inline_query = new InlineQuery();
            Assert.IsFalse(reader.IsInlineQuery(update));
        }

        [TestMethod]
        public void CheckUpdateForInlineQuery_ReturnsFalse()
        {
            IUpdateReader reader = new UpdateReader();
            var update = new Update();
            Assert.IsTrue(reader.IsInlineQuery(update));
        }

        [TestMethod]
        public void GetCommandFromUpdate()
        {
            IUpdateReader reader = new UpdateReader();
            var update = new Update();
            update.message = new Message();
            update.message.text = "/join/MM-15-2";
            var command = reader.GetCommand(update);
            Assert.AreEqual("/join",command);
        }

        [TestMethod]
        public void GetArgumentFromUpdate()
        {
            IUpdateReader reader = new UpdateReader();
            var update = new Update();
            update.message = new Message();
            update.message.text = "/join/MM-15-2";
            var groupName = reader.GetArgument(update);
            Assert.AreEqual("MM-15-2",groupName);
        }
    }
}
