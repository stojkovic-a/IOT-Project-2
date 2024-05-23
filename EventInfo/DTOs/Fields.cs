namespace EventInfo.DTOs
{
    public class Fields
    {
        public string GlobalActivePower { get; set; } = string.Empty;
        public string GlobalReactivePower { get; set; } = string.Empty;
        public string Voltage { get; set; } = string.Empty;
        public string GlobalIntensity { get; set; } = string.Empty;
        public string SubMetering_1 { get; set; } = string.Empty;
        public string SubMetering_2 { get; set; } = string.Empty;
        public string SubMetering_3 { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }

    }
}
