
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting.Server;

namespace Analytics.Services
{
    public class eKuiperService : IeKuiperService
    {
        private readonly IConfiguration _configuration;
        private readonly IMQTTService _mqttService;

        public eKuiperService(
            IConfiguration configuration,
            MQTTService mQTTService)
        {
            this._configuration = configuration;
            this._mqttService = mQTTService;
        }


        public async Task PrepareDataAsync()
        {
            var topic = this._configuration.GetValue<string>("FromSensorTopic");
            if (topic == null)
            {
                throw new Exception("Invalid Appsettings entry");
            }
            this._mqttService.AddTopicHandler(topic,
                async e =>
                await SendDataToKuiperAsync(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<SensorMessage>(Encoding.UTF8.GetString(e)).Data)));
            await this._mqttService.SubscribeToTopicAsync(topic);
        }

        private async Task SendDataToKuiperAsync(string serializedData)
        {
            var topic = this._configuration.GetValue<string>("ToEkuiperTopic");
            if (topic == null)
            {
                throw new Exception("Invalid Appsettings entry");
            }
            await this._mqttService.PublishMessageAsync(topic, serializedData);
        }

        public async Task RecieveDataFromKuiperAsync()
        {
            var topic = this._configuration.GetValue<string>("FromEkuiperTopic");
            if (topic == null)
            {
                throw new Exception("Invalid Appsettings entry");
            }
            await this._mqttService.SubscribeToTopicAsync(topic);
            this._mqttService.AddTopicHandler(topic, e => Console.WriteLine(Encoding.UTF8.GetString(e)));

        }


        public async Task InitializeKuiperAsync()
        {
            try
            {   
                
                var topic = this._configuration.GetValue<string>("ToEkuiperTopic");
                var server = this._configuration.GetValue<string>("eKuiperServer");
                var client = new HttpClient();
                //var response = await client.DeleteAsync(server + "/streams/sensor_stream");
                var sqlStatement = $"create stream sensor_stream (Measurement string, Time datetime, GlobalActivePower float, GlobalIntensity float , lobalReactivePower float , SubMetering_1 float , SubMetering_2 float , SubMetering_3 float , Voltage float ) WITH ( datasource = \"topic/eKuiper:To\", FORMAT = \"json\", KEY = \"Time\")";
                var requestJson = new { sql = sqlStatement };
                var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestJson), Encoding.UTF8, "application/json");
                //var content = new FormUrlEncodedContent([streamInitValues]);

                //Console.WriteLine(content.Headers);
                var response = await client.PostAsync(server + "/streams", jsonContent);

                Console.WriteLine(await response.Content.ReadAsStringAsync());

                var r = await client.GetAsync(server + "/streams");
                Console.WriteLine(await r.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }


    }
}
