using RabbitAlerts.Alerts;
using RabbitAlerts.Models;
using RabbitAlerts.Queue;
using System;

namespace RabbitAlerts
{
    public class Program
    {
        public static string Title = "'{0}' have a problem!";
        public static string Content = "Current count ({0}) is out of range (min: {1} and max: {2})";

        public static void Main(string[] args)
        {
            Console.WriteLine("Rabbit Alerts");
            Console.WriteLine("Start! > {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Rabbit rabbit = null;

            try
            {
                var configuration = Configuration.FromEnvironment();

                rabbit = new Rabbit(configuration.Rabbit);
                var alert = new Alert(configuration);

                var currentCount = rabbit.GetCount();
                var title = string.Format(Title, configuration.Rabbit.Queue);
                var content = string.Format(Content, currentCount, configuration.Limits.Min, configuration.Limits.Max);

                if (currentCount < configuration.Limits.Min ||
                    currentCount > configuration.Limits.Max)
                {
                    alert.AlertMe(title, content);
                }
                else
                {
                    Console.WriteLine("All is fine!!");
                    Console.WriteLine("CurrentCount: {0}", currentCount);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("### Ooops! Exception: {0}", ex.Message);
            }

            Console.WriteLine("Finish! > {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
