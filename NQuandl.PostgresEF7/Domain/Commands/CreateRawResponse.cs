using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.PostgresEF7.Api.Entities;
using NQuandl.PostgresEF7.Api.Transactions;
using NQuandl.PostgresEF7.Domain.Entities;

namespace NQuandl.PostgresEF7.Domain.Commands
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