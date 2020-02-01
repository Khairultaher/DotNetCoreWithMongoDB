using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreWithMongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //var connectionString = "mongodb://localhost:27017";
            ////var client = new MongoClient(connectionString);
            //var client = new MongoClient(MongoUrl.Create(connectionString));

            //var settings1 = MongoClientSettings
            //.FromUrl(MongoUrl.Create("mongodb://localhost:27017"));

            //var settings2 = new MongoClientSettings
            //{
            //    Server = new MongoServerAddress("localhost", 27017),
            //    UseSsl = false
            //};

            //var client1 = new MongoClient(settings1);
            //var client2 = new MongoClient(settings2);
            MainAsync().Wait();
            Console.ReadLine();
        }

 

        static async Task MainAsync()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            
            IMongoDatabase db = client.GetDatabase("school");

            //await db.CreateCollectionAsync("students", new CreateCollectionOptions
            //{
            //    MaxDocuments = 25,
            //    Capped = true
            //});

            //await db.CreateCollectionAsync("students");
            //var document = new BsonDocument
            //{
            //    {"firstname", BsonValue.Create("Peter")},
            //    {"lastname", new BsonString("Mbanugo")},
            //    { "subjects", new BsonArray(new[] {"English", "Mathematics", "Physics"}) },
            //    { "class", "JSS 3" },
            //    { "age", int.MaxValue }
            //};

            //var document = new BsonDocument();
            //document.Add("name", "Steven Johnson");
            //document.Add("age", 23);
            //document.Add("subjects", new BsonArray() { "English", "Mathematics", "Physics" });

            //IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("students");
            //await collection.InsertOneAsync(document);

            //var newStudents = CreateNewStudents();
            //await collection.InsertManyAsync(newStudents);

            //using (IAsyncCursor<BsonDocument> cursor = await collection.FindAsync(new BsonDocument()))
            //{
            //    while (await cursor.MoveNextAsync())
            //    {
            //        IEnumerable<BsonDocument> batch = cursor.Current;
            //        foreach (BsonDocument document in batch)
            //        {
            //            Console.WriteLine(document);
            //            Console.WriteLine();
            //        }
            //    }
            //}
            //collection.FindSync(filter).ToList();
            //await collection.FindSync(filter).ToListAsync();
            //await collection.FindSync(filter).ForEachAsync(doc => Console.WriteLine());
            //collection.FindSync(filter).FirstOrDefault();
            //collection.FindSync(filter).FirstOrDefault();
            //await collection.FindSync(filter).FirstOrDefaultAsync();

            //Using BsonDocument or string
            //var filter = new BsonDocument("firstname", "Peter");
            //var filter = "{ FirstName: 'Peter'}";
            // var filter = new BsonDocument("age", new BsonDocument("$eq", 23));
            //var filter = "{ age: {'$eq': 23}}";
            //var filter = Builders<BsonDocument>.Filter.Lt("age", 25);
            //var filter = Builders<Student>.Filter.Lt(student => student.Age, 25);
           // await collection.Find(filter).ForEachAsync(document => Console.WriteLine(document));
            //LINQ Expression
            
            var collection = db.GetCollection<Student>("students");
            await collection.Find(student => student.Age < 25 && student.FirstName != "Peter")
                .ForEachAsync(student => Console.WriteLine(student.FirstName + " " + student.LastName));


            //await Task.Delay(10);
            Console.ReadLine();
        }

        private static IEnumerable<BsonDocument> CreateNewStudents()
        {
            var student1 = new BsonDocument
    {
      {"firstname", "Ugo"},
      {"lastname", "Damian"},
      {"subjects", new BsonArray {"English", "Mathematics", "Physics", "Biology"}},
      {"class", "JSS 3"},
      {"age", 23}
    };

            var student2 = new BsonDocument
    {
      {"firstname", "Julie"},
      {"lastname", "Lerman"},
      {"subjects", new BsonArray {"English", "Mathematics", "Spanish"}},
      {"class", "JSS 3"},
      {"age", 23}
    };

            var student3 = new BsonDocument
    {
      {"firstname", "Julie"},
      {"lastname", "Lerman"},
      {"subjects", new BsonArray {"English", "Mathematics", "Physics", "Chemistry"}},
      {"class", "JSS 1"},
      {"age", 25}
    };

            var newStudents = new List<BsonDocument>();
            newStudents.Add(student1);
            newStudents.Add(student2);
            newStudents.Add(student3);

            return newStudents;
        }
    }
}
