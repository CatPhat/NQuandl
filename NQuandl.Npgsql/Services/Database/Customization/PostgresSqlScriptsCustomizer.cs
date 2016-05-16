using System.Linq;
using System.Reflection;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Services.Database.Customization
{
    public class PostgresSqlScriptsCustomizer : ICustomizeDb
    {
        public void Customize(IDbContext dbContext)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var manifests = assembly.GetManifestResourceNames();
            var sqlScriptNames = manifests.Where(x => x.StartsWith("NQuandl.Npgsql.Services.Database.Customization.PostgresSqlScripts.") && x.EndsWith(".sql"));
            foreach (var sqlScriptName in sqlScriptNames)
            {
                var sqlScriptText = assembly.GetManifestResourceText(sqlScriptName);
                dbContext.ExecuteSqlCommand(sqlScriptText);
            }
        }
    }
}