using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { HostName = "192.168.1.13", Port = 5672 };
using (var connection = factory.CreateConnection())

using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "hello",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [x] Received {0}", message);
        Task.Delay(1000).Wait();
    };

    channel.BasicConsume(queue: "hello",
                         autoAck: true,
                         consumer: consumer);

    //while (true)
    //{
    //    var data = channel.BasicGet("hello", true);
    //    var message = Encoding.UTF8.GetString(data.Body.ToArray());
    //    Console.WriteLine(" [x] Received {0}", message);
    //    Task.Delay(1000).Wait();
    //}


    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}