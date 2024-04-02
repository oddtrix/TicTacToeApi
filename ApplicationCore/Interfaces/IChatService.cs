using Domain.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IChatService
    {
        Guid CreateChat();

        Guid CreateMessage(string messageBody, Guid chatId, Guid playerId);
    }
}
