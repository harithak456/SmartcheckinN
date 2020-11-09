using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ZS_SmartCheckIn.Models.Common
{
    public class Email
    {
        public int SendMail(string body, string tomail, string subject)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {

                    //int port = 587;
                    //string host = "smtp.gmail.com";
                    //string sendmail = "intellilabscloud@gmail.com";
                    //string password = "Edappilly@1452";
                    int port = 587;
                    string host = "smtp.yandex.com.tr";
                    string sendmail = "mailsupport@intellilabs.co.in";
                    string password = "admin@123";

                    HttpCookie OrgName = HttpContext.Current.Request.Cookies["OrgName"];
                    mail.From = new MailAddress(sendmail, OrgName.Value.Split('=')[1]);
                    mail.To.Add(tomail);
                    mail.Subject = subject;
                    mail.IsBodyHtml = true;
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                    mail.AlternateViews.Add(htmlView);
                    //using (SmtpClient smtp = new SmtpClient(host, port))
                    //{
                    //    smtp.Credentials = new NetworkCredential(sendmail, password);
                    //    smtp.EnableSsl = true;
                    //    smtp.Send(mail);
                    //}
                    using (SmtpClient emailClient = new SmtpClient(host, port))
                    {
                        System.Net.NetworkCredential userInfo = new System.Net.NetworkCredential(sendmail, password);
                        emailClient.UseDefaultCredentials = false;
                        emailClient.EnableSsl = true;
                        emailClient.DeliveryFormat = SmtpDeliveryFormat.International;
                        emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        if (!string.IsNullOrEmpty(userInfo.UserName.Trim()) && !string.IsNullOrEmpty(userInfo.Password.Trim()))
                        {
                            emailClient.Credentials = userInfo;
                        }
                        emailClient.Send(mail);
                    }

                }
                return 1;

            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public int SendConfirmMail(string guestName, string body, string subject)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {

                    //int port = 587;
                    //string host = "smtp.gmail.com";
                    //string sendmail = "intellilabscloud@gmail.com";
                    //string password = "Edappilly@1452";
                    int port = 587;
                    string host = "smtp.yandex.com.tr";
                    string sendmail = "mailsupport@intellilabs.co.in";
                    string password = "admin@123";
                    mail.From = new MailAddress(sendmail, guestName);
                    //mail.To.Add(tomail);
                    HttpCookie BranchMail =HttpContext.Current.Request.Cookies["BranchMail"];                
                    mail.To.Add(BranchMail.Value.Split('=')[1]);
                    mail.Subject = subject;

                    mail.IsBodyHtml = true;
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                    mail.AlternateViews.Add(htmlView);
                    //using (SmtpClient smtp = new SmtpClient(host, port))
                    //{
                    //    smtp.Credentials = new NetworkCredential(sendmail, password);
                    //    smtp.EnableSsl = true;
                    //    smtp.Send(mail);
                    //}
                    using (SmtpClient emailClient = new SmtpClient(host, port))
                    {
                        System.Net.NetworkCredential userInfo = new System.Net.NetworkCredential(sendmail, password);
                        emailClient.UseDefaultCredentials = false;
                        emailClient.EnableSsl = true;
                        emailClient.DeliveryFormat = SmtpDeliveryFormat.International;
                        emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        if (!string.IsNullOrEmpty(userInfo.UserName.Trim()) && !string.IsNullOrEmpty(userInfo.Password.Trim()))
                        {
                            emailClient.Credentials = userInfo;
                        }
                        emailClient.Send(mail);
                    }
                }
                return 1;

            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}