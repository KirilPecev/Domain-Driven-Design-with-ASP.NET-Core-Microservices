using CarRentalSystem.Admin.Infrastructure;
using CarRentalSystem.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

// Add services to the container.
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseJwtCookieAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints
        .MapHealthChecks()
        .MapDefaultControllerRoute());

app.Run();
