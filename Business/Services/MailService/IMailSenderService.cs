namespace Business.Services.MailService
{
    public interface IMailSenderService
    {
        bool SendMail(EmailMessage emailMessage);
    }
}
