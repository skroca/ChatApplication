using AutoMapper;
using Data.DTOModels;
using Data.Models;
using Data.Repositories;
using Data.Utils;

namespace Data.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        public readonly IMessageRepository _messageRepository;
        public readonly IRabitMQProducer _rabbitMQProducer;

        public MessageService(IMapper mapper, IMessageRepository messageRepository, IRabitMQProducer rabbitMQProducer)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _rabbitMQProducer = rabbitMQProducer;
        }
        public void AddMessage(int idRoom, string Message)
        {
            Message message = new Message()
            {
                IdRoom = idRoom,
                MessageText = Message,
                DateCreated = DateTime.Now
            };
            _messageRepository.AddMessage(message);
        }

        public List<String> GetMessagesByIdRoom(int idRoom)
        {
            return _messageRepository.GetMessagesByIdRoom(idRoom).Select(x=> x.MessageText).ToList();
        }
        public async Task ProcessCommandAsync(string room,string command)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://stooq.com/q/l/?s={command.ToLower().Replace("/stock=","")}&f=sd2t2ohlcv&h&e=csv");

                if (response.IsSuccessStatusCode)
                {
                    Stream csvStream = await response.Content.ReadAsStreamAsync();
                    using (StreamReader reader = new StreamReader(csvStream))
                    {
                        int x = 0;
                        while (!reader.EndOfStream)
                        {
                            string line = await reader.ReadLineAsync();
                            if(x>0)
                            {
                                try
                                {
                                    var items = line.Split(",");
                                    var messageQueue = new MessageQueue()
                                    {
                                        IdRoom = room,
                                        Message = $"{items[0]} quote is ${items[3]} per share"
                                    };
                                    _rabbitMQProducer.SendMessage(messageQueue);
                                }
                                catch (Exception ex)
                                {
                                    _rabbitMQProducer.SendMessage("new message");
                                }
                            }
                            
                            x++;
                        }
                    }
                    
                }
                else
                {
                    Console.WriteLine("La llamada a la API falló. Código de estado: " + response.StatusCode);
                }
            }
        }
    }
}
