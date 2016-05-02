using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Entities;
using NQuandl.Npgsql.Services.Transactions;

namespace NQuandl.Npgsql.Domain.Commands
{
    public class BulkCreateCountries : IDefineCommand
    {
        public BulkCreateCountries(IObservable<Country> countries)
        {
            Countries = countries;
        }

        public IObservable<Country> Countries { get; }
    }

    public class HandleBulkCreateCountries : IHandleCommand<BulkCreateCountries>
    {
        private readonly IWriteEntities<Country> _entites;


        public HandleBulkCreateCountries([NotNull] IWriteEntities<Country> entites)
        {
            if (entites == null)
                throw new ArgumentNullException(nameof(entites));
            _entites = entites;
        }

        public async Task Handle(BulkCreateCountries command)
        {
            await _entites.BulkWriteEntities(command.Countries);
        }
    }
}