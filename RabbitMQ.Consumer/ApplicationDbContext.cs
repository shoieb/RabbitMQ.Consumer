using Microsoft.EntityFrameworkCore;
using RabbitMQ.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.Database.Migrate();
        }
        public DbSet<MessageQueue> MessageQueue { get; set; }
    }
}
