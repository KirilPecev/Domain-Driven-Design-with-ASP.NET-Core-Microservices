using CarRentalSystem.Identity.Infrastructure;
using CarRentalSystem.Infrastructure;
using CarRentalSystem.Notifications;
using CarRentalSystem.Notifications.Hub;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddServices(builder.Configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

string allowedOrigins = builder.Configuration
    .GetSection(nameof(NotificationSettings))
    .GetValue<string>(nameof(NotificationSettings.AllowedOrigins));

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(opt => opt
    .WithOrigins(allowedOrigins)
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());

app.UseAuthentication();

app.UseAuthorization();

app.MapHealthChecks();

app.MapHub<NotificationsHub>("/notifications");

app.Run();
