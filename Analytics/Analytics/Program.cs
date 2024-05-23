using Analytics.Services;
using Newtonsoft.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MQTTService>();
builder.Services.AddSingleton<AnalyticsService>();

var app = builder.Build();


var mqttService=app.Services.GetRequiredService<MQTTService>();
var analyticsService=app.Services.GetRequiredService<AnalyticsService>();

mqttService.ConnectAsync().Wait();
//mqttService.AddTopicHandler("sensor", x => Console.WriteLine(JsonConvert.SerializeObject( JsonConvert.DeserializeObject<SensorMessage>(Encoding.UTF8.GetString(x)).Data)));
analyticsService.ReceiveDataAsync().Wait();
mqttService.Listen();
app.Run();
