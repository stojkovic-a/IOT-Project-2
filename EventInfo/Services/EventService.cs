
using EventInfo.DTOs;
using Newtonsoft.Json;
using System.Text;

namespace EventInfo.Services
{
    public class EventService : IEventService
    {

        private readonly IConfiguration _configuration;
        private readonly IMQTTService _mqttService;
        private Fields data= new Fields();
        private Events events=new Events();
        public EventService(
            IConfiguration configuration,
            MQTTService mQTTService)
        {
            this._mqttService = mQTTService;
            this._configuration = configuration;
        }

        public async Task ReceiveDataAsync()
        {
            var topic = this._configuration.GetValue<string>("ToApiTopic");
            
            if (topic == null)
            {
                throw new Exception("Invalid Appsettings entry");
            }
            Console.WriteLine(topic);
            this._mqttService.AddTopicHandler(topic,
                 e => MQttHandlerWrapper(JsonConvert.DeserializeObject<Fields>(Encoding.UTF8.GetString(e))));
            //e => MQttHandlerWrapper(Encoding.UTF8.GetString(e)));

            await this._mqttService.SubscribeToTopicAsync(topic);
        }

        public Fields GetLatestInfo()
        {
            return data;
        }

        public Events GetLastEvents()
        {
            return events;
        }

        private void MQttHandlerWrapper(Fields? data)
        {
            this.SetLatestInfo(data);
            this.SetLastEvent(data);
        }

        private void SetLatestInfo(Fields? data)
        {
            if (data == null)
                return;

            this.data = data;
        }

        private void SetLastEvent(Fields? data)
        {
            if (data== null)
                return;

            if (!string.IsNullOrEmpty(data.GlobalActivePower)) 
            {
                this.events.GlobalActivePowerEvent = data.GlobalActivePower;
                this.events.GlobalActivePowerTimestamp = data.Timestamp;
            }

            if (!string.IsNullOrEmpty(data.GlobalReactivePower))
            {
                this.events.GlobalReactivePowerEvent= data.GlobalReactivePower;
                this.events.GlobalReactivePowerTimestamp = data.Timestamp;
            }

            if (!string.IsNullOrEmpty(data.Voltage))
            {
                this.events.VoltageEvent = data.Voltage;
                this.events.VoltageTimestamp = data.Timestamp;
            }

            if (!string.IsNullOrEmpty(data.GlobalIntensity))
            {
                this.events.GlobalIntensityEvent = data.GlobalIntensity;
                this.events.GlobalIntensityTimestamp = data.Timestamp;
            }

            if (data.SubMetering_1 != events.SubMetering_1Event)
            {
                this.events.SubMetering_1Event = data.SubMetering_1;
                this.events.SubMetering_1Timestamp = data.Timestamp;
            }

            if (data.SubMetering_2 != events.SubMetering_2Event)
            {
                this.events.SubMetering_2Event = data.SubMetering_2;
                this.events.SubMetering_2Timestamp = data.Timestamp;
            }

            if (data.SubMetering_3 != events.SubMetering_3Event)
            {
                this.events.SubMetering_3Event = data.SubMetering_3;
                this.events.SubMetering_3Timestamp = data.Timestamp;
            }
        }
    }
}
