using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.Models
{
    public enum ActivityType
    {
        Cardio,
        Aerobic,
        Strength,
        Yoga,
        Flexibility,
        Endurance,
        HIIT
    }
    public class Activity
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ActivityType Type { get; set; }

        public string ActivityPicture { get; set; }

        public int DifficultyLevel { get; set; }

        public string PrimaryColour { get; set; }

        public string SecondaryColour { get; set; }

        public List<Trainer> Trainers { get; set; }
    }
}