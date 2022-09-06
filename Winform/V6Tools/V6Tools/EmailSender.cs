using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

//using Outlook = Microsoft.Office.Interop.Outlook;

namespace V6Tools
{
    public class EmailSender
    {
        /// <summary>
        /// Gmail, Yahoo!, Hotmail, AOL
        /// </summary>
        /// <param name="sender">Email người gửi.</param>
        /// <param name="password">Password người gửi.</param>
        /// <param name="sendto">Email người nhận.</param>
        /// <param name="subject">Chủ đề.</param>
        /// <param name="body">Nội dung.</param>
        /// <param name="attachments">Danh sách file upload lên.</param>
        public void SendEmail(string sender, string password,
            string sendto, string subject,
            string body, params string[] attachments)
        {
            sender = sender.ToLower();
            if (attachments == null) attachments = new string[] { };
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
        }

        /// <summary>
        /// Send an email from [DELETED]
        /// </summary>
        /// <param name="smtpHost">Email server host.</param>
        /// <param name="to">Message to address</param>
        /// <param name="body">Text of message to send</param>
        /// <param name="subject">Subject line of message</param>
        /// <param name="fromAddress">Message from address</param>
        /// <param name="fromDisplay">Display name for "message from address"</param>
        /// <param name="credentialUser">User whose credentials are used for message send</param>
        /// <param name="credentialPassword">User password used for message send</param>
        /// <param name="attachments">Optional attachments for message</param>
        private static void Email(string smtpHost, string to, string body, string subject,
                                 string fromAddress, string fromDisplay, string credentialUser, string credentialPassword,
                                 params MailAttachment[] attachments)
        {
            string host = smtpHost;// ConfigurationManager.AppSettings["SMTPHost"];
            //body = UpgradeEmailFormat(body);
            try
            {
                MailMessage mail = new MailMessage();
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(to));
                mail.From = new MailAddress(fromAddress, fromDisplay, Encoding.UTF8);
                mail.Subject = subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.Normal;
                foreach (MailAttachment ma in attachments)
                {
                    mail.Attachments.Add(ma.File);
                }
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential(credentialUser, credentialPassword);
                smtp.Host = host;
                smtp.Send(mail);
            }
            catch
            {
                StringBuilder sb = new StringBuilder(1024);
                sb.Append("\nTo:" + to);
                sb.Append("\nbody:" + body);
                sb.Append("\nsubject:" + subject);
                sb.Append("\nfromAddress:" + fromAddress);
                sb.Append("\nfromDisplay:" + fromDisplay);
                sb.Append("\ncredentialUser:" + credentialUser);
                sb.Append("\ncredentialPasswordto:" + credentialPassword);
                sb.Append("\nHosting:" + host);
                //ErrorLog(sb.ToString(), ex.ToString(), ErrorLogCause.EmailSystem);
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
                    if(File.Exists(item))
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
                SmtpServer.Send(mM);
            }//end of try block
            catch
            {

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
                SmtpServer.Send(mM);
            }//end of try block
            catch
            {
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
                sC.Send(mM);
            }//end of try block
            catch
            {


            }//end of catch
        }
        
        public void Send()
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

        public class MailAttachment
        {
            #region Fields
            private MemoryStream stream;
            private string filename;
            private string mediaType;
            #endregion
            #region Properties
            /// <summary>
            /// Gets the data stream for this attachment
            /// </summary>
            public Stream Data { get { return stream; } }
            /// <summary>
            /// Gets the original filename for this attachment
            /// </summary>
            public string Filename { get { return filename; } }
            /// <summary>
            /// Gets the attachment type: Bytes or String
            /// </summary>
            public string MediaType { get { return mediaType; } }
            /// <summary>
            /// Gets the file for this attachment (as a new attachment)
            /// </summary>
            public Attachment File { get { return new Attachment(Data, Filename, MediaType); } }
            #endregion
            #region Constructors
            /// <summary>
            /// Construct a mail attachment form a byte array
            /// </summary>
            /// <param name="data">Bytes to attach as a file</param>
            /// <param name="filename">Logical filename for attachment</param>
            public MailAttachment(byte[] data, string filename)
            {
                this.stream = new MemoryStream(data);
                this.filename = filename;
                this.mediaType = MediaTypeNames.Application.Octet;
            }
            /// <summary>
            /// Construct a mail attachment from a string
            /// </summary>
            /// <param name="data">String to attach as a file</param>
            /// <param name="filename">Logical filename for attachment</param>
            public MailAttachment(string data, string filename)
            {
                this.stream = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(data));
                this.filename = filename;
                this.mediaType = MediaTypeNames.Text.Html;
            }
            #endregion
        }
    }


}
