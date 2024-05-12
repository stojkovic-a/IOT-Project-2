using System.Diagnostics.Metrics;

namespace Analytics.DTOs
{
    public class Fields
    {
        public string Measurement { get; set; } = string.Empty;
        public DateTime Time {  get; set; }
        public float GlobalActivePower {  get; set; }
        public float GlobalIntensity { get; set; }

        public float GlobalReactivePower {  get; set; }
        public float SubMetering_1 { get; set; }
        public float SubMetering_2 { get;set; }
        public float SubMetering_3 { get; set; }
        public float Voltage { get; set; }


    }
}
