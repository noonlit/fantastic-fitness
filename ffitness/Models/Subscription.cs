using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.Models
{
    public enum SubscriptionDuration : int
    {
        [Description("1 Month")]
        Month1 = 30,

        [Description("3 Months")]
        Month3 = 90,

        [Description("12 Months")]
        Month12 = 360
    }
    public class Subscription
    {
        public int Id { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public DateTime SubscriptionStart { get; set; }
        private DateTime subscriptionEnd;
        [Required]
        public DateTime SubscriptionEnd { 
            get => subscriptionEnd;
            set => subscriptionEnd = SubscriptionStart.AddDays((double)Duration);
        }
        [Required]
        public SubscriptionDuration Duration { get; set; }
        [Required]
        public double SubscriptionPrice { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public bool Active { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
