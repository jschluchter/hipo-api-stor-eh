using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace HiPo.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            string eventHubConnectionString = "Endpoint=sb://eh-demo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=shihzPAai4oz8A9ZEYVxm6FvAuPszO+jx7683yYhkS0=";
            string eventHubName = "eh-example";
            //string storageAccountName = "{storage account name}";
            //string storageAccountKey = "{storage account key}";
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=stehdemo;AccountKey=smc73oUiyJzWk045+QMbFjY9umOVmnRpZhxTTTm2rnWpQA1wprBGITbCjlKtruPr192RB0enyoNmo9K4bJblzg==";

            string eventProcessorHostName = Guid.NewGuid().ToString();
            EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, eventHubName, EventHubConsumerGroup.DefaultGroupName, eventHubConnectionString, storageConnectionString);
            Console.WriteLine("Registering EventProcessor...");
            var options = new EventProcessorOptions();
            options.ExceptionReceived += (sender, e) => { Console.WriteLine(e.Exception); };
            eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>(options).Wait();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();
            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}
