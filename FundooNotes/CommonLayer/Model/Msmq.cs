// <copyright file="Msmq.cs" company="mahafuz">
//     Company copyright tag.
// </copyright>
namespace CommonLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using Experimental.System.Messaging;

    /// <summary>
    /// microsoft message queue class
    /// </summary>
    public class Msmq
    {
        /// <summary>
        /// The message queue
        /// </summary>
         MessageQueue messageQue = new MessageQueue();

        /// <summary>
        /// Senders the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        public void Sender(string token)
        {
            this.messageQue.Path = @".\private$\Tokens";
            try
            {
                if (!MessageQueue.Exists(this.messageQue.Path))
                {
                    MessageQueue.Create(this.messageQue.Path);
                }

                this.messageQue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                this.messageQue.ReceiveCompleted += this.MessageQue_ReceiveCompleted;
                this.messageQue.Send(token);
                this.messageQue.BeginReceive();
                this.messageQue.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Handles the Receive Completed event of the Message Queue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ReceiveCompletedEventArgs"/> instance containing the event data.</param>
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
                    Credentials = new NetworkCredential("janto4115@gmail.com", "jabbawockeez@5"),
                    EnableSsl = true
                };
                mailMessage.From = new MailAddress("janto4115@gmail.com");
                mailMessage.To.Add(new MailAddress("janto4115@gmail.com"));
                mailMessage.Body = token;
                mailMessage.Subject = "FundooNote App reset Link";
                smtpclient.Send(mailMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }
    } 
}
