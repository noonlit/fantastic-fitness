using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.ViewModels
{
	public class UserSubscriptionViewModel
	{
		public int Id { get; set; }
		public ApplicationUserViewModel User { get; set; }
		public string UserId { get; set; }
		public SubscriptionViewModel Subscription { get; set; }
		public int SubscriptionId { get; set; }
		public DateTime StartTime { get; set; }
	}
}
