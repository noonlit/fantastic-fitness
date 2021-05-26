using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.Models
{
    public class UserRole : IdentityRole
    {
        [Required]
        public override string Name { get; set; }
    }
}
