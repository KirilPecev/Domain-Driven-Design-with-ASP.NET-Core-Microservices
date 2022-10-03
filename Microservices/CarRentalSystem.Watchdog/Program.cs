WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints => endpoints
    .MapHealthChecksUI(healthChecks => healthChecks.UIPath = "/healthchecks"));

app.Run();
