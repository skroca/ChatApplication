using Data.Models;

namespace Data.Repositories
{
    public interface IMessageRepository
    {
        public Message AddMessage(Message message);
        public List<Message> GetMessagesByIdRoom(int idRoom);
    }
}