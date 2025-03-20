using MongoDB.Bson;

namespace DomainDrivenDesign.Domain.Entities;

public class GroupMongo
{
    public ObjectId Id { get; set; }
    public string ConversationId { get; set; }
    public GroupMongoMessage[] Messages { get; set; } = Array.Empty<GroupMongoMessage>();
}
