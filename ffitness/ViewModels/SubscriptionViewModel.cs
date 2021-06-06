using Ffitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.ViewModels
{
    public class SubscriptionViewModel
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime SubscriptionStart { get; set; }
        public DateTime SubscriptionEnd { get; set; }
        public SubscriptionDuration Duration { get; set; }
        public double SubscriptionPrice { get; set; }
        public string UserId { get; set; }
    }
}
