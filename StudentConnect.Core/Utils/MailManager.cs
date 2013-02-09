using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Text;
using StudentConnect.Data;

namespace StudentConnect
{
    public sealed class MailManager
    {
        private string username;
        private string password;

        public MailManager()
        {
            username = ConfigurationManager.AppSettings["SENDGRID_USERNAME"];
            password = ConfigurationManager.AppSettings["SENDGRID_PASSWORD"];
        }

        public void NotifySave(ContactInfo info)
        {
            string content = info.ToString();
            SendEmail("glenn.ferrie@gmail.com", string.Format("Student Registered: {0}", info.FullName), content);
        }

        private void SendEmail(string to, string subject, string body)
        {
            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(username, password);
            var message = new MailMessage();
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;
            client.Send(message);
        }
    }
}