
using EventInfo.DTOs;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using Newtonsoft.Json;
using System.Text;

namespace EventInfo.Services
{
    public class MQTTService : IMQTTService
    {
        private Dictionary<string, Action<ArraySegment<byte>>> _handlersDictionary;
        private IConfiguration _configuration;
        private IMqttClient _mqttClient;
        private MqttFactory _mqttFactory;

        public  MQTTService(IConfiguration configuration)
        {
            _configuration = configuration;
            this._mqttFactory = new MqttFactory();
            this._mqttClient = _mqttFactory.CreateMqttClient();
            this._handlersDictionary=new Dictionary<string, Action<ArraySegment<byte>>>();
        }

        public async Task ConnectAsync()
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(this._configuration.GetValue<string>("MQTTServer"), this._configuration.GetValue<int>("MQTTPort"))
                .Build();
            await this._mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
        }

        public async Task DisconnectAsync()
        {
            await this._mqttClient.DisconnectAsync(
                new MqttClientDisconnectOptionsBuilder()
                .WithReason(MqttClientDisconnectOptionsReason.NormalDisconnection).Build());
        }


        public void AddTopicHandler(string topic, Action<ArraySegment<byte>> handler)
        {
            this._handlersDictionary[topic] = handler;
        }
        public void Listen()
        {
            _mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                //Console.WriteLine((e.ApplicationMessage.PayloadSegment));
                ////Console.WriteLine(JsonConvert.DeserializeObject<SensorMessage>(Encoding.UTF8.GetString( e.ApplicationMessage.PayloadSegment)).Data.Voltage);
                var topic = e.ApplicationMessage.Topic;
                this._handlersDictionary.TryGetValue(topic, out var handler);
                if (handler != null)
                {
                    handler(e.ApplicationMessage.PayloadSegment);
                }
                else
                {
                    Console.WriteLine("Unhandled topic");
                }
                return Task.CompletedTask;
            };
        }

        public async Task SubscribeToTopicAsync(string topic)
        {
            var mqttSubscribeOptions = _mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(
                    f =>
                    {
                        f.WithTopic(topic);
                    })
                .Build();
            _=await this._mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }

        public async Task UnsubscribeFromTopicAsync(string topic)
        {
            var mqttSubscribeOptions = _mqttFactory.CreateUnsubscribeOptionsBuilder()
               .WithTopicFilter(topic) 
               .Build();
            _ = await this._mqttClient.UnsubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }

        public async Task PublishMessageAsync(string topic, string payloadSerialized)
        {
            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payloadSerialized)
                .Build();

            await _mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

        }

    }
}
