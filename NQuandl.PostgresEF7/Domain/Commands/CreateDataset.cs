using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.PostgresEF7.Api.Entities;
using NQuandl.PostgresEF7.Api.Transactions;
using NQuandl.PostgresEF7.Domain.Entities;

namespace NQuandl.PostgresEF7.Domain.Commands
{
    public class CreateDataset : BaseEntityCommand, IDefineCommand
    {
        public CreateDataset([NotNull] Dataset dataset)
        {
            if (dataset == null)
                throw new ArgumentNullException(nameof(dataset));
            Dataset = dataset;
        }

        public Dataset Dataset { get; set; }
    }

    public class HandleCreateDataset : IHandleCommand<CreateDataset>
    {
        private readonly IWriteEntities _entities;

        public HandleCreateDataset([NotNull] IWriteEntities entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            _entities = entities;
        }

        public async Task Handle(CreateDataset command)
        {
            _entities.Create(command.Dataset);
            await _entities.SaveChangesAsync();
        }
    }
}