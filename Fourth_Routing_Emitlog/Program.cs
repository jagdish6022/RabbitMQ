﻿using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace Fourth_Routing_Emitlog
{
    class Program
    {
        public static void Main(string[] args)
        {
            

            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange:"direct_logs",type:"direct");

                var severity = (args.Length > 0) ? args[0] : "message size was less than 0";

                var message = (args.Length > 1) ? string.Join(" ", args.ToArray()) : "hello world";

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange:"direct_logs",
                    routingKey:severity,
                    basicProperties:null,
                    body:body);

                Console.WriteLine("[x] sent {0} : {1}",severity,message);

            }

            Console.WriteLine("press Enter to the close the windows");

            Console.ReadLine();
        }
    }
}