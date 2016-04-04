using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Persistence.Entities;
using NQuandl.Api.Persistence.Transactions;
using NQuandl.Domain.Persistence.Entities;

namespace NQuandl.Domain.Persistence.Commands
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