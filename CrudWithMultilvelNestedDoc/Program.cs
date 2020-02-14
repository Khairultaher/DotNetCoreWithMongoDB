using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace CrudWithMultilvelNestedDoc
{
    public class Program
    {
        private static Like NewLike()
        {
            return new Like
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Like 3"
            };
        }

        private static Track NewTrack()
        {
            return new Track
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Trak 3",
                Likes = new Like[] { }
            };
        }

        public static Episode NewShow()
        {
            return new Episode
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Episode 3",
                Tracks = new Track[] { }
            };
        }

        public static Channel NewChannel()
        {
            return new Channel
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Channel 2",
                Episodes = new Episode[] { }
            };
        }
        static void Main(string[] args)
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            IMongoDatabase db = client.GetDatabase("ContentDB");
            IMongoCollection<Channel> collection = db.GetCollection<Channel>("Channels");

            //Channel chann = NewChannel();
            //collection.InsertOne(chann);
            //Episode episode = NewShow();          
            Track track = NewTrack();
            Like like = NewLike();


            var chanelId = "5e4606e6ae7b090688671416";
            var episodeId = "5e46071d385a672b0cea0f86";
            var trackId = "5e460dbe2bc5e70c9cfeac21";

            var filter = Builders<Channel>.Filter.Eq(x => x.Id, chanelId);
            var arrayFilters = new List<ArrayFilterDefinition>();
            ArrayFilterDefinition<BsonDocument> episodesFilter = new BsonDocument("e._id", new BsonDocument("$eq", episodeId));
            ArrayFilterDefinition<BsonDocument> tracksFilter = new BsonDocument("t._id", new BsonDocument("$eq", trackId));
            arrayFilters.Add(episodesFilter);
            arrayFilters.Add(tracksFilter);

            var update = Builders<Channel>.Update.Push("Episodes.$[e].Tracks.$[t].Likes", like);
            var updateOptions = new UpdateOptions { ArrayFilters = arrayFilters };

            var result = collection.UpdateOne(filter, update, updateOptions);

            Console.ReadLine();
        }


    }
}
