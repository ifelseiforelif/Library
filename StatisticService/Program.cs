namespace StatisticService;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

sealed class Author
{
    public string Name { get; set; } = String.Empty;
    public string Surname { get; set; } = String.Empty;
}

internal class Program
{

    static async Task Main(string[] args)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672
        };

        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (sender, e) =>
        {
            // отримуємо байти повідомлення
            var body = e.Body.ToArray();

            // конвертуємо у string
            var json = Encoding.UTF8.GetString(body);

            // десеріалізуємо JSON у об'єкт
            var message = JsonSerializer.Deserialize<Author>(json);

            Console.WriteLine($"Name: {message.Name}");
            Console.WriteLine($"Surname: {message.Surname}");
        };

        await channel.BasicConsumeAsync(
            queue: "Authors",
            autoAck: true,
            consumer: consumer
        );

        Console.WriteLine("Waiting messages...");
        Console.ReadLine();
    }
}
