using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace BooksApi.Models

{
    public class Book
    {
        //  the id property is required to mapping the CLR object to the MongoDb collection.
        //  , this property is anotated with the BsonId atribute to designate the property as the
        // document 's primary key.
        // It's also anotated with [BsonRepresentation( aBsonType) to allow passing the parameter
        // the parameter type string instead of an ObjectId structure.

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // The atribute value of Name represent the property name in the MongoDb collection.
        [JsonProperty("Name")]
        [BsonElement("Name")]
        public string BookName { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }
    }
}
