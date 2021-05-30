using Ffitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.ViewModels
{
	public class ActivityWithTrainersViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public ActivityType Type { get; set; }

		public string ActivityPicture { get; set; }

		public int DifficultyLevel { get; set; }

		public string PrimaryColour { get; set; }

		public string SecondaryColour { get; set; }

		public IEnumerable<TrainerViewModel> Trainers { get; set; }
	}
}
