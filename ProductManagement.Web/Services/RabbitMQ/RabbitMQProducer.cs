using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Connections;

namespace ProductManagement.Web.Services.RabbitMQ
{

    public class RabbitMQProducer
    {
        private readonly IConfiguration _configuration;

        public RabbitMQProducer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void PublishMessage(string email)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:Host"],      
                Port = int.Parse(_configuration["RabbitMQ:Port"] ?? "5672"), 
                UserName = _configuration["RabbitMQ:Username"],
                Password = _configuration["RabbitMQ:Password"]
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            string queueName = _configuration["RabbitMQ:QueueName"];
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var message = new { Email = email, Message = "You have successfully logged in!" };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }
    }

}
