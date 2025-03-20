using MongoDB.Bson;

namespace DomainDrivenDesign.Application.Entities.Response;

public class MessagesResponseDto
{
    public ObjectId Id { get; set; }
    public string ConversationId { get; set; }
    public string PhoneNumber { get; set; }
    public string DateConversation { get; set; }
    public GroupMessageResponseDto[] Messages { get; set; } = Array.Empty<GroupMessageResponseDto>();
}
