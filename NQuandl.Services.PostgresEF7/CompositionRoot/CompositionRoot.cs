﻿using NQuandl.Domain.Persistence.Api.Entities;
using NQuandl.Services.PostgresEF7.Models.ModelCreation;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace NQuandl.Services.PostgresEF7.CompositionRoot
{
    public static class CompositionRoot
    {
        public static void RegisterEntityFramework(this Container container)
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