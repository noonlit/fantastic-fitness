using Ffitness.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.ViewModels
{
	public class ApplicationUserViewModel
	{
        public string Id { get; set; }

        public IList<string> Roles { get; set; } = new List<string>();

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
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PlainPassword { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderType Gender { get; set; }
    }
}
