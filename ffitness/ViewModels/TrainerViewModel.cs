using Ffitness.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ffitness.ViewModels
{
    public class TrainerViewModel
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Description { get; set; }

        public List<Activity> Activities { get; set; }

        //public IFormFile ProfileImage { get; set; }
    }
}
