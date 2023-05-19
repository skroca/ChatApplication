using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Data.DTOModels;

namespace ChatApplication.Infrastructure
{
    public class RabbitMQConsumer : IHostedService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public RabbitMQConsumer(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private EventingBasicConsumer _consumer;
        private Timer _timer;
        public Task StartAsync(CancellationToken cancellationToken)
        {

            _timer = new Timer(ConsumerMessage, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        public void ConsumerMessage(object state)
        {
            _connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "messages", durable: false, exclusive: false, autoDelete: true, arguments: null);
            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += async (model, ea) =>
            {
                var receivedMessage = Encoding.UTF8.GetString(ea.Body.ToArray());

                var result = JsonConvert.DeserializeObject<MessageQueue>(receivedMessage);

                await _hubContext.Clients.Group(result.IdRoom).SendAsync("ReceivedMessage", "BotUser", result.Message);
            };
            _channel.BasicConsume(queue: "messages", autoAck: true, consumer: _consumer);

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
