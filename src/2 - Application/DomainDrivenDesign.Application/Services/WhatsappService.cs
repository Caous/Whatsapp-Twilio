using DomainDrivenDesign.Application.Interfaces;
using DomainDrivenDesign.Domain.Entities;
using DomainDrivenDesign.Domain.Interfaces.Repositories;

namespace DomainDrivenDesign.Application.Services;

public class WhatsappService : IWhatsappService
{
    private readonly ITwilioRepository _repositoryTwilio;

    public WhatsappService(ITwilioRepository repositoryTwilio)
    {
        _repositoryTwilio = repositoryTwilio;
    }

    public async Task<int?> CountMessagesPending(Message? filter)
    {
        return 2;
    }

    public async Task<int?> CountNewCustomerMonth(Message? filter)
    {
        if (filter == null)
            filter = new Message() { DateCreate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), DateUpdated = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1) };

        var result = await _repositoryTwilio.GetMessages(filter);
        var count = result.Messages.Select(x => x.ConversationId).Distinct().Count();
        return count;
    }

    public async Task<int?> CountNewTicketsSupport(Message? filter)
    {
        //ToDo: Implementar posteriormente
        return 1;
    }

    public async Task<List<IGrouping<string, Message>>> GetLastMessagens(Message? filter)
    {
        var result = await _repositoryTwilio.GetMessages(filter);

        var resultGroup = result.Messages.GroupBy(x => x.ToUser).ToList();

        return resultGroup;

    }

    public async Task<BrokerLastMessagesResult> GetMessages(Message? filter)
    {
        return await _repositoryTwilio.GetMessages(filter);
    }

    public async Task<int?> RecurrenceCustomer(Message? filter)
    {
        if (filter == null)
            filter = new Message() { DateCreate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), DateUpdated = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1) };

        var recurrenceCustomer = new List<string>() { "whatsapp:+5511942616650", "whatsapp:+551193143-8599" };

        var result = await _repositoryTwilio.GetMessages(filter);
        var count = result.Messages.Select(x => recurrenceCustomer.Contains(x.ToUser)).Distinct().Count();
        return count;
    }
}
