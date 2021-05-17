using Ffitness.Models;

namespace Ffitness.ViewModels
{
	public class BookingViewModel
	{
		public int Id { get; set; }

		public ScheduledActivity ScheduledActivity { get; set; }

		public int ScheduledActivityId { get; set; }

		public ApplicationUser User { get; set; }

		public int UserId { get; set; }
	}
}
