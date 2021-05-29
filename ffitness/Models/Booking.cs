using System.ComponentModel.DataAnnotations;

namespace Ffitness.Models
{
	public class Booking
	{
		public int Id { get; set; }

		public ScheduledActivity ScheduledActivity { get; set; }

		public int ScheduledActivityId { get; set; }

		public ApplicationUser User { get; set; }

		public string UserId { get; set; }
	}
}
