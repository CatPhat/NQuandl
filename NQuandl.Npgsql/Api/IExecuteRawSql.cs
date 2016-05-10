using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Npgsql;
using NQuandl.Npgsql.Api.DTO;


namespace NQuandl.Npgsql.Api
{
    public interface IDb
    {
        NpgsqlConnection CreateConnection();
    }
}