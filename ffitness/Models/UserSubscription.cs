using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.Models
{
	public class UserSubscription
	{
		public int Id { get; set; }
		public ApplicationUser User { get; set; }
		public string UserId { get; set; }
		public Subscription Subscription { get; set; }
		public int SubscriptionId { get; set; }
		public DateTime StartTime { get; set; }
	}
}
