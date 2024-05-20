using Analytics.Services;
using Newtonsoft.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MQTTService>();
builder.Services.AddSingleton<AnalyticsService>();

var app = builder.Build();


var mqttService=app.Services.GetRequiredService<MQTTService>();
var analyticsService=app.Services.GetRequiredService<AnalyticsService>();

mqttService.ConnectAsync().Wait();
//mqttService.AddTopicHandler("sensor", x => Console.WriteLine(JsonConvert.SerializeObject( JsonConvert.DeserializeObject<SensorMessage>(Encoding.UTF8.GetString(x)).Data)));
//mqttService.Listen();
//mqttService.SubscribeToTopicAsync("sensor").Wait();

analyticsService.ReceiveDataAsync().Wait();
mqttService.Listen();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();
