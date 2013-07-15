using RestSharp;
using SendGridMail;
using SendGridMail.Transport;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace NorthCarolinaTaxRecoveryCalculator.Misc
{
    public interface IEmailSender
    {
        void SendMail(string to, string subject, string body);
    }

    /// <summary>
    /// Send an email usng the 'Send Grid' providor
    /// </summary>
    public class SendGridEmailSender : IEmailSender
    {
        public void SendMail(string to, string subject, string body)
        {
            //create the email
            var email = SendGrid.GetInstance();

            email.From = new MailAddress("kevin@keyholeservices.com");
            email.AddTo(new List<string> {to});
            email.Subject = subject;
            email.Text = body;
            email.Html = body;

            //send the email
            var username = ConfigurationManager.AppSettings["SENDGRID_USERNAME"];
            var password = ConfigurationManager.AppSettings["SENDGRID_PASSWORD"];

            var credentials = new NetworkCredential(username, password);

            var transport = SMTP.GetInstance(credentials);
            transport.Deliver(email);

        }
    }

    /// <summary>
    /// Send an email usng the 'Mail Gun' providor
    /// </summary>
    public class MailGunEmailSender : IEmailSender
    {
        public void SendMail(string to, string subject, string body)
        {
            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";

            string apikey = ConfigurationManager.AppSettings["MAILGUN_API_KEY"];
            client.Authenticator = new HttpBasicAuthenticator("api", apikey);

            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                 "app10744.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";

            request.AddParameter("from", "North Carolina Tax Recovery Calculator <noreply@nctaxcalc.com>");
            request.AddParameter("subject", subject);
            request.AddParameter("to", to);
            request.AddParameter("text", body);//"You have been invited to a new project.\nClick the link to accept the invitation.\nhttps://mail.google.com/mail/u/0/?shva=1#inbox");
            request.Method = Method.POST;

            client.Execute(request);
        }
    }
}