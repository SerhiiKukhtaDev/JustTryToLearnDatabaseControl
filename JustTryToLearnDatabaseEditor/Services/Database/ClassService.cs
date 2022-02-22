using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JustTryToLearnDatabaseEditor.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JustTryToLearnDatabaseEditor.Services.Database
{
    public interface IClassService
    {
        Task InsertClass(Class newClass);
        Task UpdateClass(Class newClass);
        Task RemoveClass(Class classToDelete);
    }

    public class ClassService : IClassService
    {
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly CancellationToken _token;


        public ClassService(IMongoCollection<BsonDocument> collection, CancellationToken token)
        {
            _token = token;
            _collection = collection;
        }
        
        public async Task InsertClass(Class newClass)
        {
            await _collection.UpdateOneAsync(Builders<BsonDocument>.Filter.Eq("_id", newClass.Subject.School.Id),
                Builders<BsonDocument>.Update.Push("Items.$[g].Items", newClass),
                new UpdateOptions
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("g._id", newClass.Subject.Id))
                    }
                }, _token);

            var thread = Thread.CurrentThread.ManagedThreadId;
        }
        
        public async Task UpdateClass(Class newClass)
        {
            await _collection.UpdateOneAsync(Builders<BsonDocument>.Filter.Eq("_id", newClass.Subject.School.Id),
                Builders<BsonDocument>.Update.Set("Items.$[g].Items.$[c].Name", newClass.Name),
                new UpdateOptions
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("g._id", newClass.Subject.Id)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("c._id", newClass.Id))
                    }
                }, _token);
        }
        
        public async Task RemoveClass(Class classToDelete)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", classToDelete.Subject.School.Id);
            var update = Builders<BsonDocument>.Update.PullFilter("Items.$[s].Items", Builders<BsonDocument>.Filter.Eq("_id", classToDelete.Id));
            
            var updateOptions = new UpdateOptions
            {
                ArrayFilters = new List<ArrayFilterDefinition>
                {
                    new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("s._id", classToDelete.Subject.Id))
                }
            };
            
            await _collection.UpdateOneAsync(filter, update, updateOptions, _token);
        }
    }
}
