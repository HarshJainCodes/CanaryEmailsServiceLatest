using CanaryEmailsService.Consumers;
using CanaryEmailsService.Contracts;
using CanaryEmailsService.Core;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.ApplicationInsights.Extensibility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string insKey = builder.Configuration["InstrumentationKey"];
builder.Services.AddApplicationInsightsTelemetry(insKey);

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<SendEmailConsumer>();

builder.Services.AddMassTransit(x =>
{
    x.UsingAzureServiceBus((context, config) =>
    {
        string azureServiceBusConnString = builder.Configuration["AzureBusEmail"];
        config.Host(azureServiceBusConnString);
        config.Message<ISendEmailMessage>(configurator => { });
        config.SubscriptionEndpoint<ISendEmailMessage>("scheduler_email_new", 
            d =>
            {
                d.Consumer<SendEmailConsumer>(context);
            }
        );
    });
});

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

app.Run();
