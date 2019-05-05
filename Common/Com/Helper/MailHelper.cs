using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 邮件发送类
    /// <para>MailHelper m1 = new MailHelper("smtp.qq.com", "9051xxxxx@qq.com", "授权码", "2694xxxxx@qq.com");</para>
    /// <para>m1.SendMail("心情还好", "66666666666666666");</para>
    /// </summary>
    public class MailHelper
    {
        public string SmtpServer { get; set; }
        public string UserName { get; set; }
        /// <summary> 授权码代替密码 </summary>
        public string Password { get; set; }
        public string SourceEmail { get; set; }
        public string[] TargetEmails { get; set; }

        public MailHelper(string smtpServer, string userName, string password, params string[] targetEmails)
        {
            SmtpServer = smtpServer;
            UserName = userName;
            Password = password;
            SourceEmail = userName;
            TargetEmails = targetEmails;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        public void SendMail(string subject, string message)
        {
            using (var mailMessage = new System.Net.Mail.MailMessage())
            {
                var smtpClient = new System.Net.Mail.SmtpClient(SmtpServer);
                foreach (var bossEmail in TargetEmails)
                    mailMessage.To.Add(bossEmail);
                //mailMessage.To.Add("269488683@qq.com");
                //mailMessage.To.Add("229661572@qq.com");
                mailMessage.Body = message;    // "你好！我是XXX。";
                mailMessage.From = new System.Net.Mail.MailAddress(SourceEmail);  // ("905130631@qq.com");
                mailMessage.Subject = subject;
                // 如果启用了“客户端授权码”，要用授权码代替密码
                smtpClient.Credentials = new System.Net.NetworkCredential(UserName, Password);
                // ("905130631@qq.com", "icvdovbnsnlbbfgj");
                //如果启用了ssl,并且不支持非安全连接置,还需要设置,只要支持ssl建议都开启ssl
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
            }
        }
    }
}
