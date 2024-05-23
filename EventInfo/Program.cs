using EventInfo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MQTTService>();
builder.Services.AddSingleton<EventService>();



var app = builder.Build();

var mqttService = app.Services.GetRequiredService<MQTTService>();
var eventService= app.Services.GetRequiredService<EventService>();
mqttService.ConnectAsync().Wait();
eventService.ReceiveDataAsync().Wait();
mqttService.Listen();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
