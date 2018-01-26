using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Core.Interface
{
    public interface IRabbitMQListener
    {
        void CreateListener(Func<string, bool> insertIntoDb, string queueName);
    }
}
