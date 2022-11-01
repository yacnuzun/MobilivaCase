using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Utilities.MailService
{
    public class EmailMessage
    {
        public string Contacts { get; set; }
        public string SenderMail { get; set; }
        public string Password { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
