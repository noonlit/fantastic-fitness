using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.ViewModels
{
	public class SubscriptionViewModel
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

        public int Id { get; set; }
        public string Description { get; set; }
        public SubscriptionDuration Duration { get; set; }
        public decimal Price { get; set; }
	}
}
