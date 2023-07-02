using System.Text;
using API.Applications.Services;
using API.Applications.Services.RabbitMqs;
using API.Presentations.Extensions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationService();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var receiveHelloService = serviceProvider.GetRequiredService<ReceiveHelloService>();
    receiveHelloService.Register();
}

app.Run();
