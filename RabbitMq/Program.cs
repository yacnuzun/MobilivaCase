using Core.Utilities.MailService;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.WebSockets;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.HostName = "localhost";

using (IConnection connection = factory.CreateConnection())
using (IModel channel = connection.CreateModel())
{
    channel.QueueDeclare("mail", true, false, true);
    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
    channel.BasicConsume("mail", false, consumer);
    consumer.Received += (sender, args) =>
    {
        var body = args.Body.Span;
        Mail.SendMail(Encoding.UTF8.GetString(body));
    };
    Console.Read();
}

public static class Mail
{
    public static void SendMail(string jsonMessage)
    {
        var emailMessage = JsonConvert.DeserializeObject<EmailMessage>(jsonMessage);
        MailSenderManager.SendMail(emailMessage);
    }
}