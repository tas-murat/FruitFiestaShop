using Azure.Messaging.ServiceBus;
using MessageBus.ConfigurationModel;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace MessageBus
{
    public class AzureMessageBus : IMessageBus
    {

        private readonly ServiceBusConfiguration _configuration;

        public AzureMessageBus(IOptions<ServiceBusConfiguration> configuration )
        {
            _configuration = configuration.Value;
        }
        private string connectionString = "";

        public async Task PublishMessage(object message, string topic_queue_Name)
        {
            await using var client = new ServiceBusClient(_configuration.ConnectionString);

            ServiceBusSender sender = client.CreateSender(topic_queue_Name);

            var jsonMessage = JsonConvert.SerializeObject(message);
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding
                .UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            await sender.SendMessageAsync(finalMessage);
            await client.DisposeAsync();
        }
    }
}
