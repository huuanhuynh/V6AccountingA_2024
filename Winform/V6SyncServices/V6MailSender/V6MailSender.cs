using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace V6Tools
{
    public class V6MailSender
    {
        //setting ma hoa V6MailSenderConfig.xml

        /// <summary>
        /// Gmail, Yahoo!, Hotmail, AOL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="password"></param>
        /// <param name="sendto"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachments"></param>
        public bool SendEmail(string sender, string password,
            string sendto, string subject,
            string body, params string[] attachments)
        {
            try
            {
                sender = sender.ToLower();
                string sw = sender.Substring(sender.IndexOf('@'));
                switch (sw)
                {
                    case "@gmail.com":
                        sendEMailThroughGmail(sender, password, sendto, subject, body, attachments);
                        break;
                    case "@yahoo.com":
                        sendEMailThroughYahoo(sender, password, sendto, subject, body, attachments);
                        break;
                    case "@hotmail.com":
                        sendEMailThroughHotMail(sender, password, sendto, subject, body, attachments);
                        break;
                    case "@aol.com":
                        sendEMailThroughAOL(sender, password, sendto, subject, body, attachments);
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception)
            {
                
                throw;
            }
        }



        //method to send email to Gmail

        public void sendEMailThroughGmail(string sender, string password,
            string sendto, string subject,
            string body, string[] attachments)
        {
            try
            {
                //Mail Message
                MailMessage mM = new MailMessage();
                //Mail Address
                mM.From = new MailAddress(sender);//"sender@gmail.com"
                //receiver email id
                mM.To.Add(sendto);
                //subject of the email
                mM.Subject = subject;
                //deciding for the attachment
                foreach (var item in attachments)
                {
                    if (File.Exists(item))
                        mM.Attachments.Add(new Attachment(item));
                }

                //add the body of the email
                mM.Body = body;
                mM.IsBodyHtml = true;

                //SMTP client
                SmtpClient sC = new SmtpClient("smtp.gmail.com");

                System.Security.Cryptography.X509Certificates.X509Certificate a
                    = new System.Security.Cryptography.X509Certificates.X509Certificate();

                sC.ClientCertificates.Add(a);

                sC.UseDefaultCredentials = true;
                sC.EnableSsl = true;
                //credentials to login in to Gmail account
                sC.Credentials = new NetworkCredential(sender, password);
                //sC.Credentials = basicCredential;
                //port number for Gmail mail
                sC.Port = 587;
                //enabled SSL
                sC.EnableSsl = true;
                //Send an email
                sC.Send(mM);

                mM.Dispose();
            }//end of try block
            catch// (Exception ex)
            {
                throw;

            }//end of catch
        }

        //Method to send email from YAHOO!!
        public void sendEMailThroughAOL(string sender, string password,
            string sendto, string subject,
            string body, string[] attachments)
        {
            try
            {
                //mail message
                MailMessage mM = new MailMessage();
                //Mail Address
                mM.From = new MailAddress(sender);
                //emailid to send
                mM.To.Add(sendto);
                //your subject line of the message
                mM.Subject = subject;
                //now attached the file
                foreach (var item in attachments)
                {
                    if (File.Exists(item))
                        mM.Attachments.Add(new Attachment(item));
                }
                //add the body of the email
                mM.Body = body;
                mM.IsBodyHtml = false;
                //SMTP 
                SmtpClient SmtpServer = new SmtpClient();
                //your credential will go here
                SmtpServer.Credentials = new System.Net.NetworkCredential(sender, password);
                //port number to login yahoo server
                SmtpServer.Port = 587;
                //yahoo host name
                SmtpServer.Host = "smtp.aol.com";
                //Send the email
                SmtpServer.Send(mM); mM.Dispose();
            }//end of try block
            catch// (Exception ex)
            {
                throw;
                //<pre>
            }//end of catch
        }

        //Method to send email from YAHOO!!
        public void sendEMailThroughYahoo(string sender, string password,
            string sendto, string subject,
            string body, string[] attachments)
        {
            try
            {
                //mail message
                MailMessage mM = new MailMessage();
                //Mail Address
                mM.From = new MailAddress(sender);
                //emailid to send
                mM.To.Add(sendto);
                //your subject line of the message
                mM.Subject = subject;
                //now attached the file
                foreach (var item in attachments)
                {
                    if (File.Exists(item))
                        mM.Attachments.Add(new Attachment(item));
                }
                //add the body of the email
                mM.Body = body;
                mM.IsBodyHtml = false;
                //SMTP 
                SmtpClient SmtpServer = new SmtpClient();
                //your credential will go here
                SmtpServer.Credentials = new System.Net.NetworkCredential(sender, password);
                //port number to login yahoo server
                SmtpServer.Port = 587;
                //yahoo host name
                SmtpServer.Host = "smtp.mail.yahoo.com";
                //Send the email
                SmtpServer.Send(mM); mM.Dispose();
            }//end of try block
            catch// (Exception ex)
            {
                throw;
            }//end of catch
        }//end of Yahoo Email Method

        //method to send email to HOTMAIL
        public void sendEMailThroughHotMail(string sender, string password,
            string sendto, string subject,
            string body, string[] attachments)
        {
            try
            {
                //Mail Message
                MailMessage mM = new MailMessage();
                //Mail Address
                mM.From = new MailAddress(sender);
                //receiver email id
                mM.To.Add(sendto);
                //subject of the email
                mM.Subject = subject;
                //deciding for the attachment
                foreach (var item in attachments)
                {
                    if (File.Exists(item))
                        mM.Attachments.Add(new Attachment(item));
                }
                //add the body of the email
                mM.Body = body;
                mM.IsBodyHtml = true;
                //SMTP client
                SmtpClient sC = new SmtpClient("smtp.live.com");
                //port number for Hot mail
                sC.Port = 25;
                //credentials to login in to hotmail account
                sC.Credentials = new NetworkCredential(sender, password);
                //enabled SSL
                sC.EnableSsl = true;
                //Send an email
                sC.Send(mM); mM.Dispose();
            }//end of try block
            catch// (Exception ex)
            {
                throw;
            }//end of catch
        }

        void Send()
        {
            string smtpAddress = "smtp.mail.yahoo.com";
            int portNumber = 587;
            bool enableSSL = true;

            string emailFrom = "email@yahoo.com";
            string password = "abcdefg";
            string emailTo = "someone@domain.com";
            string subject = "Hello";
            string body = "Hello, I'm just writing this to say Hi!";
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                // Can set to false, if you are sending pure text.

                mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                SmtpClient smtp = new SmtpClient(smtpAddress, portNumber);
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
    }//end class
}//end namespace
