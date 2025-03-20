using DomainDrivenDesign.Domain.Entities;

namespace DomainDrivenDesign.Domain.Interfaces.Repositories;

public interface IMongoRepository
{
    Task<ICollection<GroupMongo>> GetAllAsync();
}
