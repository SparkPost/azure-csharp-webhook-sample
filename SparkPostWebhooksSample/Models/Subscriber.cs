using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkPostWebhooksSample.Models
{
    public class Subscriber
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool Subscribed { get; set; }
    }
}
