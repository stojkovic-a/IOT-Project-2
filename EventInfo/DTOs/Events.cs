namespace EventInfo.DTOs
{
    public class Events
    {
        public DateTime GlobalActivePowerTimestamp{ get; set; }
        public string GlobalActivePowerEvent { get; set; } = string.Empty;
        public DateTime GlobalReactivePowerTimestamp { get; set; }
        public string GlobalReactivePowerEvent { get; set; } = string.Empty;
        public DateTime VoltageTimestamp { get; set; }
        public string VoltageEvent { get; set; } = string.Empty;
        public DateTime GlobalIntensityTimestamp { get; set; }
        public string GlobalIntensityEvent { get;set; } = string.Empty;
        public DateTime SubMetering_1Timestamp {  get; set; } 
        public string SubMetering_1Event { get;set; } = string.Empty;
        public DateTime SubMetering_2Timestamp { get; set; }
        public string SubMetering_2Event { get; set; } = string.Empty;
        public DateTime SubMetering_3Timestamp { get; set; }
        public string SubMetering_3Event { get; set; } = string.Empty;

    }
}
