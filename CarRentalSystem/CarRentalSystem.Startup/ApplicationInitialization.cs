namespace CarRentalSystem.Startup
{
    using Infrastructure;

    public static class ApplicationInitialization
    {
        public static IApplicationBuilder Initialize(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            IEnumerable<IInitializer> initializers = serviceScope.ServiceProvider.GetServices<IInitializer>();

            foreach (var initializer in initializers)
            {
                initializer.Initialize();
            }

            return app;
        }
    }
}
