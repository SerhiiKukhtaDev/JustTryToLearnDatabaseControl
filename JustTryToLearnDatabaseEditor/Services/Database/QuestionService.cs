using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JustTryToLearnDatabaseEditor.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JustTryToLearnDatabaseEditor.Services.Database
{
    public interface IQuestionService
    {
        Task InsertQuestion(Question newQuestion);
        Task UpdateQuestion(Question newQuestion);

        Task RemoveQuestion(Question question);
    }

    public class QuestionService : IQuestionService
    {
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly CancellationToken _token;


        public QuestionService(IMongoCollection<BsonDocument> collection, CancellationToken token)
        {
            _token = token;
            _collection = collection;
        }
        
        public async Task InsertQuestion(Question newQuestion)
        {
            await _collection.UpdateOneAsync(Builders<BsonDocument>.Filter.Eq("_id", newQuestion.Theme.Class.Subject.School.Id),
                Builders<BsonDocument>.Update.Push("Items.$[s].Items.$[c].Items.$[t].Items", newQuestion),
                new UpdateOptions
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("s._id", newQuestion.Theme.Class.Subject.Id)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("c._id", newQuestion.Theme.Class.Id)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("t._id", newQuestion.Theme.Id))
                    }
                }, _token);
        }
        
        public async Task UpdateQuestion(Question newQuestion)
        {
            await _collection.UpdateOneAsync(Builders<BsonDocument>.Filter.Eq("_id", newQuestion.Theme.Class.Subject.School.Id),
                Builders<BsonDocument>.Update.Set("Items.$[s].Items.$[c].Items.$[t].Items.$[q]", newQuestion),
                new UpdateOptions
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("s._id", newQuestion.Theme.Class.Subject.Id)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("c._id", newQuestion.Theme.Class.Id)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("t._id", newQuestion.Theme.Id)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("q._id", newQuestion.Id))
                    }
                }, _token);
        }
        
        public async Task RemoveQuestion(Question question)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", question.Theme.Class.Subject.School.Id);
            var update = Builders<BsonDocument>.Update.PullFilter("Items.$[s].Items.$[c].Items.$[t].Items", Builders<BsonDocument>.Filter.Eq("_id", question.Id));

            var updateOptions = new UpdateOptions
            {
                ArrayFilters = new List<ArrayFilterDefinition>
                {
                    new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("s._id", question.Theme.Class.Subject.Id)),
                    new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("c._id", question.Theme.Class.Id)),
                    new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("t._id", question.Theme.Id))
                }
            };
            
            await _collection.UpdateOneAsync(filter, update, updateOptions, _token);
        }
    }
}
