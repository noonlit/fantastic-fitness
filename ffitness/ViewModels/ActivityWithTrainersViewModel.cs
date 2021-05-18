﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.ViewModels
{
	public class ActivityWithTrainersViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Type { get; set; }

		public int DifficultyLevel { get; set; }

		public String Colour { get; set; }

		public IEnumerable<TrainerViewModel> Trainers { get; set; }
	}
}