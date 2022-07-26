using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProjetoFinalApi.Models.Enuns;
using ProjetoFinalApi.Services.Configs;
using System.Text;

namespace ProjetoFinalApi.Services
{
    public class AzureServiceBusPublisher
    {
        private readonly AzureServiceBusConfig _settings;

        private readonly ServiceBusClient _serviceBusClient;

        public AzureServiceBusPublisher(IOptions<AzureServiceBusConfig> settings, 
            ServiceBusClient serviceBusClient)
        {
            _settings = settings.Value;
            _serviceBusClient = serviceBusClient;
        }

        public async Task Send(TipoEvento eventType, string content)
        {
            var sbqSender = _serviceBusClient.CreateSender(_settings.QueueName);

            var payload = JsonConvert.SerializeObject(new
            {
                actionType = eventType,
                content
            });

            var body = Encoding.UTF8.GetBytes(payload);
            var message = new ServiceBusMessage(body);

            await sbqSender.SendMessageAsync(message);
        }
    }
}
