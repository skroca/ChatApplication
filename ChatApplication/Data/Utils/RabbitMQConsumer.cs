using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Data.Models;


namespace Data.Utils
{
    internal class RabbitMQConsumer : IHostedService
    {
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

            _channel.QueueDeclare(queue: "messages", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += (model, ea) =>
            {
                var receivedMessage = Encoding.UTF8.GetString(ea.Body.ToArray());
                
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
