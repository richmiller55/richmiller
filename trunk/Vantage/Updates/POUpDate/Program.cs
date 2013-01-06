using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Util;
using System.Xml;

namespace POUpDate
{
    class Program
    {
        static void Main(string[] args)
        {
            ReceiveMessage();
        }
        public void HandleMessage(byte[] message)
        {
            System.Console.WriteLine(message);
        }
        public static void ReceiveMessage()
        {
            string HOST_NAME = "oak.rlm.io";
            string QUEUE_NAME = "podate";
            bool noAck = false;
            ConnectionFactory factory = new ConnectionFactory();
            factory.Protocol = Protocols.FromEnvironment();
            factory.HostName = HOST_NAME;
            factory.Port = AmqpTcpEndpoi1nt.UseDefaultPort;
            IConnection conn = factory.CreateConnection();
            IModel channel = conn.CreateModel();
            BasicGetResult result = channel.BasicGet(QUEUE_NAME, noAck);
            byte[] body = result.Body;
            string resBody = System.Text.Encoding.UTF8.GetString(body);
            System.Console.WriteLine(resBody);
            channel.BasicAck(result.DeliveryTag, false);
            SendMessage(resBody);
        }

        public static void SendMessage(string response)
        {
            ConnectionFactory factory = new ConnectionFactory();
            // factory.UserName = user;
            // factory.Password = pass;
            // factory.VirtualHost = vhost;
            factory.Protocol = Protocols.FromEnvironment();
            factory.HostName = "oak.rlm.io";
            factory.Port = AmqpTcpEndpoint.UseDefaultPort;
            IConnection conn = factory.CreateConnection();
            byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes("Hello, world!");
            IModel channel = conn.CreateModel();
            string exchangeName = "rlm";
            string routingKey = "poDateUpdate";
            string queueName = "poDateUpdate";
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);
            channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);
        }
    }
}