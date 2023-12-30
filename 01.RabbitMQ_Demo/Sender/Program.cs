using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
var uri = "amqp://guest:guest@localhost:5672";
factory.Uri = new(uri);

var providerName = "Rabbit Sender App";
factory.ClientProvidedName = providerName;

IConnection connection = factory.CreateConnection();

IModel channel = connection.CreateModel();

var exchangeName = "demo-exchange";
var queueName = "demo-queue";
var routingKey = "demo-key";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName, exchangeName, routingKey, null);


var i = 0;
// when enter keyboard will exit
while (true)
{
    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
    {
        break;
    }

    // Create a simple message
    var message = $"Message#{i}";
    var body = Encoding.UTF8.GetBytes(message);

    // Publish the message
    channel.BasicPublish(exchangeName, routingKey, null, body);
    Console.WriteLine($"Message sent to the RabbitMQ Exchange {message}.");

    Thread.Sleep(1000);
    i++;
}

channel.Close();
connection.Close();
