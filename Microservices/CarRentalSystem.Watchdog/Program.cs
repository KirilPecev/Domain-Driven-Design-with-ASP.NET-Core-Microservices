WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();

app.UseHealthChecksUI(config => config.UIPath = "/healthchecks");

app.Run();
