using RabbitAlerts.Models;
using RestSharp;
using System;
using System.Net;

namespace RabbitAlerts.Alerts
{
    public class PushoverAlert
    {
        private static string PushoverAPI = "https://api.pushover.net/1/messages.json";

        private PushoverConfiguration PushoverConfiguration { get; set; }

        public PushoverAlert(PushoverConfiguration pushoverConfiguration)
        {
            this.PushoverConfiguration = pushoverConfiguration;
        }

        public void AlertMe(string title, string content)
        {
            if (this.PushoverConfiguration == null)
            {
                return;
            }

            var restClient = new RestClient(PushoverAPI);
            RestRequest restRequest = new RestRequest(Method.POST);
            restRequest.AddJsonBody(new
            {
                token = this.PushoverConfiguration.Token,
                user = this.PushoverConfiguration.User,
                title = title,
                message = content
            });

            IRestResponse restResponse = restClient.Execute(restRequest);

            if (restResponse.ErrorException != null || restResponse.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Pushover Notification Failed :(");
                Console.WriteLine("ErrorException: {0}", restResponse.ErrorException?.Message);
                Console.WriteLine("StatusCode: {0}", restResponse.StatusDescription);
            }
        }
    }
}
