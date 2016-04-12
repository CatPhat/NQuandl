using NQuandl.PostgresEF7.Api.Entities;
using NQuandl.PostgresEF7.Services;
using NQuandl.PostgresEF7.Services.Models.ModelCreation;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Packaging;

namespace NQuandl.PostgresEF7.SimpleInjector.EntityFramework
{
    public static class CompositionRoot
    {
        public static void RegisterEntityFramework(this Container container) {}
    }

    public class Package : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ICreateDbModel, DefaultDbModelCreator>();

            //container.Register<EntityDbContext, EntityDbContext>();

            var lifestyle = new ExecutionContextScopeLifestyle();
            var contextRegistration = lifestyle.CreateRegistration<EntityDbContext>(container);
            //container.AddRegistration(typeof (EntityDbContext), contextRegistration);
            container.AddRegistration(typeof (IWriteEntities), contextRegistration);
            container.AddRegistration(typeof (IReadEntities), contextRegistration);

            //container.Options.DefaultScopedLifestyle = lifestyle;
        }
    }
}