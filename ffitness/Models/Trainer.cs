using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ffitness.Models
{
    public class Trainer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required, MinLength(5)]
        public String Description { get; set; }
        public List<Activity> Activities { get; set; }
    }
}
