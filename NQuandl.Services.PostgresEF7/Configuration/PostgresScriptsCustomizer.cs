using System;
using JetBrains.Annotations;
using Microsoft.Data.Entity;

namespace NQuandl.Services.PostgresEF7.Configuration
{
    [UsedImplicitly]
    public class PostgresScriptsCustomizer : ICustomizeDb
    {
        public void Customize(DbContext dbContext)
        {
            throw new NotImplementedException();
        }
    }
}