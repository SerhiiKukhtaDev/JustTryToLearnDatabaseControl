using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace DatabaseTest
{
    internal class Program
    {
        private static IMongoDatabase _mongoDb;
        private static IMongoCollection<BsonDocument> school;

        public static void Main(string[] args)
        {
            MongoClient client = new MongoClient(@"mongodb://Ram:6VmlQQx0ccXJho1p@cluster0-shard-00-00.zprr4.mongodb.net:27017,cluster0-shard-00-01.zprr4.mongodb.net:27017,cluster0-shard-00-02.zprr4.mongodb.net:27017/SubjectsDB?ssl=true&replicaSet=atlas-gquya2-shard-0&authSource=admin&retryWrites=true&w=majority
");
            _mongoDb = client.GetDatabase("SubjectsDB");

            school = _mongoDb.GetCollection<BsonDocument>("SubjectsCollection");

            //School school = new School() {Name = "School"};
            /*Subject subject = new Subject {Name = "Subject 1"};
            Class someClass = new Class {Name = "Class 1"};
            Theme theme = new Theme {Name = "Theme 1"};
            Question question = new Question() {Name = "Question 1"};
            
            question.AddItem(new Answer() {IsRightAnswer = false, Text = "Answer 1"});
            question.AddItem(new Answer() {IsRightAnswer = false, Text = "Answer 2"});
            question.AddItem(new Answer() {IsRightAnswer = false, Text = "Answer 3"});
            question.AddItem(new Answer() {IsRightAnswer = true, Text = "Answer 4"});
            
            theme.AddItem(question);
            someClass.AddItem(theme);
            subject.AddItem(someClass);
            school.AddItem(subject);#1#*/
            School school1 = Deserialize();
            UpdateQuestion(new Question(){Name = "Changed"}, school1.Id, school1.Items[4].Id, school1.Items[4].Items[1].Id, school1.Items[4].Items[1].Items[0].Id, school1.Items[4].Items[1].Items[0].Items[0].Id);
        }

        #region Subjects

        public static void InsertSubject(Subject subject, ObjectId id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var update = Builders<BsonDocument>.Update.Push("Items", subject);
 
            school.UpdateOne(filter, update);
        }
        
        public static void UpdateSubject(ObjectId id1, ObjectId id2, Subject newSubject)
        {
            var watcher = Stopwatch.StartNew();
            
            school.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id1),
                Builders<BsonDocument>.Update.Set("Items.$[g].Name", newSubject.Name),
                new UpdateOptions
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("g._id", id2))
                    }
                });
            
            Console.WriteLine(watcher.Elapsed);
        }
        

        #endregion

        #region Classes

        public static void InsertClass(Class newClass, ObjectId id1, ObjectId id2)
        {
            var watcher = Stopwatch.StartNew();
            
            school.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id1),
                Builders<BsonDocument>.Update.Push("Items.$[g].Items", newClass),
                new UpdateOptions
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("g._id", id2))
                    }
                });
            
            Console.WriteLine(watcher.Elapsed);
        }

        public static void UpdateClass(Class newClass, ObjectId id1, ObjectId id2, ObjectId id3)
        {
            var watcher = Stopwatch.StartNew();
            
            school.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id1),
                Builders<BsonDocument>.Update.Set("Items.$[g].Items.$[c].Name", newClass.Name),
                new UpdateOptions
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("g._id", id2)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("c._id", id3))
                    }
                });
            
            Console.WriteLine(watcher.Elapsed);
        }

        #endregion

        #region Themes

        public static void InsertTheme(Theme newTheme, ObjectId id1, ObjectId id2, ObjectId id3)
        {
            var watcher = Stopwatch.StartNew();
            
            school.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id1),
                Builders<BsonDocument>.Update.Push("Items.$[g].Items.$[c].Items", newTheme),
                new UpdateOptions
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("g._id", id2)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("c._id", id3))
                    }
                });
            
            Console.WriteLine(watcher.Elapsed);
        }
        
        public static void UpdateTheme(Theme newTheme, ObjectId id1, ObjectId id2, ObjectId id3, ObjectId id4)
        {
            var watcher = Stopwatch.StartNew();
            
            school.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id1),
                Builders<BsonDocument>.Update.Set("Items.$[s].Items.$[c].Items.$[t].Name", newTheme.Name),
                new UpdateOptions
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("s._id", id2)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("c._id", id3)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("t._id", id4))
                    }
                });
            
            Console.WriteLine(watcher.Elapsed);
        }

        #endregion

        #region Questions

        public static void InsertQuestion(Question newQuestion, ObjectId id1, ObjectId id2, ObjectId id3, ObjectId id4)
        {
            var watcher = Stopwatch.StartNew();
            
            school.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id1),
                Builders<BsonDocument>.Update.Push("Items.$[s].Items.$[c].Items.$[t].Items", newQuestion),
                new UpdateOptions
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("s._id", id2)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("c._id", id3)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("t._id", id4))
                    }
                });
            
            Console.WriteLine(watcher.Elapsed);
        }
        
        public static void UpdateQuestion(Question newQuestion, ObjectId id1, ObjectId id2, ObjectId id3, ObjectId id4, ObjectId id5)
        {
            var watcher = Stopwatch.StartNew();
            
            school.UpdateOne(Builders<BsonDocument>.Filter.Eq("_id", id1),
                Builders<BsonDocument>.Update.Set("Items.$[s].Items.$[c].Items.$[t].Items.$[q]", newQuestion),
                new UpdateOptions
                {
                    ArrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("s._id", id2)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("c._id", id3)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("t._id", id4)),
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("q._id", id5))
                    }
                });
            
            Console.WriteLine(watcher.Elapsed);
        }

        #endregion

        public static School Deserialize()
        {
            var firstDocument = Program.school.Find(new BsonDocument()).FirstOrDefault();
            School school = BsonSerializer.Deserialize<School>(firstDocument);

            return school;
        }

        public abstract class Model<T>
        {
            [BsonId(IdGenerator = typeof(GuidGenerator))]
            public ObjectId Id { get; set; }
            
            public string Name { get; set; }

            public ObservableCollection<T> Items { get; set; }

            public void AddItem(T item)
            {
                Items.Add(item);
            }

            public void RemoveItem(T item)
            {
                Items.Remove(item);
            }

            public Model()
            {
                Items = new ObservableCollection<T>();
                Id = ObjectId.GenerateNewId();
            }
        }

        public sealed class School : Model<Subject>
        {
            
        }
        
        public sealed class Subject : Model<Class>
        {
            
        }
        
        public sealed class Class : Model<Theme>
        {
            
        }

        public sealed class Theme : Model<Question>
        {
            
        }

        public sealed class Question : Model<Answer>
        {

        }

        public sealed class Answer
        {
            public string Text { get; set; }
            public bool IsRightAnswer { get; set; }
        }
    }
}
