using ApplicationCore.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace ApplicationCore.Services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork unitOfWork;

        public ChatService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Guid CreateChat()
        {
            var chat = new Chat() { Id = Guid.NewGuid() };
            this.unitOfWork.ChatRepository.Create(chat);
            this.unitOfWork.Save();
            return chat.Id;
        }

        public Guid CreateMessage(string messageBody, Guid chatId, Guid playerId)
        {
            var message = new Message()
            {
                Id = Guid.NewGuid(),
                MessageBody = messageBody,
                ChatId = chatId,
                PlayerId = playerId,
                DateTime = DateTime.Now
            };
            this.unitOfWork.MessageRepository.Create(message);
            this.unitOfWork.Save();
            return message.Id;
        }
    }
}
