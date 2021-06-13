using Ffitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.ViewModels
{
    public class SubscriptionWithBookingViewModel
    {
        public int Id { get; set; }
        public DateTime SubscriptionStart { get; set; }
        public DateTime SubscriptionEnd { get; set; }
        public SubscriptionDuration Duration { get; set; }
        public double SubscriptionPrice { get; set; }
        public List<int> BookingIds { get; set; }
        public string UserId { get; set; }
        public bool Active { get; set; }
    }
}
