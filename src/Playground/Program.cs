using Playground.RedisChannelMessageHandlers;
using Playground.Services;
using StackExchange.Redis;
using TraTech.Redis.Core.Cache;
using TraTech.Redis.Core.MessageHub;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRedisCacheService((options) =>
{
    options.ExpireTime = TimeSpan.FromDays(1);
    options.ConnectionMultiplexer = ConnectionMultiplexer.Connect(builder.Configuration["RedisConfiguration:Address"]);
});

builder.Services.AddRedisMessageHub("localhost")
    .AddChannelMessageHandler<DenemeMessageHandler>("deneme");

builder.Services.AddScoped<Service1>();
builder.Services.AddTransient<Service2>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.UseRedisMessageHub();
app.Run();