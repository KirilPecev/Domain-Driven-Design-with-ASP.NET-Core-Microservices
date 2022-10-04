using CarRentalSystem.Infrastructure;
using CarRentalSystem.Statistics.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddServices(builder.Configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.UseWebService(app.Environment);
app.Initialize();

app.Run();
