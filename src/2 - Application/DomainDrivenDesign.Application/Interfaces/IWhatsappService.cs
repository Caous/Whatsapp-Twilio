using DomainDrivenDesign.Domain.Entities;

namespace DomainDrivenDesign.Application.Interfaces;

public interface IWhatsappService
{
    Task<BrokerLastMessagesResult> GetMessages(Message? filter);
}
