using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ffitness.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum GenderType
        {
            NOT_SPECIFIED,
            [Display(Name = "Female")]
            FEMALE,
            [Display(Name = "Male")]
            MALE,
            [Display(Name = "Non-binary")]
            NON_BINARY,
            [Display(Name = "Transgender")]
            TRANSGENDER,
            [Display(Name = "Intersex")]
            INTERSEX,
            [Display(Name = "Other")]
            OTHER
        }
        public GenderType? Gender { get; set; }
    }
}
