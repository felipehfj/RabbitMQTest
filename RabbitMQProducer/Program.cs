using System.Text;
using RabbitMQ.Client;


var factory = new ConnectionFactory() { HostName = "192.168.1.13", Port = 5672 };
using (var connection = factory.CreateConnection())

using (var channel = connection.CreateModel())
{

    foreach (var item in Enumerable.Range(1, 1000))
    {
        channel.QueueDeclare(queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

        string message = $"{Guid.NewGuid().ToString().Replace("-","")}";
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                             routingKey: "hello",
                             basicProperties: null,
                             body: body);
        Console.WriteLine($" [{item}] Sent {message}");
    }
   
}