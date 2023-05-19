using Data.Services;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace ChatApplication.Infrastructure
{
    public class ChatHub : Hub
    {
        public readonly IMessageService _messageService;
        private readonly string stockCodePattern = @"/stock=.+";
        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task SendMessage(string room, string user, string message)
        {
            var parsedMessage = "<div><b>" + user + ":</b> " + message + "</div>";

            if (Regex.Match(message, stockCodePattern).Success)
            {
                await _messageService.ProcessCommandAsync(room, message);
            }
            else
            {
                _messageService.AddMessage(int.Parse(room), parsedMessage);

                await Clients.Group(room).SendAsync("ReceivedMessage", user, message);
            }

        }
        public async Task AddToGroup(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
        }

    }
}
