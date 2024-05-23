
using Analytics.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Text;

namespace Analytics.Services
{
    
    public class AnalyticsService : IAnalyticsService
    {
        private int movingAverageWindow=10;
        private float avgGlobalActivePower = 2.5f;
        private float avgGlobalReactivePower = 0.1f;
        private float avgVoltage = 240f;
        private float avgGlobalIntensity=10.5f;

        private readonly IConfiguration _configuration;
        private readonly IMQTTService _mqttService;

        public AnalyticsService(
            IConfiguration configuration,
            MQTTService mQTTService) 
        {
            this._configuration = configuration;
            this._mqttService = mQTTService;
        }
    
        private void UpdateAverages(Fields data)
        {
            this.avgGlobalActivePower -= this.avgGlobalActivePower / this.movingAverageWindow;
            this.avgGlobalActivePower += data.GlobalActivePower / this.movingAverageWindow;

            this.avgGlobalReactivePower -= this.avgGlobalReactivePower / this.movingAverageWindow;
            this.avgGlobalReactivePower += data.GlobalReactivePower / this.movingAverageWindow;

            this.avgVoltage -= this.avgVoltage / this.movingAverageWindow;
            this.avgVoltage += data.Voltage / this.movingAverageWindow;

            this.avgGlobalIntensity -= this.avgGlobalIntensity / this.movingAverageWindow;
            this.avgGlobalIntensity += data.GlobalIntensity / this.movingAverageWindow;
        }
        private void Rules(ref Dictionary<string,string> tempData,Fields data)
        {

            if (data.GlobalActivePower > this.avgGlobalActivePower * 1.1)
            {
                tempData["GlobalActivePower"] = "Warning sudden increase in global active power";
            }
            else if (data.GlobalActivePower < this.avgGlobalActivePower * 0.9)
            {
                tempData["GlobalActivePower"] = "Warning sudden decrease in global active power";
            }


            //if (data.GlobalReactivePower > this.avgGlobalReactivePower * 1.1)
            //{
            //    tempData["globalReactivePower"] = "Warning sudden increase in global reactive power";
            //}
            //else if (data.GlobalReactivePower < this.avgGlobalReactivePower * 0.9)
            //{
            //    tempData["globalReactivePower"] = "Warning sudden decrease in global reactive power";
            //}

            if (data.GlobalReactivePower > 0.2 * data.GlobalActivePower)
            {
                tempData["GlobalReactivePower"] = "Warning suspicious amount of reactive power detected";
            }


            if (data.Voltage > this.avgVoltage * 1.1)
            {
                tempData["Voltage"] = "Warning sudden increase in voltage";
            }
            else if (data.Voltage < this.avgVoltage * 0.9)
            {
                tempData["Voltage"] = "Warning sudden decrease in voltage";
            }


            if (data.GlobalIntensity > this.avgGlobalIntensity * 1.1)
            {
                tempData["GlobalIntensity"] = "Warning sudden increase in global intensity";
            }
            else if (data.GlobalIntensity < this.avgGlobalIntensity * 0.9)
            {
                tempData["GlobalIntensity"] = "Warning sudden decrease in global intensity";
            }


            if (data.SubMetering_1 > 0)
            {
                tempData["SubMetering_1"] = "Kitchen in use";
            }
            else
            {
                tempData["SubMetering_1"] = "Kitchen not in use";
            }

            if (data.SubMetering_2 > 0)
            {
                tempData["SubMetering_2"] = "Laundry room in use";
            }else
            {
                tempData["SubMetering_2"] = "Laundry room not in use";
            }

            if (data.SubMetering_3 > 0)
            {
                tempData["SubMetering_3"] = "Water heater in use";
            }
            else
            {
                tempData["SubMetering_3"] = "Water heater not in use";
            }
        }
        public async void Analysis(Fields? data)
        {
            if (data == null)
                return;
            
            UpdateAverages(data);

            Dictionary<string, string> tempData = new Dictionary<string, string>();
            //tempData["timestamp"] = data.Time.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz", CultureInfo.InvariantCulture);
            tempData["Timestamp"] = data.Time.ToString();

            Rules(ref tempData, data);


            string? toApiTopic = this._configuration.GetValue<string>("ToApiTopic");
            if (string.IsNullOrEmpty(toApiTopic))
                throw new Exception("Invalid Appsetings entry");

            Console.WriteLine(JsonConvert.SerializeObject(tempData));
            await this._mqttService.PublishMessageAsync(toApiTopic, JsonConvert.SerializeObject(tempData));
        }

        public async Task ReceiveDataAsync()
        {
            var topic = this._configuration.GetValue<string>("FromSensorTopic");
            if (topic == null)
            {
                throw new Exception("Invalid Appsettings entry");
            }
            this._mqttService.AddTopicHandler(topic,
                 e =>Analysis(JsonConvert.DeserializeObject<SensorMessage>(Encoding.UTF8.GetString(e))?.Data));
            await this._mqttService.SubscribeToTopicAsync(topic);
        }
    
    }
}
