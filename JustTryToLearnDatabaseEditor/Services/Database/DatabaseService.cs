using System;
using System.Security.Authentication;
using JustTryToLearnDatabaseEditor.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace JustTryToLearnDatabaseEditor.Services.Database
{
    public interface IDatabaseService
    {
        IMongoCollection<BsonDocument> GetCollection();
        IMongoDatabase GetDatabase();

        T DeserializeRoot<T>();
    }

    public class DatabaseService : IDatabaseService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<BsonDocument> _collection;

        public DatabaseService()
        {
            MongoClient client = new MongoClient(@"mongodb://Ram:6VmlQQx0ccXJho1p@cluster0-shard-00-00.zprr4.mongodb.net:27017,cluster0-shard-00-01.zprr4.mongodb.net:27017,cluster0-shard-00-02.zprr4.mongodb.net:27017/SubjectsDB?ssl=true&replicaSet=atlas-gquya2-shard-0&authSource=admin&retryWrites=true&w=majority
");

            _database = client.GetDatabase("SubjectsDB");
            _collection = _database.GetCollection<BsonDocument>("SubjectsCollection");
        }

        public IMongoCollection<BsonDocument> GetCollection()
        {
            return _collection;
        }

        public IMongoDatabase GetDatabase()
        {
            return _database;
        }

        public T DeserializeRoot<T>()
        {
            var firstDocument = GetCollection().Find(new BsonDocument()).FirstOrDefault();
            T elem = BsonSerializer.Deserialize<T>(firstDocument);

            return elem;
        }
    }
}
