using ChatApplication.Models;
using Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        public readonly IMessageService _messageService;
        public ChatController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public static Dictionary<int, string> Rooms =
            new Dictionary<int, string>()
            {
                {1, "Aws Support" },
                {2, "GPC Support" },
                {3, "Azure Support" }
            };

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Room(int room)
        {
            RoomViewModel roomViewModel = new RoomViewModel();
            roomViewModel.Room = room;
            roomViewModel.Messages = _messageService.GetMessagesByIdRoom(room);
            return View("Room", roomViewModel);
        }
    }
}
