using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.Models.UserActions
{
	public class Booking
	{
        public enum Action
        {
            BookSpot,
			CancelBooking
        }

        public int Id { get; set; }
		public ApplicationUser User { get; set; }
		public string UserId { get; set; }
		public Action UserAction { get; set; }
		public ScheduledActivity ScheduledActivity { get; set; }
		public int ScheduledActivityId { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
