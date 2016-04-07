using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Domain.Persistence.Api.Entities;
using NQuandl.Domain.Persistence.Api.Transactions;
using NQuandl.Domain.Persistence.Domain.Entities;
using NQuandl.Domain.Quandl.Requests;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Domain.Persistence.Domain.Commands
{
    public class UpdateRawResponses : IDefineCommand
    {
    }

    public class HandleUpdateRawResponses : IHandleCommand<UpdateRawResponses>
    {
        private readonly IWriteEntities _entities;

        public HandleUpdateRawResponses([NotNull] IWriteEntities entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            _entities = entities;
        }

        public async Task Handle(UpdateRawResponses command)
        {
            var entitiesToUpdate =
                _entities.Query<RawResponse>().Where(x => x.RequestUri == "UnknownDataset").Select(y => y.Id).ToList();

            foreach (var id in entitiesToUpdate)
            {
                var entities = _entities.Get<RawResponse>().FirstOrDefault(x => x.Id == id);
                var entityToUpdate = entities;
                if (entityToUpdate == null)
                    return;


                var deserializedResponse =
                    entityToUpdate.ResponseContent.DeserializeToEntity<JsonResultDatasetDataAndMetadata>();
                if (deserializedResponse == null)
                    return;

                if (deserializedResponse.DataAndMetadata == null)
                    return;

                if (string.IsNullOrEmpty(deserializedResponse.DataAndMetadata.DatabaseCode) ||
                    string.IsNullOrEmpty(deserializedResponse.DataAndMetadata.DatasetCode))
                    return;


                var request = new RequestDatasetDataAndMetadataBy(deserializedResponse.DataAndMetadata.DatabaseCode,
                    deserializedResponse.DataAndMetadata.DatasetCode);
                entityToUpdate.RequestUri = request.ToUri();
                // _entities.Update(entityToUpdate);
                await _entities.SaveChangesAsync();
            }
        }
    }
}