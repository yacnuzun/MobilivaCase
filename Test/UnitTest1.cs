using Core.Utilities.MailService;
using Core.Utilities.RabitMQ;
using Newtonsoft.Json;
using NUnit.Framework;
using RabbitMQ.Client;
using System.Text;

namespace Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MailQue()
        {
            EmailMessage message = new EmailMessage()
            {
                Subject = "Test Mesajý",
                Body = "Rabbit Test",
                Contacts = "yacn.uzun@gmail.com"
            };


            Rabbit.SendQue(message);
            Assert.Pass();
        }
    }
}