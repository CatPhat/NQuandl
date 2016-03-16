﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Api
{
    public interface IMapCsvStream
    {
        Task<IEnumerable<DatabaseDatasetCsvRow>> MapToDataset(StreamReader stream);
    }
}