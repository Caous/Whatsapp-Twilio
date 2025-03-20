﻿using DomainDrivenDesign.Application.Entities;
using DomainDrivenDesign.Domain.Entities;

namespace DomainDrivenDesign.Application.Interfaces;

public interface IWhatsappService
{
    Task<BrokerLastMessagesResult> GetMessages(Message? filter);
    Task<ICollection<MessageDto>> GetLastMessagens(Message? filter);  
    Task<int?> CountNewCustomerMonth(Message? filter);
    Task<int?> RecurrenceCustomer(Message? filter);
    Task<int?> CountNewTicketsSupport(Message? filter);
    Task<int?> CountMessagesPending(Message? filter);
    Task<ICollection<Message>> ValitadorMessageAsync(string request);
    Task RegisterMessages(ICollection<Message> messages);
    Task<ICollection<GroupMongo>> GetAllAsync();
}
