using System.Net;
using System.Net.Mail;

namespace _0_FrameWork.Application
{
    public class SendEmail
    {
        public static void Send(string to, string subject, string body)
        {
            var mail = new MailMessage();
            var smtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("afghanlearn.ed@gmail.com", "شیفت کدر");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            smtpServer.Port = 587;
            smtpServer.Credentials = new NetworkCredential("ghzaladurani@gmail.com", "12Qudrat");
            smtpServer.EnableSsl = true;

            smtpServer.Send(mail);
        }
    }
}