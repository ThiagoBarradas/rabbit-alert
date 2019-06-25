using System;

namespace RabbitAlerts.Models
{
    public class Configuration
    {
        public SlackConfiguration Slack { get; set; }

        public PushoverConfiguration Pushover { get; set; }

        public RabbitConfiguration Rabbit { get; set; }

        public LimitsConfiguration Limits { get; set; }

        public static Configuration FromEnvironment()
        {
            var configuration = new Configuration();

            // slack
            var slackUrl = Environment.GetEnvironmentVariable("slack_url");
            if (!string.IsNullOrWhiteSpace(slackUrl))
            {
                configuration.Slack = new SlackConfiguration { Url = slackUrl }; 
            }

            // pushover
            var pushoverToken = Environment.GetEnvironmentVariable("pushover_token");
            var pushoverUser = Environment.GetEnvironmentVariable("pushover_user");
            if (!string.IsNullOrWhiteSpace(pushoverToken) && !string.IsNullOrWhiteSpace(pushoverUser))
            {
                configuration.Pushover = new PushoverConfiguration
                {
                    Token = pushoverToken,
                    User = pushoverUser
                };
            }

            // rabbit
            var rabbitUser = Environment.GetEnvironmentVariable("rabbit_user");
            var rabbitPass = Environment.GetEnvironmentVariable("rabbit_pass");
            var rabbitVirtualHost = Environment.GetEnvironmentVariable("rabbit_virtual_host");
            var rabbitHost = Environment.GetEnvironmentVariable("rabbit_host");
            var rabbitQueue = Environment.GetEnvironmentVariable("rabbit_queue");
            configuration.Rabbit = new RabbitConfiguration
            {
                User = rabbitUser,
                Pass = rabbitPass,
                Host = rabbitHost,
                VirtualHost = rabbitVirtualHost,
                Queue = rabbitQueue
            };

            // limits
            var min = Environment.GetEnvironmentVariable("min");
            var max = Environment.GetEnvironmentVariable("max");
            configuration.Limits = new LimitsConfiguration(min, max);

            return configuration;
        }
    }

    public class LimitsConfiguration
    {
        public LimitsConfiguration(string min, string max)
        {
            this.Max = StringToNullableInt(max);
            this.Min = StringToNullableInt(min);
        }

        public int? Min { get; set; }

        public int? Max { get; set; }

        private static int? StringToNullableInt(string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }
    }

    public class RabbitConfiguration
    {
        public string Host { get; set; }

        public string User { get; set; }

        public string Pass { get; set; }

        public string Queue { get; set; }

        public string VirtualHost { get; set; }
    }

    public class PushoverConfiguration
    {
        public object Token { get; internal set; }

        public object User { get; internal set; }
    }

    public class SlackConfiguration
    {
        public string Url { get; set; }
    }
}
