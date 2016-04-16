using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Domain.Commands
{
    public class CreateDatasets : IDefineCommand
    {
        public CreateDatasets(IObservable<Dataset> datasets)
        {
            Datasets = datasets;
        }

        public IObservable<Dataset> Datasets { get; }
    }

    public class HandleCreateDatasets : IHandleCommand<CreateDatasets>
    {
        private readonly IMapDataRecordToEntity<Dataset> _mapper;

        private readonly IExecuteRawSql _sql;


        public HandleCreateDatasets([NotNull] IExecuteRawSql sql, [NotNull] IMapDataRecordToEntity<Dataset> mapper)
        {
            if (sql == null)
                throw new ArgumentNullException(nameof(sql));
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            _sql = sql;
            _mapper = mapper;
        }

        public Task Handle(CreateDatasets command)
        {
            command.Datasets.Subscribe(async dataset =>
            {
                var parameters = new List<NpgsqlParameter>();

                if (!string.IsNullOrEmpty(dataset.Code))
                {
                    parameters.Add(_mapper.GetNpgsqlParameterByProperty(x => x.Code, dataset.Code));
                }

                if (!string.IsNullOrEmpty(dataset.DatabaseCode))
                {
                    parameters.Add(_mapper.GetNpgsqlParameterByProperty(x => x.DatabaseCode, dataset.DatabaseCode));
                }

                if (dataset.DatabaseId.HasValue)
                {
                    parameters.Add(_mapper.GetNpgsqlParameterByProperty(x => x.DatabaseId, dataset.DatabaseId));
                }

                if (!string.IsNullOrEmpty(dataset.Description))
                {
                    parameters.Add(_mapper.GetNpgsqlParameterByProperty(x => x.Description, dataset.Description));
                }

                if (dataset.EndDate.HasValue)
                {
                    parameters.Add(_mapper.GetNpgsqlParameterByProperty(x => x.EndDate, dataset.EndDate));
                }

                if (!string.IsNullOrEmpty(dataset.Frequency))
                {
                    parameters.Add(_mapper.GetNpgsqlParameterByProperty(x => x.Frequency, dataset.Frequency));
                }

                if (!string.IsNullOrEmpty(dataset.Name))
                {
                    parameters.Add(_mapper.GetNpgsqlParameterByProperty(x => x.Name, dataset.Name));
                }

                if (dataset.RefreshedAt.HasValue)
                {
                    parameters.Add(_mapper.GetNpgsqlParameterByProperty(x => x.RefreshedAt, dataset.RefreshedAt));
                }

                if (dataset.StartDate.HasValue)
                {
                    parameters.Add(_mapper.GetNpgsqlParameterByProperty(x => x.StartDate, dataset.StartDate));
                }

                if (dataset.Data != null)
                {
                    parameters.Add(_mapper.GetNpgsqlParameterByProperty(x => x.Data, (Newtonsoft.Json.Linq.JArray) dataset.Data));
                        // todo this seems so wrong
                }

                var insertStatement =
                    $"INSERT INTO {_mapper.GetTableName()} ({string.Join(",", parameters.Select(x => x.ParameterName))}) " +
                    $"VALUES ({string.Join(",", parameters.Select(x => $":{x.ParameterName}"))});";


                await _sql.ExecuteCommandAsync(insertStatement, parameters.ToArray());
            },
                exception => { throw new Exception(exception.Message); });

            return Task.FromResult(0);
        }
    }
}