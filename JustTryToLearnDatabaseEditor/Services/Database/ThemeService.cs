using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JustTryToLearnDatabaseEditor.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JustTryToLearnDatabaseEditor.Services.Database
{
    public interface IThemeService
    {
        Task InsertTheme(Theme newTheme);
        Task UpdateTheme(Theme newTheme);

        Task RemoveTheme(Theme theme);
    }

    public class ThemeService : IThemeService
    {
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly CancellationToken _token;


        public ThemeService(IMongoCollection<BsonDocument> collection, CancellationToken token)
        {
            _token = token;
            _collection = collection;
        }
        
        public async Task InsertTheme(Theme newTheme)
        {
            await _collection.UpdateOneAsync(Builders<BsonDocument>.Filter.Eq("_id", newTheme.Class.Subject.School.Id),
                Builders<BsonDocument>.Update.Push("Items.$[g].Items.$[c].Items", newTheme),
                new UpdateOptions
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("g._id", newTheme.Class.Subject.Id)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("c._id", newTheme.Class.Id))
                    }
                }, _token);
        }
        
        public async Task UpdateTheme(Theme newTheme)
        {
            await _collection.UpdateOneAsync(Builders<BsonDocument>.Filter.Eq("_id", newTheme.Class.Subject.School.Id),
                Builders<BsonDocument>.Update.Set("Items.$[s].Items.$[c].Items.$[t].Name", newTheme.Name),
                new UpdateOptions
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("s._id", newTheme.Class.Subject.Id)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("c._id", newTheme.Class.Id)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("t._id", newTheme.Id))
                    }
                }, _token);
            
        }
        
        public async Task RemoveTheme(Theme theme)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", theme.Class.Subject.School.Id);
            var update = Builders<BsonDocument>.Update.PullFilter("Items.$[s].Items.$[c].Items", Builders<BsonDocument>.Filter.Eq("_id", theme.Id));
            
            var updateOptions = new UpdateOptions
            {
                ArrayFilters = new List<ArrayFilterDefinition>
                {
                    new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("s._id", theme.Class.Subject.Id)),
                    new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("c._id", theme.Class.Id))
                }
            };
            
            await _collection.UpdateOneAsync(filter, update, updateOptions, _token);
        }
    }
}
