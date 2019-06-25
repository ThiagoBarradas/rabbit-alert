using RabbitAlerts.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net;

namespace RabbitAlerts.Queue
{
    public class Rabbit 
    {
        private RestClient RabbitMQClient { get; set; }

        private RabbitConfiguration RabbitConfiguration { get; set; }

        public Rabbit(RabbitConfiguration rabbitConfiguration)
        {
            this.RabbitConfiguration = rabbitConfiguration;
        }

        public long GetCount()
        {
            var restClient = new RestClient(this.RabbitConfiguration.Host);
            restClient.Authenticator = new HttpBasicAuthenticator(this.RabbitConfiguration.User, this.RabbitConfiguration.Pass);

            var restRequest = new RestRequest("/api/queues/{virtual_host}/{queue}", Method.GET);
            var virtualHost = (string.IsNullOrWhiteSpace(this.RabbitConfiguration.VirtualHost)) ? "/" : this.RabbitConfiguration.VirtualHost;

            restRequest.AddUrlSegment("virtual_host", virtualHost);
            restRequest.AddUrlSegment("queue", this.RabbitConfiguration.Queue);

            var restResponse = restClient.Execute<dynamic>(restRequest);

            if (restResponse.ErrorException != null || restResponse.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Pushover Notification Failed :(");
                Console.WriteLine("ErrorException: {0}", restResponse.ErrorException?.Message);
                Console.WriteLine("StatusCode: {0}", restResponse.StatusDescription);
                return 0;
            }

            return restResponse.Data["messages"];
        }
    }
}
