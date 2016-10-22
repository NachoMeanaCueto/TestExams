using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;


namespace TestExams.UsersGestion
{
    public static class MailGestion
    {
        public static void SendMail(MailMessage message)
        {
            using (var db = new DBModel.TestExamsContext())
            {
                var sender = db.AppMails.FirstOrDefault();
                using (SmtpClient client = new SmtpClient(sender.Host.ToString()))
                {
                    client.Port = sender.port;
                    client.UseDefaultCredentials = false;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    client.ServicePoint.MaxIdleTime = 1;
                    client.Credentials = new NetworkCredential(sender.MailAddress, sender.Password.Decrypt());
                    client.Send(message);

                }
            }
        }


        //TODO implementar gestion de usuarios y mails personalizados
        public static MailMessage MessageGenerator()
        {
            MailMessage message = null;

            using (var db = new DBModel.TestExamsContext())
            {
                var sender = db.AppMails.FirstOrDefault();

                 message = new MailMessage(
                    sender.MailAddress, 
                    "nachomeanacueto@gmail.com", 
                    "Mensaje de prueba",
                    "Mensaje de prueba 1");
            }
            return message;
        }
    }
}
