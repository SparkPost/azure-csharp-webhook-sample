using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkPostWebhooksSample.Models
{
    public class SubscribersDbContext : DbContext
    {

        public SubscribersDbContext(DbContextOptions<SubscribersDbContext> contextOptions) 
            : base(contextOptions) { }

        public DbSet<Subscriber> Subscribers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscriber>()
                        .HasAlternateKey(s => s.Email)
                        .HasName("Emails_UniqueKey");
        }
    }
}
