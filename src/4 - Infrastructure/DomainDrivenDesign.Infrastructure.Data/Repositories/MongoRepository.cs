using DomainDrivenDesign.Domain.Entities;
using DomainDrivenDesign.Domain.Interfaces.Repositories;
using MongoDB.Driver;

namespace DomainDrivenDesign.Infrastructure.Data.Repositories;

public class MongoRepository : IMongoRepository
{
    private const string CollectionsExections = "executions";
    private const string CollectionsGroupMessages = "whatsapp";
    private readonly IMongoDatabase _dataBase;

    public MongoRepository(IMongoDatabase database)
    {
        _dataBase = database;
    }

    public async Task<ICollection<GroupMongo>> GetAllAsync()
    {
        var collection = _dataBase.GetCollection<GroupMongo>(CollectionsGroupMessages);

        var group = await collection.Find(Builders<GroupMongo>.Filter.Empty).ToListAsync();

        return group;
    }
}
