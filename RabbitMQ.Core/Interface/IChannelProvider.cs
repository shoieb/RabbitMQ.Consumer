using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Core.Interface
{
    public interface IChannelProvider
    {
        IModel GetChannel();
    }
}
