using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services.Database.Configuration
{
    public sealed class DebugConnectionConfiguration : IConfigureConnection
    {
        public string ConnectionString => "Host=10.0.0.78;" +
                                          "Username=postgres;" +
                                          "Password=postgres;" +
                                          "Database=debug_nquandl;" +
                                          "MINPOOLSIZE=1;" +
                                          "MAXPOOLSIZE=40;" +
                                          "COMMANDTIMEOUT=5000;";
    }
}