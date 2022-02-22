using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JustTryToLearnDatabaseEditor.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JustTryToLearnDatabaseEditor.Services.Database
{
    public interface ISubjectService
    {
        Task InsertSubjectAsync(Subject subject);
        Task UpdateSubject(Subject subject);

        Task RemoveSubject(Subject subject);
    }

    public class SubjectService : ISubjectService
    {
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly CancellationToken _token;


        public SubjectService(IMongoCollection<BsonDocument> collection, CancellationToken token)
        {
            _token = token;
            _collection = collection;
        }
        
        public async Task InsertSubjectAsync(Subject subject)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", subject.School.Id);
            var update = Builders<BsonDocument>.Update.Push("Items", subject);
 
            await _collection.UpdateOneAsync(filter, update, cancellationToken: _token);
        }
        
        public async Task UpdateSubject(Subject subject)
        {
            await _collection.UpdateOneAsync(
                Builders<BsonDocument>.Filter.Eq("_id", subject.School.Id),
                Builders<BsonDocument>.Update.Set("Items.$[g].Name", subject.Name),
                new UpdateOptions
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("g._id", subject.Id))
                    }
                }, _token);
        }

        public async Task RemoveSubject(Subject subject)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", subject.School.Id);
            var update = Builders<BsonDocument>.Update.PullFilter("Items", Builders<BsonDocument>.Filter.Eq("_id", subject.Id));
            await _collection.UpdateOneAsync(filter, update, cancellationToken: _token);
        }
    }
}
