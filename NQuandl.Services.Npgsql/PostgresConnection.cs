using NQuandl.Services.Npgsql.Api;

namespace NQuandl.Services.Npgsql
{
    internal sealed class PostgresConnection : IConfigureConnection
    {
        public string ConnectionString => "Host=192.168.43.191;" +
                                          "Username=postgres;" +
                                          "Password=postgres;" +
                                          "Database=nquandl;" +
                                          "MINPOOLSIZE=1;" +
                                          "MAXPOOLSIZE=40;" +
                                          "Connection Lifetime=0;";
    }
}