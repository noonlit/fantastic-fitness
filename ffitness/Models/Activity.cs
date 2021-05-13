using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.Models
{
    public class Activity
    {
        private string hexCode;

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [Range(1, 5)]
        public int DifficultyLevel { get; set; }

        [StringLength(7)]
        public String Colour { 
            get { return hexCode; }
            set {
                if (value == null)
                    hexCode = "#9D908D";
            }
        }
    }
}