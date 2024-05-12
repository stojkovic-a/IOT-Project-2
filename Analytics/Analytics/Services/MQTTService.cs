
using Analytics.DTOs;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using System.Text;

namespace Analytics.Services
{
    public class MQTTService : IMQTTService
    {
        private IConfiguration _configuration;
        private IMqttClient _mqttClient;
        private MqttFactory _mqttFactory;
        //private MqttClientOptions _mqttClientOptions;
        public  MQTTService(IConfiguration configuration)
        {
            _configuration = configuration;
            this._mqttFactory = new MqttFactory();
            this._mqttClient = _mqttFactory.CreateMqttClient();
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

        public void SetTopicHandler()
        {
            _mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                Console.WriteLine("Received application message.");
                Console.WriteLine(Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment));
                return Task.CompletedTask;
            };
        }

        //public async Task SubscribeToTopic(string topic)
        //{
        //    var mqttFactory = new MqttFactory();

        //    using ( mqttClient = mqttFactory.CreateMqttClient())
        //    {
        //        var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(this._configuration.GetValue<string>("MQTTServer")).Build();

        //        mqttClient.ApplicationMessageReceivedAsync += e =>
        //        {
        //            Console.WriteLine("Received application message.");
        //            Console.WriteLine(e);

        //            return Task.CompletedTask;
        //        };

        //        await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

        //        var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
        //            .WithTopicFilter(
        //                f =>
        //                {
        //                    f.WithTopic("sensor");
        //                })
        //            .Build();

        //        await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

        //        Console.WriteLine("MQTT client subscribed to topic.");

        //    }
        //}

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

        public async Task UnsubscribeFromTopic(string topic)
        {
            var mqttSubscribeOptions = _mqttFactory.CreateUnsubscribeOptionsBuilder()
               .WithTopicFilter(topic) 
               .Build();
            _ = await this._mqttClient.UnsubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }
    }
}
