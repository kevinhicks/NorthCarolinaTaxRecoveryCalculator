using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using RestSharp;
using System.Web;
using System.Configuration;

namespace NorthCarolinaTaxRecoveryCalculator.Misc
{
    public class EmailSender
    {

        public static void SendEmail(string to, string subject, string body)
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