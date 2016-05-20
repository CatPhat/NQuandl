using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services.Database.Configuration
{
    public sealed class DebugConnectionConfiguration : IConfigureConnection
    {
        public string ConnectionString => "Host=10.177.187.196;" +
                                          "Username=postgres;" +
                                          "Password=postgres;" +
                                          "Database=debug_nquandl;" +
                                          "MINPOOLSIZE=1;" +
                                          "MAXPOOLSIZE=40;" +
                                          "Connection Lifetime=0;" +
                                          "COMMANDTIMEOUT=5000;";
    }
}