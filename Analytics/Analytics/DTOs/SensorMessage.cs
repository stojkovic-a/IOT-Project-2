using Analytics.DTOs;

namespace Analytics.DTOs
{
    public class SensorMessage
    {
    }
}
public class SensorMessage
{
    public string Pattern { get; set; } = string.Empty;

    public Fields Data { get; set; } = null!;
}