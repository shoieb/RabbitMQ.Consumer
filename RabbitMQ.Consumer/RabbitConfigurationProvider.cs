using Microsoft.Extensions.Configuration;
using RabbitMQ.Core.Helper;
using RabbitMQ.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public class RabbitConfigurationProvider : IRabbitConfigurationProvider
    {
        private IConfiguration _configuration;
        public RabbitConfigurationProvider(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public RabbbitConfiguration GetConfig()
        {
            var rabbitConfiguration = new RabbbitConfiguration();
            this._configuration.Bind("RabbitMQConfig", rabbitConfiguration);
            return rabbitConfiguration;
        }
    }
}
