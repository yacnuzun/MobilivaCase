using System.Net.Mail;
using System.Net;
using System.Text;

namespace Core.Utilities.MailService
{
    public static class MailSenderManager
    {
        public static bool SendMail(EmailMessage emailMessage)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress("mail@")
            };
            //foreach (var contacts in emailMessage.Contacts)
            //{
            //    mail.To.Add(new MailAddress(contacts));
            //}
            mail.To.Add(new MailAddress(emailMessage.Contacts));
            mail.Subject = emailMessage.Subject;
            mail.Body = emailMessage.Body;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.UTF8;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.HeadersEncoding = Encoding.UTF8;
            var smtpClient = new SmtpClient("smtp.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("mail", "password")
            };
            smtpClient.Send(mail);
            return true;
        }
    }
}
