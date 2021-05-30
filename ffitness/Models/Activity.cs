﻿using System;
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
        private string primaryHexCode;
        private string secondaryHexCode;
        private string activityPicture;

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        public ActivityType Type { get; set; }

        [MinLength(5)]
        public string ActivityPicture {
            get { return this.activityPicture; }
            set {
                if (value == null)
                    this.activityPicture = "default-activity-picture.jpg";
                else this.activityPicture = value;
            }
        }

        [Required]
        [Range(1, 5)]
        public int DifficultyLevel { get; set; }

        [MaxLength(7), MinLength(4)]
        public string PrimaryColour { 
            get { return this.primaryHexCode; }
            set {
                if (value == null)
                    this.primaryHexCode = "#9D908D";
                else this.primaryHexCode = value.ToUpper();
            }
        }

        [MaxLength(7), MinLength(4)]
        public string SecondaryColour
        {
            get { return this.secondaryHexCode; }
            set
            {
                if (value == null)
                    this.secondaryHexCode = "#000";
                else this.secondaryHexCode = value.ToUpper();
            }
        }

        public List<Trainer> Trainers { get; set; }
    }
}