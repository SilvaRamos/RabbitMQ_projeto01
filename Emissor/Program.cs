using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ_Projeto01
{
    class Program
    {
        /*Envia uma mensagem simples para o RabbitMQ*/
        static void Main(string[] args)
        {
            string _mensagem = "Mensagem Direcionada!";
            var mensagem = Encoding.UTF8.GetBytes(_mensagem);

            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "fila_01",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                channel.BasicPublish(exchange: "",
                    routingKey: "fila_01",
                    basicProperties: null,
                    body: mensagem);

                Console.WriteLine($"Mensagem Enviada: {_mensagem}");
            }
        }
    }
}