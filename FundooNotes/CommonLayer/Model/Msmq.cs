using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class Msmq
    {
        MessageQueue messageQue = new MessageQueue();

        public void Sender(string token)
        {
            this.messageQue.Path = @".\private$\Tokens";
            try
            {
                if(!MessageQueue.Exists(this.messageQue.Path))
                {
                    MessageQueue.Create(this.messageQue.Path);
                }
                this.messageQue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                this.messageQue.ReceiveCompleted += MessageQue_ReceiveCompleted;
                this.messageQue.Send(token);
                this.messageQue.BeginReceive();
                this.messageQue.Close();
            }
            catch (Exception)
            {

            }
        }

        private void MessageQue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = this.messageQue.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpclient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("janto4115@gmail.com", "joeanto@123"),
                    EnableSsl = true


                };
                mailMessage.From = new MailAddress("janto4115@gmail.com");
                mailMessage.To.Add(new MailAddress("janto4115@gmail.com"));
                mailMessage.Body = token;
                mailMessage.Subject = "FundooNote App reset Link";
                smtpclient.Send(mailMessage);


            }
            catch(Exception)
            {
                throw;
            }
        }
    } 
}
