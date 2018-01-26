using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Domain;

namespace RabbitMQ.Consumer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ApplicationDbContext _context;
        public ValuesController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<MessageQueue> Get()
        {
            return  this._context.MessageQueue; 
        }       
    }
}
