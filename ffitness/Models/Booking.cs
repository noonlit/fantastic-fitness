using System.ComponentModel.DataAnnotations;

namespace Ffitness.Models
{
	public class Booking
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public ScheduledActivity ScheduledActivity { get; set; }

		public int ScheduledActivityId { get; set; }

		[Required]
		public ApplicationUser User { get; set; }

		public string UserId { get; set; }
	}
}
