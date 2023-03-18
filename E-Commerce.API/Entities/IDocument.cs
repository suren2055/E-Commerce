using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace E_Commerce.API.Entities;

public interface IDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    ObjectId Id { get; set; }
    DateTime CreatedAt { get; }
}