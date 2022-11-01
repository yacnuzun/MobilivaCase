using Core.Utilities.MailService;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Utilities.RabitMQ
{
    public static class Rabbit
    {
        public static void SendQue(EmailMessage emailMessage)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";

            var jsonFormat=JsonConvert.SerializeObject(emailMessage);
            
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare("mail", true, false, true);
                byte[] byteMessage = Encoding.UTF8.GetBytes(jsonFormat);
                channel.BasicPublish(exchange: "", routingKey: "mail", body: byteMessage);
            }
        }
    }
}
