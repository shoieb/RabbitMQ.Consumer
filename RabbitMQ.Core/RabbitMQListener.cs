using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Core
{
    public class RabbitMQListener : IRabbitMQListener
    {
        private IChannelProvider _channelProvider;

        public RabbitMQListener(IChannelProvider channelProvider)
        {
            this._channelProvider = channelProvider;
        }

        public void CreateListener(Func<string, bool> handleMessage, string queueName)
        {
            var channel = this._channelProvider.GetChannel();

            channel.QueueDeclare(queueName, false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                handleMessage(message);
            };
            channel.BasicConsume(queueName, true, consumer);            
        }

    }
}
