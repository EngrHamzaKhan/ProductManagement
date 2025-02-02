using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using ProductManagement.Web.Services.EmailService;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Threading;

namespace ProductManagement.Web.Services.RabbitMQ
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQConsumer(IConfiguration configuration, IEmailService emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Initialize connection and channel
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:Host"],
                Port = int.Parse(_configuration["RabbitMQ:Port"] ?? "5672"),
                UserName = _configuration["RabbitMQ:Username"],
                Password = _configuration["RabbitMQ:Password"]
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            string queueName = _configuration["RabbitMQ:QueueName"];

            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var loginNotification = JsonConvert.DeserializeObject<LoginNotification>(message);

                // Send email notification
                _emailService.SendEmail(loginNotification.Email, "Login Notification", loginNotification.Message);
            };

            // Start consuming
            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            // Wait until cancellation token is triggered to stop the background service
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _channel?.Close();
            _connection?.Close();

            return base.StopAsync(stoppingToken);
        }
    }

    public class LoginNotification
    {
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
