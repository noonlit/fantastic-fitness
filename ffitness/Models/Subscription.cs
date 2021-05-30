using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.Models
{
    public enum SubscriptionDuration
    {
        [Description("1 Month")]
        Month1,

        [Description("3 Months")]
        Month3,

        [Description("12 Months")]
        Month12
    }
    public class Subscription
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public SubscriptionDuration Duration { get; set; }
        public double SubscriptionPrice { get; set; }
        public string UserId { get; set; }
    }
}
