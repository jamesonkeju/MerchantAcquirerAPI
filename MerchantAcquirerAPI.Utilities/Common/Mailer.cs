using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Utilities.Common
{
    public class Mailer
    {
        private IConfiguration _configuration;

        public Mailer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //
        // TODO: Add constructor logic here
        //
        public  void SendMailMessage(string from, string to, string bcc, string cc, string subject, string body)
        {
            // Instantiate a new instance of MailMessage
            MailMessage mMailMessage = new MailMessage();

            // Set the sender address of the mail message
            mMailMessage.From = new MailAddress(from);
            // Set the recepient address of the mail message
            mMailMessage.To.Add(new MailAddress(to));
            // Check if the bcc value is null or an empty string
            if ((bcc != null) && (bcc != string.Empty))
            {
                // Set the Bcc address of the mail message
                mMailMessage.Bcc.Add(new MailAddress(bcc));
            }

            // Check if the cc value is null or an empty value
            if ((cc != null) && (cc != string.Empty))
            {
                // Set the CC address of the mail message
                mMailMessage.CC.Add(new MailAddress(cc));
            }       // Set the subject of the mail message
            mMailMessage.Subject = subject;
            // Set the body of the mail message
            mMailMessage.Body = body;
            // Set the format of the mail message body as HTML
            mMailMessage.IsBodyHtml = true;
            // Set the priority of the mail message to normal
            mMailMessage.Priority = MailPriority.Normal;

            //instantiate a new instance of SmtpClient

            SmtpClient smtp = new SmtpClient();
            smtp.Host = _configuration["smtp"];
            smtp.Port = 25;
            //send the mail message
            smtp.Credentials = new System.Net.NetworkCredential(_configuration["smtpusername"], _configuration["smtppassword"]);

            smtp.Send(mMailMessage);
        }

        public void MailSend(string from, string to, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(from);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(to));


                SmtpClient smtp = new SmtpClient();
                smtp.Host = _configuration["smtp"];
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = mailMessage.From.Address;
                NetworkCred.Password = "password";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);
            }
        }

        public  bool sendMail(string destinationEmail, string sourceEmail, string ccEmailAddress, string bccEmailAddress, string body, string subject, string filename)
        {
            bool status = false;
            try
            {
                SmtpClient smClient = new SmtpClient();
                smClient.Host = _configuration["smtp"];
                smClient.Port = Convert.ToInt32(_configuration["port"]);
                MailMessage mailMsg = new MailMessage();

                mailMsg.From = new MailAddress(sourceEmail);
                if (destinationEmail.Contains(","))
                {
                    string[] mailParts = destinationEmail.Split(new char[] { ',' });
                    if (mailParts != null && mailParts.Length > 0)
                    {
                        foreach (string destmail in mailParts)
                        {
                            mailMsg.To.Add(destmail);
                        }
                    }
                }
                else
                {
                    mailMsg.To.Add(destinationEmail);
                }

                if (ccEmailAddress != "")
                {
                    mailMsg.CC.Add(ccEmailAddress);
                }

                if (bccEmailAddress != "")
                {
                    mailMsg.Bcc.Add(bccEmailAddress);
                }

                mailMsg.Subject = subject;
                mailMsg.Body = body;
                mailMsg.IsBodyHtml = true;

                smClient.Send(mailMsg);
                status = true;
            }
            catch (SmtpFailedRecipientException smtpExc)
            {
                status = false;
            }

            return status;
        }

        public  bool sendMailWithAttachement(string destinationEmail, string sourceEmail, string ccEmailAddress, string bccEmailAddress, string body, string subject, string filename)
        {
            bool status = false;
            try
            {
                SmtpClient smClient = new SmtpClient();
                smClient.Host = _configuration["smtp"];
                smClient.Port = Convert.ToInt32(_configuration["port"]);
                MailMessage mailMsg = new MailMessage();

                mailMsg.From = new MailAddress(sourceEmail);
                if (destinationEmail.Contains(","))
                {
                    string[] mailParts = destinationEmail.Split(new char[] { ',' });
                    if (mailParts != null && mailParts.Length > 0)
                    {
                        foreach (string destmail in mailParts)
                        {
                            mailMsg.To.Add(destmail);
                        }
                    }
                }
                else
                {
                    mailMsg.To.Add(destinationEmail);
                }

                if (ccEmailAddress != "")
                {
                    mailMsg.CC.Add(ccEmailAddress);
                }

                if (bccEmailAddress != "")
                {
                    mailMsg.Bcc.Add(bccEmailAddress);
                }

                //   mailMsg.AlternateViews.Add(avHtml);
                mailMsg.Subject = subject;
                mailMsg.Body = body;
                mailMsg.IsBodyHtml = true;

                smClient.Send(mailMsg);
                status = true;
            }
            catch (SmtpFailedRecipientException smtpExc)
            {
                status = false;
            }

            return status;
        }

    }
}
