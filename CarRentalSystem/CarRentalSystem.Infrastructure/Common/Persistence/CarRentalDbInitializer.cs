namespace CarRentalSystem.Infrastructure.Common.Persistence
{
    using System.Reflection;

    using Common;

    using Domain.Common;

    using Identity;
    using Identity.Seeders;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    internal class CarRentalDbInitializer : IInitializer
    {
        private readonly CarRentalDbContext db;
        private readonly IEnumerable<IInitialData> initialDataProviders;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public CarRentalDbInitializer(
           CarRentalDbContext db,
           IEnumerable<IInitialData> initialDataProviders,
           UserManager<User> userManager,
           RoleManager<Role> roleManager)
        {
            this.db = db;
            this.initialDataProviders = initialDataProviders;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void Initialize()
        {
            db.Database.Migrate();

            foreach (IInitialData initialDataProvider in initialDataProviders)
            {
                if (DataSetIsEmpty(initialDataProvider.EntityType))
                {
                    IEnumerable<object> data = initialDataProvider.GetData();

                    foreach (var entity in data)
                    {
                        db.Add(entity);
                    }
                }
            }

            IdentityUserSeeder.SeedData(userManager, roleManager);

            db.SaveChanges();
        }

        private bool DataSetIsEmpty(Type type)
        {
            MethodInfo setMethod = GetType()
                .GetMethod(nameof(this.GetSet), BindingFlags.Instance | BindingFlags.NonPublic)!
                .MakeGenericMethod(type);

            object? set = setMethod.Invoke(this, Array.Empty<object>());

            MethodInfo countMethod = typeof(Queryable)
                .GetMethods()
                .First(m => m.Name == nameof(Queryable.Count) && m.GetParameters().Length == 1)
                .MakeGenericMethod(type);

            int result = (int)countMethod.Invoke(null, new[] { set })!;

            return result == 0;
        }

        private DbSet<TEntity> GetSet<TEntity>()
            where TEntity : class
            => db.Set<TEntity>();
    }
}
