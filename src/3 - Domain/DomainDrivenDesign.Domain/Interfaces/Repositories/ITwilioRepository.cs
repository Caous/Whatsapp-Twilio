using DomainDrivenDesign.Domain.Entities;

namespace DomainDrivenDesign.Domain.Interfaces.Repositories;

public interface ITwilioRepository
{
    Task<BrokerLastMessagesResult> GetMessages(Message? filter);
}
