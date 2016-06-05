using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SportLife.Utils
{
    public class MailService
    {
        private MailMessage message = new MailMessage();

        public MailService()
        {
            message.From = new MailAddress(ConfigurationManager.AppSettings["mailerAddress"], ConfigurationManager.AppSettings["mailerDisplayName"]);
        }

        public string SendMail(string emailTo, string messageToSend)
        {

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
          //  client.Timeout = 10;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["mailerAddress"], ConfigurationManager.AppSettings["mailPassword"]);

            //  MailMessage mm = new MailMessage("donotreply@domain.com", "sendtomyemail@domain.co.uk", "test", "test");
            message.To.Add(new MailAddress(emailTo));
            message.Subject = "Activate your SportLife account !";
            message.Body = "Activate your account !\n" + "Please access the flowing link...\n" + messageToSend;

            message.BodyEncoding = UTF8Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
        }
    }
}
