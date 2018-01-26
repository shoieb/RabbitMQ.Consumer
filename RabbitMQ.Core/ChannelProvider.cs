using System;
using RabbitMQ.Client;
using RabbitMQ.Core.Interface;

namespace RabbitMQ.Core
{
    public class ChannelProvider : IChannelProvider
    {
        private IRabbitConfigurationProvider _rabbitConfigurationProvider;

        public ChannelProvider(IRabbitConfigurationProvider rabbitConfigurationProvider)
        {
            this._rabbitConfigurationProvider = rabbitConfigurationProvider;
        }
        public IModel GetChannel()
        {
            var configuration = this._rabbitConfigurationProvider.GetConfig();
            
            var factory = new ConnectionFactory
            {
                HostName = configuration.HostName,
                Port = configuration.Port,
                UserName = configuration.UserName,
                Password = configuration.Password
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            return channel;
        }
    }
}
