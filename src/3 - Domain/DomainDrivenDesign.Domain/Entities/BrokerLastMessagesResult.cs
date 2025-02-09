namespace DomainDrivenDesign.Domain.Entities;

public class BrokerLastMessagesResult
{
    public BrokerLastMessagesResult()
    {
        Messages = new List<Message>();
    }
    public List<Message> Messages { get; set; }
    public DateTime DateSentAfter { get; set; }
    public DateTime DateSentBefore { get; set; }
}

