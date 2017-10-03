using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using SparkPostWebhooksSample.Models;
using Microsoft.EntityFrameworkCore;

namespace SparkPostWebhooksSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<SubscribersDbContext>(builder => builder.UseSqlite("Data Source=subscribers.db")); // SQLite connection string
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SubscribersDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            Seed(db); // Seed the database with sample data
        }

        public void Seed(SubscribersDbContext db)
        {
            db.Database.Migrate(); // Run the migrations to update the database to latest version

            if (!db.Subscribers.Any()) // Seed the database with sample data only when empty (first time only)
            {
                // The sample data represents a set of subscribers with their names, emails and the subscribtion status
                db.Subscribers.AddRange(new List<Subscriber> {
                        new Subscriber{ Name= "Recipient", Email="recipient@example.com", Subscribed=true},
                        new Subscriber{ Name= "Recipient1", Email="recipient1@example.com", Subscribed=true},
                        new Subscriber{ Name= "Recipient2", Email="recipient2@example.com", Subscribed=true},
                        new Subscriber{ Name= "Recipient3", Email="recipient3@example.com", Subscribed=true},
                        new Subscriber{ Name= "Recipient4", Email="recipient4@example.com", Subscribed=true},
                        new Subscriber{ Name= "Recipient5", Email="recipient5@example.com", Subscribed=true},
                        new Subscriber{ Name= "Recipient6", Email="recipient6@example.com", Subscribed=true}
                });

                db.SaveChanges();
            }
        }
    }
}
