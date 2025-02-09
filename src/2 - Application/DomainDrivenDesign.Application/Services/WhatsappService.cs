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

    public async Task<BrokerLastMessagesResult> GetMessages(Message? filter)
    {
        return await _repositoryTwilio.GetMessages(filter);
    }
}
