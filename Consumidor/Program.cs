using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumidor
{
    /*Cria um consumidor simples de mensagens. Este consumidor fica vinculado a uma fila esperando que um emissor envie mensagens. Quando as mensagens chegam na fila
     este consumidor recebe estas mensagens e faz o processamento necessario. Neste exemplo, a mensagem recebida será mostrada no console, e só.*/
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "fila_01",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumidor = new EventingBasicConsumer(channel);

                consumidor.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var mensagem = Encoding.UTF8.GetString(body);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    Console.WriteLine($"Mensagem Recebida:{mensagem}");
                };

                channel.BasicConsume(queue: "fila_01",
                    autoAck: false,
                    consumer: consumidor);

                Console.ReadLine();
            }
           

        }
    }
}
