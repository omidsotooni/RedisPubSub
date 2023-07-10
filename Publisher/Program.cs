using StackExchange.Redis;

namespace Publisher
{
    class Program
    {
        private const string RedisConStr = "127.0.0.1:6379,syncTimeout=10000,DefaultDatabase=2,allowAdmin=True,connectTimeout=3000,ssl=False,abortConnect=False,connectRetry=10";
        private static ConnectionMultiplexer _connection = ConnectionMultiplexer.Connect(RedisConStr);
        private const string ChannelName = "asp-net-core-channel";

        static void Main(string[] args)
        {
            Console.Title = "Console Publisher";

            var pubsub = _connection.GetSubscriber();

            RedisChannel myChannel = RedisChannel.Literal(ChannelName);

            pubsub.PublishAsync(myChannel, "This is a test message!", CommandFlags.FireAndForget);
            
            Console.WriteLine($"Message Successfully sent to {ChannelName}");

            Console.ReadLine();
        }
    }
}
