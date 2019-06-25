using RabbitAlerts.Models;
using System;

namespace RabbitAlerts.Alerts
{
    public class Alert
    {
        private SlackAlert SlackAlert { get; set; }

        private PushoverAlert PushoverAlert { get; set; }

        private Configuration Configuration { get; set; }

        public Alert(Configuration configuration)
        {
            this.Configuration = configuration;
            this.SlackAlert = new SlackAlert(configuration.Slack);
            this.PushoverAlert = new PushoverAlert(configuration.Pushover);
        }

        public void AlertMe(string title, string content)
        {
            Console.WriteLine("F****CK!!!!");
            Console.WriteLine(" - {0}", title);
            Console.WriteLine(" - {0}", content);

            this.PushoverAlert.AlertMe(title, content);
            this.SlackAlert.AlertMe(title, content);
        }
    }
}
