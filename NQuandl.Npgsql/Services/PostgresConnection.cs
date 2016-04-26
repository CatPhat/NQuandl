using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services
{
    public sealed class PostgresConnection : IConfigureConnection
    {
        public string ConnectionString => "Host=192.168.1.3;" +
                                          "Username=postgres;" +
                                          "Password=postgres;" +
                                          "Database=nquandl;" +
                                          "MINPOOLSIZE=1;" +
                                          "MAXPOOLSIZE=40;" +
                                          "Connection Lifetime=0;" +
                                          "COMMANDTIMEOUT=5000;";
    }
}