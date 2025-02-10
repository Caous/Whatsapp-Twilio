using DomainDrivenDesign.Domain.Entities;

namespace DomainDrivenDesign.Application.Interfaces;

public interface IWhatsappService
{
    Task<BrokerLastMessagesResult> GetMessages(Message? filter);
    Task<List<IGrouping<string, Message>>> GetLastMessagens(Message? filter);  
    Task<int?> CountNewCustomerMonth(Message? filter);
    Task<int?> RecurrenceCustomer(Message? filter);
    Task<int?> CountNewTicketsSupport(Message? filter);
    Task<int?> CountMessagesPending(Message? filter);
}
