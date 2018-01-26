using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Core;
using RabbitMQ.Core.Interface;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Domain;

namespace RabbitMQ.Consumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private IServiceProvider _serviceProvider;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = this.Configuration.GetConnectionString("ConnectionString");
            services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));

            services.AddSingleton<IRabbitConfigurationProvider, RabbitConfigurationProvider>();
            services.AddSingleton<IChannelProvider, ChannelProvider>();
            services.AddSingleton<IRabbitMQListener, RabbitMQListener>();

            // The RabbitMQ container starts before endpoints but it may
            // take several seconds for the broker to become reachable.
            Thread.Sleep(TimeSpan.FromSeconds(5));

            var sp = services.BuildServiceProvider();
            this._serviceProvider = sp;
            var rabbitListener = sp.GetService<IRabbitMQListener>();

            try
            {
                rabbitListener.CreateListener(this.HandleMessage, "docker.test.queue");
            }
            catch (Exception e)
            {

            }

            services.AddMvc();
        }

        private bool HandleMessage(string msg)
        {
            var dbContext = this._serviceProvider.GetService<ApplicationDbContext>();
            var queue = new MessageQueue
            {
                Message = msg
            };
            dbContext.MessageQueue.Add(queue);
            dbContext.SaveChanges();

            return true;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
