using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Persistence.Entities;
using NQuandl.Api.Persistence.Transactions;
using NQuandl.Domain.Persistence.Entities;

namespace NQuandl.Domain.Persistence.Commands
{
    public class CreateRawResponse : BaseCreateEntityCommand<RawResponse>, IDefineCommand
    {
        public string Uri { get; set; }
        public string Content { get; set; }
    }

    public class HandleCreateRawResponse : IHandleCommand<CreateRawResponse>
    {
        private readonly IWriteEntities _entities;

        public HandleCreateRawResponse([NotNull] IWriteEntities entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            _entities = entities;
        }

        public async Task Handle(CreateRawResponse command)
        {
            var rawResponse = new RawResponse
            {
                RequestUri = command.Uri,
                ResponseContent = command.Content
            };

            _entities.Create(rawResponse);
            await _entities.SaveChangesAsync();
           
        }
    }
}