using RabbitMQ.Core.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Core.Interface
{
    public interface IRabbitConfigurationProvider
    {
        RabbbitConfiguration GetConfig();
    }
}
