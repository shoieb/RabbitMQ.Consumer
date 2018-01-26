using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Core.Helper
{
    public class RabbbitConfiguration
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
