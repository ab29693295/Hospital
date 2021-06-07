using Edu.Models;
using Edu.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Edu.Service
{
    public class EmailService
    {
        public bool sendEmail(EmailCs e_Email)
        {
            //默认的发送地址为联系人的邮箱 
            MailAddress mailfrom = new MailAddress(e_Email.Email, e_Email.EmailUser, System.Text.Encoding.UTF8);//发件人邮箱地址，名称，编码UTF8  
            MailAddress mailto = new MailAddress(e_Email.ReceiveEmail, e_Email.ReceiveName, System.Text.Encoding.UTF8);//收件人邮箱地址，名称，编码UTF8   
            MailMessage mail = new MailMessage(mailfrom, mailto);
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.HeadersEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Body = e_Email.SendBody;
            mail.Subject = e_Email.Subject;
            using (SmtpClient client = new SmtpClient())
            {
                client.Host = e_Email.EmailSmtp;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(e_Email.Email, e_Email.EmailPassword);
                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {

                    LogHelper.Error(string.Format("邮件发送失败,发送用户名称：{0}邮箱:{1},失败原因:{2}", e_Email.ReceiveName, e_Email.ReceiveEmail, ex.Message));
                    return false;
                }
                mail.Dispose();
            }
            return true;
        }
    }
}
