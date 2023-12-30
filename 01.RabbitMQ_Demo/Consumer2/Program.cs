using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


ConnectionFactory factory = new();
var uri = "amqp://guest:guest@localhost:5672";
factory.Uri = new(uri);

var providerName = "Rabbit Consumer2 App";
factory.ClientProvidedName = providerName;

IConnection connection = factory.CreateConnection();

IModel channel = connection.CreateModel();

var exchangeName = "demo-exchange";
var queueName = "demo-queue";
var routingKey = "demo-key";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName, exchangeName, routingKey, null);
channel.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += async (sender, eventArgs) =>
{
    // Simulate some work
    await Task.Delay(TimeSpan.FromSeconds(3));
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Message received by the {providerName} : {message}");
    channel.BasicAck(eventArgs.DeliveryTag, false);
};

var channelTag = channel.BasicConsume(queueName, false, consumer);

Console.WriteLine("Press any key to exit.");
Console.ReadKey();

channel.BasicCancel(channelTag);
channel.Close();
connection.Close();
