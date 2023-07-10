using StackExchange.Redis;

namespace Subscriber
{
    class Program
    {
        private const string RedisConStr = "127.0.0.1:6379,syncTimeout=10000,DefaultDatabase=2,allowAdmin=True,connectTimeout=3000,ssl=False,abortConnect=False,connectRetry=10";
        private static ConnectionMultiplexer _connection = ConnectionMultiplexer.Connect(RedisConStr);
        private const string ChannelName = "asp-net-core-channel";

        static void Main(string[] args)
        {
            Console.Title = "Console Subscriber";

            Console.WriteLine($"Listening {ChannelName}");

            var pubsub = _connection.GetSubscriber();

            RedisChannel myChannel = RedisChannel.Literal(ChannelName);

            pubsub.Subscribe(myChannel, (channel, message) => Console.WriteLine($"Message received from {ChannelName} : " + message));

            Console.ReadLine();
        }
    }
}
