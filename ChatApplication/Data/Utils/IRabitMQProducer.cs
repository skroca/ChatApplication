namespace Data.Utils
{
    public interface IRabitMQProducer
    {
        public void SendMessage<T>(T message);
    }
}