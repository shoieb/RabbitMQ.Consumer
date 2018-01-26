using System;
using System.ComponentModel.DataAnnotations;

namespace RabbitMQ.Domain
{
    public class MessageQueue
    {
        [Key]
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
