namespace Data.Services
{
    public interface IMessageService
    {
        public void AddMessage(int idRoom, string Message);
        public List<String> GetMessagesByIdRoom(int idRoom);
        public Task ProcessCommandAsync(string room, string command);
    }
}