﻿using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Services.Database;
using NQuandl.Npgsql.Services.Database.Configuration;
using NQuandl.Npgsql.Services.Database.Customization;
using NQuandl.Npgsql.Services.Database.Initialization;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Npgsql.SimpleInjector.Database
{
    public class Package : IPackage
    {
        public Package(bool isGreenfield = false, bool useDebugDatabase = true)
        {
            IsGreenfield = isGreenfield;
            UseDebugDatabase = useDebugDatabase;
        }

        private bool IsGreenfield { get; }
        private bool UseDebugDatabase { get; }

        public void RegisterServices(Container container)
        {
            if (UseDebugDatabase)
            {
                container.Register<IConfigureConnection>(() => new DebugConnectionConfiguration());
            }
            else
            {
                container.Register<IConfigureConnection>(() => new ConnectionConfiguration());
            }

            if (IsGreenfield)
            {
                container.Register<ICustomizeDb, PostgresSqlScriptsCustomizer>();
                container.Register<IDbInitializer, GreenfieldDbInitializer>();
            }
            else
            {
                container.Register<ICustomizeDb, VanillaDbCustomizer>();
                container.Register<IDbInitializer, BrownfieldDbInitializer>();
            }
            container.Register<IProvideDbConnection, DbConnectionProvider>();
            container.Register<IDbContext, DbContext>(Lifestyle.Transient);
        }
    }
}