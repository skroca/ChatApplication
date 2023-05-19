using Data.Models;
using Microsoft.Extensions.Configuration;

namespace Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ChatApplicationDBContext context;

        public MessageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            context = new ChatApplicationDBContext(_configuration);
        }
        public Message AddMessage(Message message)
        {
            context.Messages.Add(message);
            context.SaveChanges();
            return message;
        }

        public List<Message> GetMessagesByIdRoom(int idRoom)
        {
            return context.Messages.Where(x => x.IdRoom == idRoom).OrderBy( x=> x.DateCreated).Take(50).ToList();       
        }
    }
}
