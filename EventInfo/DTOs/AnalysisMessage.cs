namespace EventInfo.DTOs
{
    public class AnalysisMessage
    {
        public string Pattern { get; set; } = string.Empty;

        public Fields Data { get; set; } = null!;
    }
}
