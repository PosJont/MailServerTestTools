using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace NF47_MailServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Mail mail = new Mail();
            mail.Test_SendEmail();
        }
    }
    public class Mail
    {
        public Mail() { TestEmail(); }
        //設定smtp主機
        public string smtpAddress { get; set; }
        //設定Port
        public int port { get; set; }
        public bool enableSSL { get; set; } = true;
        //填入寄送方email和密碼
        public string emailFrom { get; set; }
        public string password { get; set; } 

        //收信方email
        public string emailTo { get; set; }


        public bool Success { get; set; } = false;

        public void Test_SendEmail()
        {
            Console.Write("mail server address > ");
            smtpAddress = Console.ReadLine();
            Console.Write("mail server port > ");
            int resutPort;
            int.TryParse(Console.ReadLine(),out resutPort);
            port = resutPort;

            Console.Write("mail server mail > ");
            emailFrom = Console.ReadLine();
            
            Console.Write("mail server password > ");
            password = Console.ReadLine();
            
            Console.Write("Send Mail (Own email)  > ");
            emailTo = Console.ReadLine();
            SendEmail();
        }

        public void SendEmail()
        {
            //主旨
            string subject = "TEST Mail Send On Status ";
            //內容
            string body = "TEST MailServer";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                // 若你的內容是HTML格式，則為True
                mail.IsBodyHtml = false;

                using (SmtpClient smtp = new SmtpClient(smtpAddress, port))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Timeout = 5000;
                    smtp.Send(mail);
                }
            }
        }

        public bool TestEmail()
        {
            try
            {
                using (TcpClient client = new TcpClient(smtpAddress, port))
                {
                    if (client.Connected)
                    {
                        Success = true;
                    }
                }
            }
            catch { }
            return Success;
        }
    }
}
