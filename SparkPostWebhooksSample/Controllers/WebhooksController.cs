using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SparkPostWebhooksSample.Models;

namespace SparkPostWebhooksSample.Controllers
{
    [Route("api/webhook")]
    public class WebhooksController : Controller
    {
        private readonly SubscribersDbContext _db;

        public WebhooksController(SubscribersDbContext subscribersDb)
        {
            // Get instance for the DbContext from the DI
            _db = subscribersDb;
        }

        // POST: api/webhook
        [HttpPost]
        public async Task<IActionResult> ReceiveEventsAsync([FromBody] JArray payload)
        {
            // List of SparkPost event types that requires unsubscribe from the emails list
            var unsubscribe_events = new string[] { "bounce", "list_unsubscribe", "spam_complaint", "out_of_band", "link_unsubscribe" };
            // Check if the request paload could be parsed as JSON array
            if (!ModelState.IsValid)
                return BadRequest();
            // We are stripping msys object
            var msys_object = payload.OfType<JObject>().Select(obj => obj["msys"]).Cast<JObject>();
            // Filtering out pings
            // Then stripping the first *_event structure
            var event_object = msys_object.Where(msys => msys.Properties().FirstOrDefault() != null) 
                                          .Select(msys => msys.Properties().FirstOrDefault().Value).Cast<JObject>(); 
            // Filter on event type
            // Then extract subscriber email
            var subscribers_emails = event_object.Where(evt => unsubscribe_events.Contains(evt["type"].ToString())) 
                                                 .Select(msg => msg["rcpt_to"].ToString());
            // Remove emails duplication
            var extracted_emails = subscribers_emails.Distinct().ToList();

            foreach (var email in extracted_emails)
            {
                // Check if the email already exist
                var subscriber = _db.Subscribers.FirstOrDefault(s => s.Email == email); 
                if (subscriber != null)
                {
                    // Unsubscribe
                    subscriber.Subscribed = false; 
                }
            }
            // Save changes to the database
            await _db.SaveChangesAsync(); 
            // Return list of the affected emails as a feedback
            return Ok(extracted_emails); 
        }

        // GET: /
        [Route("/")]
        [HttpGet]
        public IActionResult Home()
        {
            return Json(new { message = "Your webhook is up and running. You can try it by sending [POST] requests to: '/api/webhook'." });
        }
    }
}